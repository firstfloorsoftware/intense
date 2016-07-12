using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Letter form.
    /// </summary>
    public enum PanoseLetterForm : byte
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
        /// Normal/Contact
        /// </summary>
        NormalContact = 2,
        /// <summary>
        /// Normal/Weighted
        /// </summary>
        NormalWeighted = 3,
        /// <summary>
        /// Normal/Boxed
        /// </summary>
        NormalBoxed = 4,
        /// <summary>
        /// Normal/Flattened
        /// </summary>
        NormalFlattened = 5,
        /// <summary>
        /// Normal/Rounded
        /// </summary>
        NormalRounded = 6,
        /// <summary>
        /// Normal/Off Center
        /// </summary>
        NormalOffCenter = 7,
        /// <summary>
        /// Normal/Square
        /// </summary>
        NormalSquare = 8,
        /// <summary>
        /// Oblique/Contact
        /// </summary>
        ObliqueContact = 9,
        /// <summary>
        /// Oblique/Weighted
        /// </summary>
        ObliqueWeighted = 10,
        /// <summary>
        /// Oblique/Boxed
        /// </summary>
        ObliqueBoxed = 11,
        /// <summary>
        /// Oblique/Flattened
        /// </summary>
        ObliqueFlattened = 12,
        /// <summary>
        /// Oblique/Rounded
        /// </summary>
        ObliqueRounded = 13,
        /// <summary>
        /// Oblique/Off Center
        /// </summary>
        ObliqueOffCenter = 14,
        /// <summary>
        /// Oblique/Square
        /// </summary>
        ObliqueSquare = 15

    }
}
