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
    /// Format 6: Trimmed table mapping
    /// </summary>
    public class CmapEncodingTableFormat6
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat6(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(6, platformID, encodingID)
        {
            this.Length = buffer.ReadUInt16(index + 2);
            this.Language = buffer.ReadUInt16(index + 4);
            this.FirstCode = buffer.ReadUInt16(index + 6);
            this.EntryCount = buffer.ReadUInt16(index + 8);

            var b = ImmutableArray.CreateBuilder<UInt16>();
            for (var i = 0; i < this.EntryCount; i++) {
                b.Add(buffer.ReadUInt16(index + 10 + i * 2));
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
        /// First character code of subrange.
        /// </summary>
        public UInt16 FirstCode { get; }
        /// <summary>
        /// Number of character codes in subrange.
        /// </summary>
        public UInt16 EntryCount { get; }
        /// <summary>
        /// Array of glyph index values for character codes in the range.
        /// </summary>
        public ImmutableArray<UInt16> GlyphIds { get; }
    }
}
