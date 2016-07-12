using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Format 0: Byte encoding table
    /// </summary>
    public class CmapEncodingTableFormat0
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat0(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(0, platformID, encodingID)
        {
            this.Length = buffer.ReadUInt16(index + 2);
            this.Language = buffer.ReadUInt16(index + 4);

            var b = ImmutableArray.CreateBuilder<byte>();
            for (var i = 0; i < 256; i++) {
                b.Add(buffer[index + 6 + i]);
            }

            this.GlyphIds = b.ToImmutable();
        }

        /// <summary>
        /// This is the length in bytes of the subtable.
        /// </summary>
        public UInt16 Length { get; }
        /// <summary>
        /// Language
        /// </summary>
        public UInt16 Language { get; }
        /// <summary>
        /// An array that maps character codes to glyph index values.
        /// </summary>
        public ImmutableArray<byte> GlyphIds { get; }
    }
}
