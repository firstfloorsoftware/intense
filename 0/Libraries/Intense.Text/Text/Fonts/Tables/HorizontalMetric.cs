using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Represents a horizontal metric for a single glyph.
    /// </summary>
    public struct HorizontalMetric
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalMetric"/>.
        /// </summary>
        /// <param name="advanceWidth"></param>
        /// <param name="leftSideBearing"></param>
        public HorizontalMetric(UInt16 advanceWidth, Int16 leftSideBearing)
        {
            this.AdvanceWidth = advanceWidth;
            this.LeftSideBearing = leftSideBearing;
        }

        /// <summary>
        /// Gets the advance width (in font design units).
        /// </summary>
        public UInt16 AdvanceWidth { get; }
        /// <summary>
        /// Gets the left side bearing (in font design units).
        /// </summary>
        public Int16 LeftSideBearing { get; }
    }
}
