using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Identifies Open Type font types.
    /// </summary>
    public enum FontType
    {
        /// <summary>
        /// Open Type font with True Type outlines.
        /// </summary>
        TrueType,
        /// <summary>
        /// Open Type font collection consisting of multiple Open Type fonts with True Type outlines.
        /// </summary>
        TrueTypeCollection,
        /// <summary>
        /// Open Type font with PostScript outlines (Type 1 Compact Font Format, CFF).
        /// </summary>
        PostScript
    }
}
