using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Extension methods for the <see cref="Stream"/> class.
    /// </summary>
    internal static class StreamExtensions
    {
        /// <summary>
        /// Reads the specified number of bytes from the stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(this Stream stream, int length)
        {
            var buffer = new byte[length];
            if (stream.Read(buffer, 0, length) != length) {
                throw new IOException("UnexpectedEndOfStream");
            }
            return buffer;
        }
    }
}
