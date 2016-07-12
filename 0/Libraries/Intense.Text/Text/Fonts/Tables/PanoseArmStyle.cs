using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Defines the arm styles.
    /// </summary>
    public enum PanoseArmStyle : byte
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
        /// Straight Arms/Horizontal
        /// </summary>
        StraightArmsHorizontal = 2,
        /// <summary>
        /// Straight Arms/Wedge
        /// </summary>
        StraightArmsWedge = 3,
        /// <summary>
        /// Straight Arms/Vertical
        /// </summary>
        StraightArmsVertical = 4,
        /// <summary>
        /// Straight Arms/Single Serif
        /// </summary>
        StraightArmsSingleSerif = 5,
        /// <summary>
        /// Straight Arms/Double Serif
        /// </summary>
        StraightArmsDoubleSerif = 6,
        /// <summary>
        /// Non-Straight Arms/Horizontal
        /// </summary>
        NonStraightArmsHorizontal = 7,
        /// <summary>
        /// Non-Straight Arms/Wedge
        /// </summary>
        NonStraightArmsWedge = 8,
        /// <summary>
        /// Non-Straight Arms/Vertical
        /// </summary>
        NonStraightArmsVertical = 9,
        /// <summary>
        /// Non-Straight Arms/Single Serif
        /// </summary>
        NonStraightArmsSingleSerif = 10,
        /// <summary>
        /// Non-Straight Arms/Double Serif
        /// </summary>
        NonStraightArmsDoubleSerif = 11
    }
}
