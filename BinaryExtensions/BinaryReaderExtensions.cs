using System;
using System.Buffers.Binary;
using System.IO;

namespace BinaryExtensions
{
    public static class BinaryReaderExtensions
    {
        // https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/BinaryReader.cs#L99
        private static void ThrowIfDisposed(this BinaryReader binaryReader)
        {
            if (!binaryReader.BaseStream.CanRead)
            {
                throw new ObjectDisposedException(null, "Cannot access a closed file.");
            }
        }


        // https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/IO/BinaryReader.cs#L472
        private static ReadOnlySpan<byte> InternalRead(this BinaryReader binaryReader, int numBytes)
        {
            binaryReader.ThrowIfDisposed();
            byte[] buffer = new byte[numBytes];
            binaryReader.BaseStream.ReadExactly(buffer.AsSpan(0, numBytes));

            return buffer;
        }


        /// <summary>
        /// Reads a signed byte from the current stream and advances the current position of the stream by one byte.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>A signed byte read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks>
        /// <see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.
        /// <para>Calls the original ReadSByte method. Only exists to support the <paramref name="isBigEndian"/> parameter.</para>
        /// </remarks>
        public static sbyte ReadSByte(this BinaryReader binaryReader, bool isBigEndian)
        {
            return binaryReader.ReadSByte();
        }


        /// <summary>
        /// Reads a signed byte from the current stream and advances the current position of the stream by one byte.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>A signed byte read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks>
        /// <see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.
        /// <para>Calls the original ReadByte method. Only exists to support the <paramref name="isBigEndian"/> parameter.</para>
        /// </remarks>
        public static byte ReadByte(this BinaryReader binaryReader, bool isBigEndian)
        {
            return binaryReader.ReadByte();
        }


        /// <summary>
        /// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks><see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.</remarks>
        public static short ReadInt16(this BinaryReader binaryReader, bool isBigEndian)
        {
            return isBigEndian ? BinaryPrimitives.ReadInt16BigEndian(binaryReader.InternalRead(2)) : binaryReader.ReadInt16();
        }


        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream and advances the position of the stream by two bytes.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>A 2-byte unsigned integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks><see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.</remarks>
        public static ushort ReadUInt16(this BinaryReader binaryReader, bool isBigEndian)
        {
            return isBigEndian ? BinaryPrimitives.ReadUInt16BigEndian(binaryReader.InternalRead(2)) : binaryReader.ReadUInt16();
        }


        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the position of the stream by four bytes.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>A 4-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks><see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.</remarks>
        public static int ReadInt32(this BinaryReader binaryReader, bool isBigEndian)
        {
            return isBigEndian ? BinaryPrimitives.ReadInt32BigEndian(binaryReader.InternalRead(4)) : binaryReader.ReadInt32();
        }


        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream and advances the position of the stream by four bytes.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>A 4-byte unsigned integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks><see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.</remarks>
        public static uint ReadUInt32(this BinaryReader binaryReader, bool isBigEndian)
        {
            return isBigEndian ? BinaryPrimitives.ReadUInt32BigEndian(binaryReader.InternalRead(4)) : binaryReader.ReadUInt32();
        }


        /// <summary>
        /// Reads an 8-byte signed integer from the current stream and advances the position of the stream by eight bytes.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>An 8-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks><see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.</remarks>
        public static long ReadInt64(this BinaryReader binaryReader, bool isBigEndian)
        {
            return isBigEndian ? BinaryPrimitives.ReadInt64BigEndian(binaryReader.InternalRead(8)) : binaryReader.ReadInt64();
        }


        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the position of the stream by eight bytes.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>An 8-byte unsigned integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks><see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.</remarks>
        public static ulong ReadUInt64(this BinaryReader binaryReader, bool isBigEndian)
        {
            return isBigEndian ? BinaryPrimitives.ReadUInt64BigEndian(binaryReader.InternalRead(8)) : binaryReader.ReadUInt64();
        }


        /// <summary>
        /// Reads a 4-byte floating point value from the current stream and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>A 4-byte floating point value read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks><see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.</remarks>
        public static float ReadSingle(this BinaryReader binaryReader, bool isBigEndian)
        {
            return isBigEndian ? BinaryPrimitivesExtensions.ReadSingleBigEndian(binaryReader.InternalRead(4)) : binaryReader.ReadSingle();
        }


        /// <summary>
        /// Reads an 8-byte floating point value from the current stream and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="isBigEndian"><see langword="true"/> to read as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <returns>An 8-byte floating point value read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks><see cref="BinaryReader"/> does not restore the file position after an unsuccessful read.</remarks>
        public static double ReadDouble(this BinaryReader binaryReader, bool isBigEndian)
        {
            return isBigEndian ? BinaryPrimitivesExtensions.ReadDoubleBigEndian(binaryReader.InternalRead(8)) : binaryReader.ReadDouble();
        }
    }
}
