using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Defines a ratio range.
    /// </summary>
    public struct RatioRange
    {
        internal RatioRange(byte[] buffer, int index)
        {
            this.CharSet = buffer[index];
            this.XRatio = buffer[index + 1];
            this.YStartRatio = buffer[index + 2];
            this.YEndRatio = buffer[index + 3];

            // TODO: add VDMX grouping
        }

        /// <summary>
        /// Character set 
        /// </summary>
        public byte CharSet { get; }
        /// <summary>
        /// Value to use for x-Ratio
        /// </summary>
        public byte XRatio { get; }
        /// <summary>
        /// Starting y-Ratio value.
        /// </summary>
        public byte YStartRatio { get; }
        /// <summary>
        /// Ending y-Ratio value.
        /// </summary>
        public byte YEndRatio { get; }
    }
}
