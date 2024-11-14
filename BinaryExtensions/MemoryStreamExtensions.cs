using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace BinaryExtensions
{
    public static class MemoryStreamExtensions
    {
        // https://github.com/dotnet/runtime/blob/d3d0fced1d125483d668bef176618e6a805cb2bd/src/libraries/System.Private.CoreLib/src/System/IO/MemoryStream.cs#L214
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ReadOnlySpan<byte> InternalReadSpan(this MemoryStream memoryStream, int count)
        {
            if (!memoryStream.CanRead)
            {
                throw new ObjectDisposedException(null, "Can not access a closed Stream.");
            }

            long origPos = memoryStream.Position;
            long newPos = memoryStream.Position + count;

            if (newPos > memoryStream.Length)
            {
                memoryStream.Position = memoryStream.Length;
                throw new EndOfStreamException();
            }

            byte[] buffer = new byte[count];
            var span = new ReadOnlySpan<byte>(buffer, (int)origPos, count);
            memoryStream.Position = newPos;
            return span;
        }
    }
}
