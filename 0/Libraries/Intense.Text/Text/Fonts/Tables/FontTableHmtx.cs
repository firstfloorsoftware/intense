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
    /// Defines the horizontal metrics for each individual glyph.
    /// </summary>
    public class FontTableHmtx
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableHmtx"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        /// <param name="numberOfHMetrics"></param>
        /// <param name="numGlyphs"></param>
        internal FontTableHmtx(Stream stream, FontTableRecord record, int numberOfHMetrics, int numGlyphs)
            : base(stream, record)
        {
            var numberOfBearings = numGlyphs - numberOfHMetrics;
            var hMetricsLength = numberOfHMetrics * 4;
            var bearingsLength = numberOfBearings * 2;

            var length = hMetricsLength + bearingsLength;
            VerifyTableLength(length);

            var buffer = ReadTable();

            // read metrics
            var metrics = ImmutableArray.CreateBuilder<HorizontalMetric>();
            for (var i = 0; i < numberOfHMetrics; i++) {
                var advanceWidth = buffer.ReadUInt16(i * 4);
                var lsb = buffer.ReadInt16(i * 4 + 2);
                metrics.Add(new HorizontalMetric(advanceWidth, lsb));
            }
            this.Metrics = metrics.ToImmutable();

            // read left side bearings
            var bearings = ImmutableArray.CreateBuilder<Int16>();
            for (var i = 0; i < numberOfBearings; i++) {
                bearings.Add(buffer.ReadInt16(hMetricsLength + i * 2));
            }
            this.LeftSideBearings = bearings.ToImmutable();
        }

        /// <summary>
        /// Gets the horizontal metrics.
        /// </summary>
        public ImmutableArray<HorizontalMetric> Metrics { get; }
        /// <summary>
        /// Gets the left side bearings for monospaced glyphs.
        /// </summary>
        public ImmutableArray<Int16> LeftSideBearings { get; }
    }
}
