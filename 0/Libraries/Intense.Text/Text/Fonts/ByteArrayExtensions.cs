using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Extension methods for byte array.
    /// </summary>
    internal static class ByteArrayExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Reads a string value from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadString(this byte[] buffer, int index, int length, Encoding encoding)
        {
            var result = encoding.GetString(buffer, index, length);

            // strip \0 characters
            var endPos = result.IndexOf(char.MinValue);
            if (endPos != -1) {
                result = result.Substring(0, endPos);
            }
            return result;
        }

        /// <summary>
        /// Reads a pascal string (value with a length byte prefix) from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReadPascalString(this byte[] buffer, int index, out int length)
        {
            length = buffer[index] + 1;     // + 1 counts for length byte

            return Encoding.UTF8.GetString(buffer, index + 1, length - 1);
        }

        /// <summary>
        /// Reads a 16-bit signed integer value from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Int16 ReadInt16(this byte[] buffer, int index)
        {
            return (Int16)(buffer[index + 1] | buffer[index] << 8);
        }

        /// <summary>
        /// Reads a 16-bit unsigned integer value from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt16 ReadUInt16(this byte[] buffer, int index)
        {
            return (UInt16)(buffer[index + 1] | buffer[index] << 8);
        }

        /// <summary>
        /// Reads a 32-bit unsigned integer value from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt32 ReadUInt32(this byte[] buffer, int index)
        {
            return (UInt32)(buffer[index + 3] | buffer[index + 2] << 0x08 | buffer[index + 1] << 0x10 | buffer[index] << 0x18);
        }

        /// <summary>
        /// Reads a 64-bit signed integer value from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Int64 ReadInt64(this byte[] buffer, int index)
        {
            var a = (Int64)buffer.ReadUInt32(index);
            var b = (Int64)buffer.ReadUInt32(index + 4);

            return a << 0x20 | b;
        }

        /// <summary>
        /// Reads a 64-bit unsigned integer value from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt64 ReadUInt64(this byte[] buffer, int index)
        {
            var a = (UInt64)buffer.ReadUInt32(index);
            var b = (UInt64)buffer.ReadUInt32(index + 4);

            return a << 0x20 | b;
        }

        /// <summary>
        /// Reads a date time value from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DateTime ReadDateTime(this byte[] buffer, int index)
        {
            // number of seconds since 12:00 midnight, January 1, 1904
            var value = buffer.ReadInt64(index);

            return Epoch.AddSeconds(value);
        }

        /// <summary>
        /// Reads a fixed version number from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Version ReadFixedVersion(this byte[] buffer, int index)
        {
            var major = buffer.ReadUInt16(index);
            var minor = buffer.ReadUInt16(index + 2);

            return new Version(major, minor);
        }

        /// <summary>
        /// Reads a fixed float number from the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static float ReadFixedFloat(this byte[] buffer, int index)
        {
            var a = buffer.ReadInt16(index);
            var b = buffer.ReadInt16(index + 2);

            // TODO: fix, should return a.b, not a+b

            return (float)a + (float)b;
        }
    }
}
