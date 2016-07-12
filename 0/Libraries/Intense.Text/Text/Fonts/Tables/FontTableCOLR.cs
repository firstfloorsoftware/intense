using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Adds support for multi-colored glyphs
    /// </summary>
    public class FontTableCOLR
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableCOLR"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableCOLR(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadUInt16(0);
            this.NumBaseGlyphRecords = buffer.ReadUInt16(2);
            var offsetBaseGlyphRecord = buffer.ReadUInt32(4);
            var offsetLayerRecord = buffer.ReadUInt32(8);
            this.NumLayerRecords = buffer.ReadUInt16(12);

            // TODO: implement
        }

        /// <summary>
        /// Gets the table version number.
        /// </summary>
        public UInt16 Version { get; }
        /// <summary>
        /// Number of Base Glyph Records.
        /// </summary>
        public UInt16 NumBaseGlyphRecords { get; }
        /// <summary>
        /// Number of Layer Records.
        /// </summary>
        public UInt16 NumLayerRecords { get; }
    }
}
