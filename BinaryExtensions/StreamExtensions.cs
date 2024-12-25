using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BinaryExtensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current source.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the size of the buffer if that many bytes are not currently available, or zero (0) if the buffer's length is zero or the end of the stream has been reached.</returns>
        /// <exception cref="IOException">Stream was too long.</exception>
        /// https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs#L790
        public static int Read(this Stream stream, Span<byte> buffer)
        {
            byte[] sharedBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length);
            try
            {
                int numRead = stream.Read(sharedBuffer, 0, buffer.Length);
                if ((uint)numRead > (uint)buffer.Length)
                {
                    throw new IOException("Stream was too long.");
                }

                new ReadOnlySpan<byte>(sharedBuffer, 0, numRead).CopyTo(buffer);
                return numRead;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(sharedBuffer);
            }
        }


        /// <summary>
        /// Reads bytes from the current stream and advances the position within the stream until the <paramref name="buffer"/> is filled.
        /// </summary>
        /// <param name="buffer">A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current stream.</param>
        /// <exception cref="EndOfStreamException">
        /// The end of the stream is reached before filling the <paramref name="buffer"/>.
        /// </exception>
        /// <remarks>
        /// When <paramref name="buffer"/> is empty, this read operation will be completed without waiting for available data in the stream.
        /// </remarks>
        /// https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs#L827
        public static void ReadExactly(this Stream stream, Span<byte> buffer)
        {
            _ = stream.ReadAtLeastCore(buffer, buffer.Length, throwOnEndOfStream: true);
        }


        /// <summary>
        /// Reads <paramref name="count"/> number of bytes from the current stream and advances the position within the stream.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. When this method returns, the buffer contains the specified byte array with the values
        /// between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced
        /// by the bytes read from the current stream.
        /// </param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The number of bytes to be read from the current stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> is outside the bounds of <paramref name="buffer"/>.
        /// -or-
        /// <paramref name="count"/> is negative.
        /// -or-
        /// The range specified by the combination of <paramref name="offset"/> and <paramref name="count"/> exceeds the
        /// length of <paramref name="buffer"/>.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the stream is reached before reading <paramref name="count"/> number of bytes.
        /// </exception>
        /// <remarks>
        /// When <paramref name="count"/> is 0 (zero), this read operation will be completed without waiting for available data in the stream.
        /// </remarks>
        /// https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs#L855
        public static void ReadExactly(this Stream stream, byte[] buffer, int offset, int count)
        {
            stream.ValidateBufferArguments(buffer, offset, count);

            _ = stream.ReadAtLeastCore(buffer.AsSpan(offset, count), count, throwOnEndOfStream: true);
        }


        /// <summary>
        /// Reads at least a minimum number of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current stream.</param>
        /// <param name="minimumBytes">The minimum number of bytes to read into the buffer.</param>
        /// <param name="throwOnEndOfStream">
        /// <see langword="true"/> to throw an exception if the end of the stream is reached before reading <paramref name="minimumBytes"/> of bytes;
        /// <see langword="false"/> to return less than <paramref name="minimumBytes"/> when the end of the stream is reached.
        /// The default is <see langword="true"/>.
        /// </param>
        /// <returns>
        /// The total number of bytes read into the buffer. This is guaranteed to be greater than or equal to <paramref name="minimumBytes"/>
        /// when <paramref name="throwOnEndOfStream"/> is <see langword="true"/>. This will be less than <paramref name="minimumBytes"/> when the
        /// end of the stream is reached and <paramref name="throwOnEndOfStream"/> is <see langword="false"/>. This can be less than the number
        /// of bytes allocated in the buffer if that many bytes are not currently available.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="minimumBytes"/> is negative, or is greater than the length of <paramref name="buffer"/>.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// <paramref name="throwOnEndOfStream"/> is <see langword="true"/> and the end of the stream is reached before reading
        /// <paramref name="minimumBytes"/> bytes of data.
        /// </exception>
        /// <remarks>
        /// When <paramref name="minimumBytes"/> is 0 (zero), this read operation will be completed without waiting for available data in the stream.
        /// </remarks>
        /// https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs#L888
        public static int ReadAtLeast(this Stream stream, Span<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true)
        {
            stream.ValidateReadAtLeastArguments(buffer.Length, minimumBytes);

            return stream.ReadAtLeastCore(buffer, minimumBytes, throwOnEndOfStream);
        }


        // https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs#L896
        private static int ReadAtLeastCore(this Stream stream, Span<byte> buffer, int minimumBytes, bool throwOnEndOfStream)
        {
            Debug.Assert(minimumBytes <= buffer.Length);

            int totalRead = 0;
            while (totalRead < minimumBytes)
            {
                int read = stream.Read(buffer.Slice(totalRead));
                if (read == 0)
                {
                    if (throwOnEndOfStream)
                    {
                        throw new EndOfStreamException("Unable to read beyond the end of the stream.");
                    }

                    return totalRead;
                }

                totalRead += read;
            }

            return totalRead;
        }


        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">A region of memory. This method copies the contents of this region to the current stream.</param>
        /// https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs#L922
        public static void Write(this Stream stream, ReadOnlySpan<byte> buffer)
        {
            byte[] sharedBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length);
            try
            {
                buffer.CopyTo(sharedBuffer);
                stream.Write(sharedBuffer, 0, buffer.Length);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(sharedBuffer);
            }
        }


        /// <summary>Validates arguments provided to reading and writing methods on <see cref="Stream"/>.</summary>
        /// <param name="buffer">The array "buffer" argument passed to the reading or writing method.</param>
        /// <param name="offset">The integer "offset" argument passed to the reading or writing method.</param>
        /// <param name="count">The integer "count" argument passed to the reading or writing method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> was null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> was outside the bounds of <paramref name="buffer"/>, or
        /// <paramref name="count"/> was negative, or the range specified by the combination of
        /// <paramref name="offset"/> and <paramref name="count"/> exceed the length of <paramref name="buffer"/>.
        /// </exception>
        /// https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs#L958
        private static void ValidateBufferArguments(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Non negative number is required.");
            }

            if ((uint)count > buffer.Length - offset)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
        }


        // https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs#L976
        private static void ValidateReadAtLeastArguments(this Stream stream, int bufferLength, int minimumBytes)
        {
            if (minimumBytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Non negative number is required.");
            }

            if (bufferLength < minimumBytes)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Must not be greater than the length of the buffer.");
            }
        }


        /// <summary>
        /// Sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public static long Seek(this Stream stream, long offset, SeekOrigin origin = SeekOrigin.Begin)
        {
            return stream.Seek(offset, origin);
        }


        /// <summary>
        /// Writes the stream contents to a byte array, regardless of the <see cref="Stream.Position"/> property.
        /// </summary>
        /// <returns>A new byte array.</returns>
        public static byte[] ToArray(this Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                long originalPos = stream.Position;
                stream.Seek(0, SeekOrigin.Begin);

                int readBytes;
                while ((readBytes = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, readBytes);
                }

                stream.Position = originalPos;

                return ms.ToArray();
            }
        }


        /// <summary>
        /// Saved position stacks for each stream.
        /// </summary>
        private static readonly ConcurrentDictionary<Stream, Stack<long>> PositionStacks = new ConcurrentDictionary<Stream, Stack<long>>();


        /// <summary>
        /// Pushes the current position into a stack and moves to a new location relative to the specified origin.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="ArgumentNullException">The stream is <see langword="null"/>.</exception>
        /// <exception cref="NotSupportedException">The stream does not support seeking.</exception>
        public static void PushToPosition(this Stream stream, long offset, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (!stream.CanSeek)
                throw new NotSupportedException("Stream does not support seeking.");

            Stack<long> stack = PositionStacks.GetOrAdd(stream, _ => new Stack<long>());
            stack.Push(stream.Position);
            stream.Seek(offset, origin);
        }


        /// <summary>
        /// Pushes the current position into a stack.
        /// </summary>
        /// <exception cref="ArgumentNullException">The stream is <see langword="null"/>.</exception>
        public static void PushCurrentPosition(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            Stack<long> stack = PositionStacks.GetOrAdd(stream, _ => new Stack<long>());
            stack.Push(stream.Position);
        }


        /// <summary>
        /// Pops the last position from the stack and moves to it.
        /// </summary>
        /// <exception cref="ArgumentNullException">The stream is <see langword="null"/>.</exception>
        /// <exception cref="NotSupportedException">Stream does not support seeking.</exception>
        /// <exception cref="InvalidOperationException">No position to pop for the given stream.</exception>
        public static void PopPosition(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (!stream.CanSeek)
                throw new NotSupportedException("Stream does not support seeking.");

            if (PositionStacks.TryGetValue(stream, out var stack) && stack.Count > 0)
            {
                stream.Position = stack.Pop();
            }
            else
            {
                throw new InvalidOperationException("No position to pop for the given stream.");
            }
        }
    }
}
