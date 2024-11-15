using System;
using System.Buffers.Binary;
using System.IO;

namespace BinaryExtensions
{
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes a signed byte to the current stream and advances the stream position by one byte.
        /// </summary>
        /// <param name="value">The signed byte to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <remarks>Calls the original Write method. Only exists to support the <paramref name="isBigEndian"/> parameter.</remarks>
        public static void Write(this BinaryWriter binaryWriter, sbyte value, bool isBigEndian)
        {
            binaryWriter.Write(value);
        }


        /// <summary>
        /// Writes an unsigned byte to the current stream and advances the stream position by one byte.
        /// </summary>
        /// <param name="value">The unsigned byte to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <remarks>Calls the original Write method. Only exists to support the <paramref name="isBigEndian"/> parameter.</remarks>
        public static void Write(this BinaryWriter binaryWriter, byte value, bool isBigEndian)
        {
            binaryWriter.Write(value);
        }


        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream position by two bytes.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write(this BinaryWriter binaryWriter, short value, bool isBigEndian)
        {
            if (isBigEndian)
            {
                Span<byte> buffer = stackalloc byte[sizeof(short)];
                BinaryPrimitives.WriteInt16BigEndian(buffer, value);
                binaryWriter.BaseStream.Write(buffer);
            }
            else
            {
                binaryWriter.Write(value);
            }
        }


        /// <summary>
        /// Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes.
        /// </summary>
        /// <param name="value">The two-byte unsigned integer to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write(this BinaryWriter binaryWriter, ushort value, bool isBigEndian)
        {
            if (isBigEndian)
            {
                Span<byte> buffer = stackalloc byte[sizeof(ushort)];
                BinaryPrimitives.WriteUInt16BigEndian(buffer, value);
                binaryWriter.BaseStream.Write(buffer);
            }
            else
            {
                binaryWriter.Write(value);
            }
        }


        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write(this BinaryWriter binaryWriter, int value, bool isBigEndian)
        {
            if (isBigEndian)
            {
                Span<byte> buffer = stackalloc byte[sizeof(int)];
                BinaryPrimitives.WriteInt32BigEndian(buffer, value);
                binaryWriter.BaseStream.Write(buffer);
            }
            else
            {
                binaryWriter.Write(value);
            }
        }


        /// <summary>
        /// Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="value">The four-byte unsigned integer to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write(this BinaryWriter binaryWriter, uint value, bool isBigEndian)
        {
            if (isBigEndian)
            {
                Span<byte> buffer = stackalloc byte[sizeof(uint)];
                BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
                binaryWriter.BaseStream.Write(buffer);
            }
            else
            {
                binaryWriter.Write(value);
            }
        }


        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write(this BinaryWriter binaryWriter, long value, bool isBigEndian)
        {
            if (isBigEndian)
            {
                Span<byte> buffer = stackalloc byte[sizeof(long)];
                BinaryPrimitives.WriteInt64BigEndian(buffer, value);
                binaryWriter.BaseStream.Write(buffer);
            }
            else
            {
                binaryWriter.Write(value);
            }
        }


        /// <summary>
        /// Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte unsigned integer to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write(this BinaryWriter binaryWriter, ulong value, bool isBigEndian)
        {
            if (isBigEndian)
            {
                Span<byte> buffer = stackalloc byte[sizeof(ulong)];
                BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
                binaryWriter.BaseStream.Write(buffer);
            }
            else
            {
                binaryWriter.Write(value);
            }
        }


        /// <summary>
        /// Writes a four-byte floating-point value to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="value">The four-byte floating-point value to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write(this BinaryWriter binaryWriter, float value, bool isBigEndian)
        {
            if (isBigEndian)
            {
                Span<byte> buffer = stackalloc byte[sizeof(float)];
                BinaryPrimitivesExtensions.WriteSingleBigEndian(buffer, value);
                binaryWriter.BaseStream.Write(buffer);
            }
            else
            {
                binaryWriter.Write(value);
            }
        }


        /// <summary>
        /// Writes an eight-byte floating-point value to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte floating-point value to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write(this BinaryWriter binaryWriter, double value, bool isBigEndian)
        {
            if (isBigEndian)
            {
                Span<byte> buffer = stackalloc byte[sizeof(double)];
                BinaryPrimitivesExtensions.WriteDoubleBigEndian(buffer, value);
                binaryWriter.BaseStream.Write(buffer);
            }
            else
            {
                binaryWriter.Write(value);
            }
        }


        /// <summary>
        /// Writes the same byte to pad the stream.
        /// </summary>
        /// <param name="padByte">Byte to write as padding.</param>
        /// <param name="padding">Padding value.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="padding"/> parameter can not be negative.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WritePadding(this BinaryWriter binaryWriter, byte padByte, int padding)
        {
            if (padding < 0)
                throw new ArgumentOutOfRangeException(nameof(padding));

            if (padding <= 1)
            {
                return;
            }

            long currentAddress = binaryWriter.BaseStream.Position;
            long padAmount = (currentAddress % padding == 0) ? 0 : padding - (currentAddress % padding);

            for (long i = 0; i < padAmount; i++)
            {
                binaryWriter.Write(padByte);
            }
        }
    }
}
