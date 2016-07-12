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
    /// Format 4: Segment mapping to delta values
    /// </summary>
    public class CmapEncodingTableFormat4
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat4(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(4, platformID, encodingID)
        {
            this.Length = buffer.ReadUInt16(index + 2);
            this.Language = buffer.ReadUInt16(index + 4);
            this.SegCountX2 = buffer.ReadUInt16(index + 6);
            this.SearchRange = buffer.ReadUInt16(index + 8);
            this.EntrySelector = buffer.ReadUInt16(index + 10);
            this.RangeShift = buffer.ReadUInt16(index + 12);

            var segCount = this.SegCountX2 / 2;

            var offset = index + 14;
            var endCounts = ImmutableArray.CreateBuilder<UInt16>();
            for (var i = 0; i < segCount; i++) {
                endCounts.Add(buffer.ReadUInt16(offset + i * 2));
            }
            this.EndCounts = endCounts.ToImmutable();

            offset += this.SegCountX2;
            this.ReservedPad = buffer.ReadUInt16(offset);

            offset += 2;
            var startCounts = ImmutableArray.CreateBuilder<UInt16>();
            for (var i = 0; i < segCount; i++) {
                startCounts.Add(buffer.ReadUInt16(offset + i * 2));
            }
            this.StartCounts = startCounts.ToImmutable();

            offset += this.SegCountX2;
            var idDeltas = ImmutableArray.CreateBuilder<Int16>();
            for (var i = 0; i < segCount; i++) {
                idDeltas.Add(buffer.ReadInt16(offset + i * 2));
            }
            this.IdDeltas = idDeltas.ToImmutable();

            offset += this.SegCountX2;
            var idRangeOffsets = ImmutableArray.CreateBuilder<UInt16>();
            for (var i = 0; i < segCount; i++) {
                idRangeOffsets.Add(buffer.ReadUInt16(offset + i * 2));
            }
            this.IdRangeOffsets = idRangeOffsets.ToImmutable();

            offset += this.SegCountX2;
            var glyphIds = ImmutableArray.CreateBuilder<UInt16>();
            // read until end of table is reached
            while(offset < index + this.Length) {
                glyphIds.Add(buffer.ReadUInt16(offset));
                offset += 2;
            }
            this.GlyphIds = glyphIds.ToImmutable();
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
        /// 2 x segCount
        /// </summary>
        public UInt16 SegCountX2 { get; }
        /// <summary>
        /// 2 x (2**floor(log2(segCount)))
        /// </summary>
        public UInt16 SearchRange { get; }
        /// <summary>
        /// log2(searchRange/2) 
        /// </summary>
        public UInt16 EntrySelector { get; }
        /// <summary>
        /// 2 x segCount - searchRange 
        /// </summary>
        public UInt16 RangeShift { get; }
        /// <summary>
        /// End characterCode for each segment, last=0xFFFF.
        /// </summary>
        public ImmutableArray<UInt16> EndCounts { get; }
        /// <summary>
        ///  Set to 0. 
        /// </summary>
        public UInt16 ReservedPad { get; }
        /// <summary>
        ///  Start character code for each segment. 
        /// </summary>
        public ImmutableArray<UInt16> StartCounts { get; }
        /// <summary>
        /// Delta for all character codes in segment.
        /// </summary>
        public ImmutableArray<Int16> IdDeltas { get; }
        /// <summary>
        /// Offsets into glyphIdArray or 0
        /// </summary>
        public ImmutableArray<UInt16> IdRangeOffsets { get; }
        /// <summary>
        /// Glyph index array 
        /// </summary>
        public ImmutableArray<UInt16> GlyphIds { get; }
    }
}
