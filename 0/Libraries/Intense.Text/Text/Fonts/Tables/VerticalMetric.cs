using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Represents a vertical metric for a single glyph.
    /// </summary>
    public struct VerticalMetric
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalMetric"/>.
        /// </summary>
        /// <param name="advanceHeight"></param>
        /// <param name="topSideBearing"></param>
        public VerticalMetric(UInt16 advanceHeight, Int16 topSideBearing)
        {
            this.AdvanceHeight = advanceHeight;
            this.TopSideBearing = topSideBearing;
        }

        /// <summary>
        /// Gets the advance height of the glyph (in font design units).
        /// </summary>
        public UInt16 AdvanceHeight { get; }
        /// <summary>
        /// Gets the top side bearing (in font design units).
        /// </summary>
        public Int16 TopSideBearing { get; }
    }
}
