using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Defines the nature of the font patterns
    /// </summary>
    [Flags]
    public enum FontSelection : UInt16
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0,
        /// <summary>
        /// Font contains italic or oblique characters, otherwise they are upright.
        /// </summary>
        Italic = 1,
        /// <summary>
        /// Characters are underscored 
        /// </summary>
        Underscore = 2,
        /// <summary>
        /// Characters have their foreground and background reversed.
        /// </summary>
        Negative = 4,
        /// <summary>
        /// Outline (hollow) characters, otherwise they are solid.
        /// </summary>
        Outlined = 8,
        /// <summary>
        /// Characters are overstruck.
        /// </summary>
        Strikeout = 16,
        /// <summary>
        /// Characters are emboldened.
        /// </summary>
        Bold = 32,
        /// <summary>
        /// Characters are in the standard weight/style for the font.
        /// </summary>
        Regular = 64,
        /// <summary>
        /// Defines how default line spacing is calculated.
        /// </summary>
        UseTypoMetrics = 128,
        /// <summary>
        /// Specifies the font has names consistent with a weight/width/slope family.
        /// </summary>
        WWS = 256,
        /// <summary>
        /// Font contains oblique characters.
        /// </summary>
        Oblique = 512
    }
}
