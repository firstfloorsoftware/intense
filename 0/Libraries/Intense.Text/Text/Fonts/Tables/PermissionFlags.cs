using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Defines the permission flags of digital signatures
    /// </summary>
    [Flags]
    public enum PermissionFlags : UInt16
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0,
        /// <summary>
        /// The font cannot be resigned.
        /// </summary>
        CannotBeResigned = 1
    }
}
