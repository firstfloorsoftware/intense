using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Contrast
    /// </summary>
    public enum PanoseContrast : byte
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
        /// None.
        /// </summary>
        None = 2,
        /// <summary>
        /// Very low.
        /// </summary>
        VeryLow = 3,
        /// <summary>
        /// Low.
        /// </summary>
        Low = 4,
        /// <summary>
        /// Medium low.
        /// </summary>
        MediumLow = 5,
        /// <summary>
        /// Medium.
        /// </summary>
        Medium = 6,
        /// <summary>
        /// Medium high.
        /// </summary>
        MediumHigh = 7,
        /// <summary>
        /// High.
        /// </summary>
        High = 8,
        /// <summary>
        /// Very high.
        /// </summary>
        VeryHigh = 9
    }
}
