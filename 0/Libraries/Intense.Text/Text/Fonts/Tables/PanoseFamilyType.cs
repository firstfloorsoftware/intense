using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Family type.
    /// </summary>
    public enum PanoseFamilyType : byte
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
        /// Text and display.
        /// </summary>
        TextAndDisplay = 2,
        /// <summary>
        /// Script.
        /// </summary>
        Script = 3,
        /// <summary>
        /// Decorative
        /// </summary>
        Decorative = 4,
        /// <summary>
        /// Pictorial
        /// </summary>
        Pictorial = 5
    }
}
