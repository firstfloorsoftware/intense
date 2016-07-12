using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Stroke variation
    /// </summary>
    public enum PanoseStrokeVariation : byte
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
        /// Gradual/Diagonal
        /// </summary>
        GradualDiagonal = 2,
        /// <summary>
        /// Gradual/Transitional
        /// </summary>
        GradualTransitional = 3,
        /// <summary>
        /// Gradual/Vertical
        /// </summary>
        GradualVertical = 4,
        /// <summary>
        /// Gradual/Horizontal
        /// </summary>
        GradualHorizontal = 5,
        /// <summary>
        /// Rapid/Vertical
        /// </summary>
        RapidVertical = 6,
        /// <summary>
        /// Rapid/Horizontal
        /// </summary>
        RapidHorizontal = 7,
        /// <summary>
        /// Instant/Vertical
        /// </summary>
        InstantVertical = 8
    }
}
