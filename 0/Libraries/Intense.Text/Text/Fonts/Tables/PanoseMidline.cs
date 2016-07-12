using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Mid line.
    /// </summary>
    public enum PanoseMidline : byte
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any = 0,
        /// <summary>
        /// No Fit
        /// </summary>
        NoFit = 1,
        /// <summary>
        /// /Standard/Trimmed
        /// </summary>
        StandardTrimmed = 2,
        /// <summary>
        /// Standard/Pointed
        /// </summary>
        StandardPointed = 3,
        /// <summary>
        /// Standard/Serifed
        /// </summary>
        StandardSerifed = 4,
        /// <summary>
        /// High/Trimmed
        /// </summary>
        HighTrimmed = 5,
        /// <summary>
        /// High/Pointed
        /// </summary>
        HighPointed = 6,
        /// <summary>
        /// High/Serifed
        /// </summary>
        HighSerifed = 7,
        /// <summary>
        /// Constant/Trimmed
        /// </summary>
        ConstantTrimmed = 8,
        /// <summary>
        /// /Constant/Pointed
        /// </summary>
        ConstantPointed = 9,
        /// <summary>
        /// Constant/Serifed
        /// </summary>
        ConstantSerifed = 10,
        /// <summary>
        /// Low/Trimmed
        /// </summary>
        LowTrimmed = 11,
        /// <summary>
        /// Low/Pointed
        /// </summary>
        LowPointed = 12,
        /// <summary>
        /// Low/Serifed
        /// </summary>
        LowSerifed = 13,
    }
}
