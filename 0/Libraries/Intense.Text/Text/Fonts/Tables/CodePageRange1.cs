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
    public enum CodePageRange1 : UInt32
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Latin 1
        /// </summary>
        Latin1 = 1,
        /// <summary>
        /// Latin 2: Eastern Europe 
        /// </summary>
        Latin2 = 2,
        /// <summary>
        /// Cyrillic
        /// </summary>
        Cyrillic = 4,
        /// <summary>
        /// Greek
        /// </summary>
        Greek = 8,
        /// <summary>
        /// Turkish
        /// </summary>
        Turkish = 16,
        /// <summary>
        /// Hebrew
        /// </summary>
        Hebrew = 32,
        /// <summary>
        /// Arabic
        /// </summary>
        Arabic = 64,
        /// <summary>
        /// Windows Baltic
        /// </summary>
        WindowsBaltic = 128,
        /// <summary>
        /// Vietnamese
        /// </summary>
        Vietnamese = 256,
        /// <summary>
        /// Reserved for Alternate ANSI
        /// </summary>
        ReservedAlternateANSI = 512,
        /// <summary>
        /// Reserved for Alternate ANSI
        /// </summary>
        ReservedAlternateANSI_2 = 1024,
        /// <summary>
        /// Reserved for Alternate ANSI
        /// </summary>
        ReservedAlternateANSI_3 = 2048,
        /// <summary>
        /// Reserved for Alternate ANSI
        /// </summary>
        ReservedAlternateANSI_4 = 4096,
        /// <summary>
        /// Reserved for Alternate ANSI
        /// </summary>
        ReservedAlternateANSI_5 = 8192,
        /// <summary>
        /// Reserved for Alternate ANSI
        /// </summary>
        ReservedAlternateANSI_6 = 16384,
        /// <summary>
        /// Reserved for Alternate ANSI
        /// </summary>
        ReservedAlternateANSI_7 = 32768,
        /// <summary>
        /// Thai
        /// </summary>
        Thai = 65536,
        /// <summary>
        ///  JIS/Japan 
        /// </summary>
        Japan = 131072,
        /// <summary>
        ///  Chinese: Simplified chars--PRC and Singapore 
        /// </summary>
        ChineseSimplified = 262144,
        /// <summary>
        /// Korean Wansung
        /// </summary>
        KoreanWansung = 524288,
        /// <summary>
        ///  Chinese: Traditional chars--Taiwan and Hong Kong 
        /// </summary>
        ChineseTraditional = 1048576,
        /// <summary>
        ///  Korean Johab 
        /// </summary>
        KoreanJohab = 2097152,
        /// <summary>
        /// Reserved for Alternate ANSI &amp; OEM
        /// </summary>
        ReservedAlternateANSIOEM = 4194304,
        /// <summary>
        /// Reserved for Alternate ANSI &amp; OEM
        /// </summary>
        ReservedAlternateANSIOEM_2 = 8388608,
        /// <summary>
        /// Reserved for Alternate ANSI &amp; OEM
        /// </summary>
        ReservedAlternateANSIOEM_3 = 16777216,
        /// <summary>
        /// Reserved for Alternate ANSI &amp; OEM
        /// </summary>
        ReservedAlternateANSIOEM_4 = 33554432,
        /// <summary>
        /// Reserved for Alternate ANSI &amp; OEM
        /// </summary>
        ReservedAlternateANSIOEM_5 = 67108864,
        /// <summary>
        /// Reserved for Alternate ANSI &amp; OEM
        /// </summary>
        ReservedAlternateANSIOEM_6 = 134217728,
        /// <summary>
        /// Reserved for Alternate ANSI &amp; OEM
        /// </summary>
        ReservedAlternateANSIOEM_7 = 268435456,
        /// <summary>
        /// Macintosh Character Set (US Roman)
        /// </summary>
        Macintosh = 536870912,
        /// <summary>
        /// OEM Character Set
        /// </summary>
        OEM = 1073741824,
        /// <summary>
        /// Symbol Character Set
        /// </summary>
        Symbol = 2147483648,
    }
}
