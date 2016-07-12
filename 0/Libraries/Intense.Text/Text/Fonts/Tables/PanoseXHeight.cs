using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// X height
    /// </summary>
    public enum PanoseXHeight : byte
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any = 0,
        /// <summary>
        /// No fit.
        /// </summary>
        NoFit = 1,
        /// <summary>
        /// Constant/Small
        /// </summary>
        ConstantSmall = 2,
        /// <summary>
        /// Constant/Standard
        /// </summary>
        ConstantStandard = 3,
        /// <summary>
        /// Constant/Large
        /// </summary>
        ConstantLarge = 4,
        /// <summary>
        /// Ducking/Small
        /// </summary>
        DuckingSmall = 5,
        /// <summary>
        /// Ducking/Standard
        /// </summary>
        DuckingStandard = 6,
        /// <summary>
        /// Ducking/Large
        /// </summary>
        DuckingLarge = 7
    }
}
