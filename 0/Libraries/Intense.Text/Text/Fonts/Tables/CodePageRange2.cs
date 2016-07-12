using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Identifies the Code Page Character Range.
    /// </summary>
    [Flags]
    public enum CodePageRange2 : UInt32
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM = 1,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_2 = 2,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_3 = 4,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_4 = 8,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_5 = 16,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_6 = 32,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_7 = 64,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_8 = 128,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_9 = 256,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_10 = 512,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_11 = 1024,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_12 = 2048,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_13 = 4096,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_14 = 8192,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_15 = 16384,
        /// <summary>
        /// Reserved for OEM
        /// </summary>
        ReservedOEM_16 = 32768,
        /// <summary>
        /// IBM Greek
        /// </summary>
        IBMGreek = 65536,
        /// <summary>
        /// MS-DOS Russian
        /// </summary>
        MSDOSRussian = 131072,
        /// <summary>
        ///  MS-DOS Nordic 
        /// </summary>
        MSDOSNordic = 262144,
        /// <summary>
        /// Arabic
        /// </summary>
        Arabic = 524288,
        /// <summary>
        /// MS-DOS Canadian French
        /// </summary>
        MSDOSCanadianFrench = 1048576,
        /// <summary>
        /// Hebrew
        /// </summary>
        Hebrew = 2097152,
        /// <summary>
        ///  MS-DOS Icelandic 
        /// </summary>
        MSDOSIcelandic = 4194304,
        /// <summary>
        /// MS-DOS Portuguese
        /// </summary>
        MSDOSPortuguese = 8388608,
        /// <summary>
        ///  IBM Turkish 
        /// </summary>
        IBMTurkish = 16777216,
        /// <summary>
        /// IBM Cyrillic; primarily Russian
        /// </summary>
        IBMCyrillic = 33554432,
        /// <summary>
        /// Latin 2
        /// </summary>
        Latin2 = 67108864,
        /// <summary>
        /// MS-DOS Baltic
        /// </summary>
        MSDOSBaltic = 134217728,
        /// <summary>
        /// Greek; former 437 G
        /// </summary>
        Greek437G = 268435456,
        /// <summary>
        /// Arabic; ASMO 708
        /// </summary>
        ArabicASMO708 = 536870912,
        /// <summary>
        /// WE/Latin 1
        /// </summary>
        WELatin1 = 1073741824,
        /// <summary>
        /// US
        /// </summary>
        US = 2147483648
    }
}
