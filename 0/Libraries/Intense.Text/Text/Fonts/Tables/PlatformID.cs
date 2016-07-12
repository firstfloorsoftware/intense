using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Identifies the platform ids.
    /// </summary>
    public enum PlatformID : UInt16
    {
        /// <summary>
        /// Unicode
        /// </summary>
        Unicode = 0,
        /// <summary>
        /// Macintosh
        /// </summary>
        Macintosh = 1,
        /// <summary>
        /// ISO
        /// </summary>
        ISO = 2,
        /// <summary>
        /// Windows
        /// </summary>
        Windows = 3,
        /// <summary>
        /// Custom
        /// </summary>
        Custom = 4
    }
}
