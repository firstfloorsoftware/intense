using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Identifies a font table in a font file.
    /// </summary>
    internal struct FontTableRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableRecord"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="checksum"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public FontTableRecord(string name, UInt32 checksum, UInt32 offset, UInt32 length)
        {
            this.Name = name;
            this.Checksum = checksum;
            this.Offset = offset;
            this.Length = length;
        }

        /// <summary>
        /// The table name.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The table checksum.
        /// </summary>
        public UInt32 Checksum { get; }
        /// <summary>
        /// Offset from the beginning of the font file.
        /// </summary>
        public UInt32 Offset { get; }
        /// <summary>
        /// The length of the table in number of bytes.
        /// </summary>
        public UInt32 Length { get; }
    }
}
