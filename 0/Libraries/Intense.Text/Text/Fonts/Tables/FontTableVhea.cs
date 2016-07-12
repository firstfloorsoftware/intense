using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Contains information needed for vertical fonts
    /// </summary>
    public class FontTableVhea
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableVhea"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableVhea(Stream stream, FontTableRecord record)
            : base(stream, record, 36)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadFixedVersion(0);
            this.Ascent = buffer.ReadInt16(4);
            this.Descent = buffer.ReadInt16(6);
            this.LineGap = buffer.ReadInt16(8);
            this.AdvanceHeightMax = buffer.ReadInt16(10);
            this.MinTopSideBearing = buffer.ReadInt16(12);
            this.MinBottomSideBearing = buffer.ReadInt16(14);
            this.YMaxExtent = buffer.ReadInt16(16);
            this.CaretSlopeRise = buffer.ReadInt16(18);
            this.CaretSlopeRun = buffer.ReadInt16(20);
            this.CaretOffset = buffer.ReadInt16(22);
            // skip 4 reserved shorts
            this.MetricDataFormat = buffer.ReadInt16(32);
            this.NumberOfVMetrics = buffer.ReadUInt16(34);
        }

        /// <summary>
        /// Gets the table version number.
        /// </summary>
        public Version Version { get; }
        /// <summary>
        /// Distance in FUnits from the centerline to the previous line’s descent.
        /// </summary>
        public Int16 Ascent { get; }
        /// <summary>
        /// Distance in FUnits from the centerline to the next line’s ascent.
        /// </summary>
        public Int16 Descent { get; }
        /// <summary>
        /// Reserved; set to 0
        /// </summary>
        public Int16 LineGap { get; }
        /// <summary>
        /// The maximum advance height measurement 
        /// </summary>
        public Int16 AdvanceHeightMax { get; }
        /// <summary>
        /// The minimum top sidebearing measurement found in the font.
        /// </summary>
        public Int16 MinTopSideBearing { get; }
        /// <summary>
        /// The minimum bottom sidebearing measurement found in the font. 
        /// </summary>
        public Int16 MinBottomSideBearing { get; }
        /// <summary>
        /// Y max extent
        /// </summary>
        public Int16 YMaxExtent { get; }
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
        /// Number of advance heights in the vertical metrics table.
        /// </summary>
        public UInt16 NumberOfVMetrics { get; }
    }
}
