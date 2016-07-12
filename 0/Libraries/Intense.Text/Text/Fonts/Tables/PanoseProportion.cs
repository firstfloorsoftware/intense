using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Proportion
    /// </summary>
    public enum PanoseProportion : byte
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
        /// Old style.
        /// </summary>
        OldStyle = 2,
        /// <summary>
        /// Modern.
        /// </summary>
        Modern = 3,
        /// <summary>
        /// Even width.
        /// </summary>
        EvenWidth = 4,
        /// <summary>
        /// Expanded.
        /// </summary>
        Expanded = 5,
        /// <summary>
        /// Condensed.
        /// </summary>
        Condensed = 6,
        /// <summary>
        /// Very expanded.
        /// </summary>
        VeryExpanded = 7,
        /// <summary>
        /// Very condensed.
        /// </summary>
        VeryCondensed = 8,
        /// <summary>
        /// Monospaced.
        /// </summary>
        Monospaced = 9
    }
}
