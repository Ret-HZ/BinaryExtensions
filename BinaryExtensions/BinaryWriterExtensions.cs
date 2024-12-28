using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace BinaryExtensions
{
    public static class BinaryWriterExtensions
    {
        #region Write
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
        /// Writes a string to the current stream and advances the current position of the stream in accordance with the <see cref="Encoding"/> used and the specific characters being written to the stream.
        /// </summary>
        /// <param name="value">The string to write.</param>
        /// <param name="encoding">Optional text encoding to use.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <remarks>If <paramref name="encoding"/> is <see langword="null"/>, it will use the <see cref="BinaryWriter"/>'s default encoding.</remarks>
        public static void Write(this BinaryWriter binaryWriter, string value, Encoding encoding = null)
        {
            // Use BinaryWriter's encoding by making it write a char array
            if (encoding == null)
                binaryWriter.Write(value.ToCharArray());
            else
                binaryWriter.Write(encoding.GetBytes(value));
        }


        /// <summary>
        /// Writes a string to the current stream and advances the current position of the stream in accordance with the <see cref="Encoding"/> used and the specific characters being written to the stream.
        /// </summary>
        /// <param name="value">The string to write.</param>
        /// <param name="nullTerminator">If set to <see langword="true" />, add null terminator.</param>
        /// <param name="encoding">Optional text encoding to use.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <remarks>If <paramref name="encoding"/> is <see langword="null"/>, it will use the <see cref="BinaryWriter"/>'s default encoding.</remarks>
        public static void Write(this BinaryWriter binaryWriter, string value, bool nullTerminator, Encoding encoding = null)
        {
            binaryWriter.Write(value, encoding);
            if (nullTerminator)
                // Use BinaryWriter's encoding by making it write a char 
                if (encoding == null)
                    binaryWriter.Write('\0');
                else
                    binaryWriter.Write(encoding.GetBytes("\0"));
        }
        #endregion


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


        #region WriteAtPosition
        /// <summary>
        /// Moves to the specified position in the stream, writes a signed byte and returns to the original position.
        /// </summary>
        /// <param name="value">The signed byte to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, sbyte value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes an unsigned byte and returns to the original position.
        /// </summary>
        /// <param name="value">The unsigned byte to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, byte value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes a two-byte signed integer and returns to the original position.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, short value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes a two-byte unsigned integer and returns to the original position.
        /// </summary>
        /// <param name="value">The two-byte unsigned integer to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, ushort value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes a four-byte signed integer and returns to the original position.
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, int value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes a four-byte unsigned integer and returns to the original position.
        /// </summary>
        /// <param name="value">The four-byte unsigned integer to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, uint value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes an eight-byte signed integer and returns to the original position.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, long value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes an eight-byte unsigned integer and returns to the original position.
        /// </summary>
        /// <param name="value">The eight-byte unsigned integer to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, ulong value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes a four-byte floating-point value and returns to the original position.
        /// </summary>
        /// <param name="value">The four-byte floating-point value to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, float value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes an eight-byte floating-point value and returns to the original position.
        /// </summary>
        /// <param name="value">The eight-byte floating-point value to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, double value, long offset, bool isBigEndian = false, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, isBigEndian);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes a string and returns to the original position.
        /// </summary>
        /// <param name="value">The string to write.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, string value, long offset, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, false);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes a string and returns to the original position.
        /// </summary>
        /// <param name="value">The string to write.</param>
        /// <param name="nullTerminator">If set to <see langword="true"/>, add null terminator.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, string value, bool nullTerminator, long offset, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, nullTerminator);
            binaryWriter.BaseStream.Position = originalPosition;
        }


        /// <summary>
        /// Moves to the specified position in the stream, writes a string and returns to the original position.
        /// </summary>
        /// <param name="value">The string to write.</param>
        /// <param name="nullTerminator">If set to <see langword="true"/>, add null terminator.</param>
        /// <param name="encoding">Text encoding to use.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteAtPosition(this BinaryWriter binaryWriter, string value, bool nullTerminator, Encoding encoding, long offset, SeekOrigin origin = SeekOrigin.Begin)
        {
            long originalPosition = binaryWriter.BaseStream.Position;
            binaryWriter.BaseStream.Seek(offset, origin);
            binaryWriter.Write(value, nullTerminator, encoding);
            binaryWriter.BaseStream.Position = originalPosition;
        }
        #endregion


        /// <summary>
        /// Repeatedly writes an unsigned byte to the current stream as many times as specified.
        /// </summary>
        /// <param name="value">The unsigned byte to write.</param>
        /// <param name="times">The amount of times the <paramref name="value"/> byte will be written.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="times"/> parameter can not be negative.</exception>
        public static void WriteTimes(this BinaryWriter binaryWriter, byte value, long times)
        {
            if (times < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(times));
            }

            for (long i = 0; i < times; i++)
            {
                binaryWriter.Write(value);
            }
        }


        #region WriteByType
        /// <summary>
        /// Writes a value of the specified type to the current stream and advances the stream position accordingly.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="NotSupportedException">The specified type is not supported for writing.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteByType(this BinaryWriter binaryWriter, object value, bool isBigEndian = false)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Type type = value.GetType();

            if (type == typeof(sbyte))
            {
                binaryWriter.Write((sbyte)value);
            }
            else if (type == typeof(byte))
            {
                binaryWriter.Write((byte)value);
            }
            else if (type == typeof(short))
            {
                binaryWriter.Write((short)value, isBigEndian);
            }
            else if (type == typeof(ushort))
            {
                binaryWriter.Write((ushort)value, isBigEndian);
            }
            else if (type == typeof(int))
            {
                binaryWriter.Write((int)value, isBigEndian);
            }
            else if (type == typeof(uint))
            {
                binaryWriter.Write((uint)value, isBigEndian);
            }
            else if (type == typeof(long))
            {
                binaryWriter.Write((long)value, isBigEndian);
            }
            else if (type == typeof(ulong))
            {
                binaryWriter.Write((ulong)value, isBigEndian);
            }
            else if (type == typeof(float))
            {
                binaryWriter.Write((float)value, isBigEndian);
            }
            else if (type == typeof(double))
            {
                binaryWriter.Write((double)value, isBigEndian);
            }
            else
            {
                throw new NotSupportedException($"Unsupported type '{type.Name}'.");
            }
        }


        /// <summary>
        /// Writes a value of the specified type to the current stream and advances the stream position accordingly.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the value to write.</typeparam>
        /// <param name="value">The value to write.</param>
        /// <param name="isBigEndian"><see langword="true"/> to write as Big Endian, <see langword="false"/> for Little Endian.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="NotSupportedException">The specified type is not supported for writing.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write<T>(this BinaryWriter binaryWriter, T value, bool isBigEndian = false)
        {
            binaryWriter.WriteByType(value, isBigEndian);
        }
        #endregion
    }
}
