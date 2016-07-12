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
    /// Specifies the vertical spacing for each glyph in a vertical font
    /// </summary>
    public class FontTableVmtx
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableVmtx"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        /// <param name="numberOfVMetrics"></param>
        /// <param name="numGlyphs"></param>
        internal FontTableVmtx(Stream stream, FontTableRecord record, int numberOfVMetrics, int numGlyphs)
            : base(stream, record)
        {
            var numberOfBearings = numGlyphs - numberOfVMetrics;
            var vMetricsLength = numberOfVMetrics * 4;
            var bearingsLength = numberOfBearings * 2;

            var length = vMetricsLength + bearingsLength;
            VerifyTableLength(length);

            var buffer = ReadTable();

            // read metrics
            var metrics = ImmutableArray.CreateBuilder<VerticalMetric>();
            for (var i = 0; i < numberOfVMetrics; i++) {
                var advanceHeight = buffer.ReadUInt16(i * 4);
                var tsb = buffer.ReadInt16(i * 4 + 2);
                metrics.Add(new VerticalMetric(advanceHeight, tsb));
            }
            this.Metrics = metrics.ToImmutable();

            // read top side bearings
            var bearings = ImmutableArray.CreateBuilder<Int16>();
            for (var i = 0; i < numberOfBearings; i++) {
                bearings.Add(buffer.ReadInt16(vMetricsLength + i * 2));
            }
            this.TopSideBearings = bearings.ToImmutable();
        }

        /// <summary>
        /// Gets the vertical metrics.
        /// </summary>
        public ImmutableArray<VerticalMetric> Metrics { get; }
        /// <summary>
        /// Gets the top side bearings for monospaced glyphs.
        /// </summary>
        public ImmutableArray<Int16> TopSideBearings { get; }
    }
}
