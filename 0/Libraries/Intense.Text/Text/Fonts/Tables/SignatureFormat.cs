using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Defines the supported digital signature formats.
    /// </summary>
    public enum SignatureFormat : UInt32
    {
        /// <summary>
        /// PKCS#7 or PKCS#9 signatures with X.509 certificates and counter-signatures.
        /// </summary>
        PKCS7or9 = 1
    }
}
