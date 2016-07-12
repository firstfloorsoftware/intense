using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Provides information for horizontal layout.
    /// </summary>
    public class FontTableHhea
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableHhea"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableHhea(Stream stream, FontTableRecord record)
            : base(stream, record, 36)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadFixedVersion(0);
            this.Ascender = buffer.ReadInt16(4);
            this.Descender = buffer.ReadInt16(6);
            this.LineGap = buffer.ReadInt16(8);
            this.AdvanceWidthMax = buffer.ReadUInt16(10);
            this.MinLeftSideBearing = buffer.ReadInt16(12);
            this.MinRightSideBearing = buffer.ReadInt16(14);
            this.XMaxExtent = buffer.ReadInt16(16);
            this.CaretSlopeRise = buffer.ReadInt16(18);
            this.CaretSlopeRun = buffer.ReadInt16(20);
            this.CaretOffset = buffer.ReadInt16(22);
            // skip 4 reserved shorts
            this.MetricDataFormat = buffer.ReadInt16(32);
            this.NumberOfHMetrics = buffer.ReadUInt16(34);
        }

        /// <summary>
        /// Gets the table version number.
        /// </summary>
        public Version Version { get; }
        /// <summary>
        /// Typographic ascent.
        /// </summary>
        public Int16 Ascender { get; }
        /// <summary>
        /// Typographic descent.
        /// </summary>
        public Int16 Descender { get; }
        /// <summary>
        /// Typographic line gap. 
        /// </summary>
        public Int16 LineGap { get; }
        /// <summary>
        /// Maximum advance width value in 'hmtx' table.
        /// </summary>
        public UInt16 AdvanceWidthMax { get; }
        /// <summary>
        /// Minimum left sidebearing value in 'hmtx' table.
        /// </summary>
        public Int16 MinLeftSideBearing { get; }
        /// <summary>
        ///  Minimum right sidebearing value.
        /// </summary>
        public Int16 MinRightSideBearing { get; }
        /// <summary>
        /// X max extent
        /// </summary>
        public Int16 XMaxExtent { get; }
        /// <summary>
        /// Used to calculate the slope of the cursor 
        /// </summary>
        public Int16 CaretSlopeRise { get; }
        /// <summary>
        /// The caret slope run.
        /// </summary>
        public Int16 CaretSlopeRun { get; }
        /// <summary>
        /// The amount by which a slanted highlight on a glyph needs to be shifted to produce the best appearance. Set to 0 for non-slanted fonts
        /// </summary>
        public Int16 CaretOffset { get; }
        /// <summary>
        /// 0 for current format.
        /// </summary>
        public Int16 MetricDataFormat { get; }
        /// <summary>
        /// Number of hMetric entries in 'hmtx' table
        /// </summary>
        public UInt16 NumberOfHMetrics { get; }
    }
}
