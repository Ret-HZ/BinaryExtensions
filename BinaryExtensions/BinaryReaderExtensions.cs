﻿using System;
using System.Buffers.Binary;
using System.IO;
using System.Linq;
using System.Text;

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


        /// <summary>
        /// Reads the specified number of characters from the current stream, returns the data in a <see langword="string"/>, and advances the current position in accordance with the <see cref="Encoding"/> used and the specific characters being read from the stream.
        /// </summary>
        /// <param name="length">The length of the string (number of characters to read).</param>
        /// <returns>A string containing data read from the underlying stream. This might be less than the number of characters requested if the end of the stream is reached.</returns>
        /// <exception cref="ArgumentException">The number of decoded characters to read is greater than <paramref name="length"/>. This can happen if a Unicode decoder returns fallback characters or a surrogate pair.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is negative.</exception>
        public static string ReadStringFixed(this BinaryReader binaryReader, int length)
        {
            char[] characters = binaryReader.ReadChars(length);
            string result = new string(characters);
            return result;
        }


        /// <summary>
        /// Reads a string suffixed with the specified <paramref name="token"/> from the current stream and advances the current position in accordance with the <see cref="Encoding"/> used and the specific characters being read from the stream.
        /// </summary>
        /// <param name="token">The token to find.</param>
        /// <returns>A string containing data read from the underlying stream. The <paramref name="token"/> is not part of the string. If the <paramref name="token"/> can not be found, the resulting string will be empty.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="token"/> is null or empty.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks>
        /// The stream position will be restored if the <paramref name="token"/> can not be found.</remarks>
        public static string ReadStringToToken(this BinaryReader binaryReader, string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            StringBuilder sb = new StringBuilder();
            char[] tokenBuffer = new char[token.Length];
            int tokenIndex = 0;
            bool isTokenMatch = false;
            long originalStreamPos = binaryReader.BaseStream.Position;

            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                char c = binaryReader.ReadChar();
                sb.Append(c);

                // Add to token buffer
                tokenBuffer[tokenIndex % token.Length] = c;
                tokenIndex++;

                // Check if the last read characters match the token when reordered
                if (tokenIndex >= token.Length && new string(tokenBuffer.Skip(tokenIndex % token.Length).Concat(tokenBuffer.Take(tokenIndex % token.Length)).ToArray()) == token)
                {
                    // Remove the token from the result string
                    sb.Length -= token.Length;
                    isTokenMatch = true;
                    break;
                }
            }

            // Only return the contents of the StringBuilder if the token was found
            // Otherwise we may return a string when reaching the end of the stream with no matches
            if (isTokenMatch)
            {
                return sb.ToString();
            }
            else
            {
                binaryReader.BaseStream.Position = originalStreamPos;
                return string.Empty;
            }
        }


        /// <summary>
        /// Reads a string suffixed with a null terminator from the current stream and advances the current position in accordance with the <see cref="Encoding"/> used and the specific characters being read from the stream.
        /// </summary>
        /// <returns>A string containing data read from the underlying stream. The null terminator is not part of the string.</returns>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <remarks>The stream position will be restored if the null terminator can not be found.</remarks>
        public static string ReadStringNullTerminated(this BinaryReader binaryReader)
        {
            return binaryReader.ReadStringToToken("\0");
        }
    }
}
