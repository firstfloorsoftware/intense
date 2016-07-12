using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Defines the font header flags.
    /// </summary>
    [Flags]
    public enum HeaderFlags : UInt16
    {
        /// <summary>
        /// No options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Baseline for font at y=0.
        /// </summary>
        BaseLine = 1,
        /// <summary>
        /// Left sidebearing point at x=0.
        /// </summary>
        LeftSidebearingPoint = 2,
        /// <summary>
        /// Instructions may depend on point size.
        /// </summary>
        PointSizeInstructions = 4,
        /// <summary>
        /// Force ppem to integer values for all internal scaler math.
        /// </summary>
        ForcePpemToInteger = 8,
        /// <summary>
        /// Instructions may alter advance width.
        /// </summary>
        AdvanceWidthInstructions = 16,
        /// <summary>
        /// Font data is ‘lossless’ as a results of having been subjected to optimizing transformation and/or compression.
        /// </summary>
        Lossless = 2048,
        /// <summary>
        /// Font converted (produce compatible metrics).
        /// </summary>
        Converted = 4096,
        /// <summary>
        /// Font optimized for ClearType™. 
        /// </summary>
        OptimizedForClearType = 8192,
        /// <summary>
        /// Last Resort font.
        /// </summary>
        LastResortFont = 16384,
    }
}
