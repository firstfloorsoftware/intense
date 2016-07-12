using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Defines the mac style font header flags.
    /// </summary>
    [Flags]
    public enum MacStyle : UInt16
    {
        /// <summary>
        /// No options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Bold.
        /// </summary>
        Bold = 1,
        /// <summary>
        /// Italic.
        /// </summary>
        Italic = 2,
        /// <summary>
        /// Underline.
        /// </summary>
        Underline = 4,
        /// <summary>
        /// Outline.
        /// </summary>
        Outline = 8,
        /// <summary>
        /// Shadow.
        /// </summary>
        Shadow = 16,
        /// <summary>
        /// Condensed.
        /// </summary>
        Condensed = 32,
        /// <summary>
        /// Extended.
        /// </summary>
        Extended = 64
    }
}
