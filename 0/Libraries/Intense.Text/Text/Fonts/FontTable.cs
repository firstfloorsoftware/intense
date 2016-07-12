using Intense.Text.Fonts.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Represents a font table.
    /// </summary>
    public class FontTable
    {
        private FontTableRecord record;
        private Stream stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="FontTable"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        /// <param name="fixedTableLength"></param>
        internal FontTable(Stream stream, FontTableRecord record, int fixedTableLength = -1)
        {
            this.stream = stream;
            this.record = record;

            VerifyTableLength(fixedTableLength);
        }

        /// <summary>
        /// Verifies whether table has specified length.
        /// </summary>
        /// <param name="expectedLength"></param>
        protected void VerifyTableLength(int expectedLength)
        {
            if (expectedLength != -1 && record.Length != expectedLength) {
                throw new InvalidOperationException("UnexpectedTableLength");
            }
        }

        /// <summary>
        /// Reads the table with optional padding zeros
        /// </summary>
        /// <param name="includePaddingZeros"></param>
        /// <returns></returns>
        protected byte[] ReadTable(bool includePaddingZeros = false)
        {
            var length = (int)this.record.Length;
            if (includePaddingZeros) {
                length = (length + 4 - 1) / 4 * 4;
            }

            this.stream.Seek(this.record.Offset, SeekOrigin.Begin);
            return this.stream.ReadBytes(length);
        }

        /// <summary>
        /// Gets the name of the font table.
        /// </summary>
        public string TableName
        {
            get { return this.record.Name; }
        }

        /// <summary>
        /// Gets the table checksum.
        /// </summary>
        public UInt32 TableChecksum
        {
            get { return this.record.Checksum; }
        }

        /// <summary>
        /// Calculates whether the checksum for this table is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsChecksumValid()
        {
            var isHead = this.TableName == FontTableNames.Head;

            // buffer must include padded 0s
            var buffer = ReadTable(true);
            uint sum = 0;

            for (var i = 0; i < buffer.Length; i += 4) {
                // skip checksumAdjustment at position 8 in head
                if (isHead && i == 8) {
                    continue;
                }
                sum += buffer.ReadUInt32(i);
            }

            return this.record.Checksum == sum;
        }
    }
}
