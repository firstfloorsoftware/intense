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
    /// Vertical Device Metrics
    /// </summary>
    public class FontTableVDMX
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableVDMX"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableVDMX(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadUInt16(0);
            this.NumRecs = buffer.ReadUInt16(2);
            this.NumRatios = buffer.ReadUInt16(4);

            var offset = 6;
            var ranges = ImmutableArray.CreateBuilder<RatioRange>();
            for (var i = 0; i < this.NumRatios; i++) {
                ranges.Add(new RatioRange(buffer, offset));
                offset += 4;

            }
            this.RatioRanges = ranges.ToImmutable();

            // TODO: implement
        }

        /// <summary>
        /// Version number.
        /// </summary>
        public UInt16 Version { get; }
        /// <summary>
        /// Number of VDMX groups present
        /// </summary>
        public UInt16 NumRecs { get; }
        /// <summary>
        /// Number of aspect ratio groupings
        /// </summary>
        public UInt16 NumRatios { get; }
        /// <summary>
        /// Ratio ranges
        /// </summary>
        public ImmutableArray<RatioRange> RatioRanges { get; }
    }
}
