using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Identifies the Unicode Character Range.
    /// </summary>
    [Flags]
    public enum UnicodeRange1 : UInt32
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Basic Latin
        /// </summary>
        BasicLatin = 1,
        /// <summary>
        /// Latin-1 Supplement 
        /// </summary>
        Latin1Supplement = 2,
        /// <summary>
        /// Latin Extended-A
        /// </summary>
        LatinExtendedA = 4,
        /// <summary>
        /// Latin Extended-B
        /// </summary>
        LatinExtendedB = 8,
        /// <summary>
        /// IPA Extensions
        /// </summary>
        IPAExtensions = 16,
        /// <summary>
        /// Spacing Modifier Letters
        /// </summary>
        SpacingModifierLetters = 32,
        /// <summary>
        /// Combining Diacritical Marks
        /// </summary>
        CombiningDiacriticalMarks = 64,
        /// <summary>
        /// Greek and Coptic
        /// </summary>
        GreekAndCoptic = 128,
        /// <summary>
        /// Coptic
        /// </summary>
        Coptic = 256,
        /// <summary>
        /// Cyrillic
        /// </summary>
        Cyrillic = 512,
        /// <summary>
        /// Armenian
        /// </summary>
        Armenian = 1024,
        /// <summary>
        /// Hebrew
        /// </summary>
        Hebrew = 2048,
        /// <summary>
        /// Vai
        /// </summary>
        Vai = 4096,
        /// <summary>
        /// Arabic
        /// </summary>
        Arabic = 8192,
        /// <summary>
        /// NKo
        /// </summary>
        NKo = 16384,
        /// <summary>
        /// Devanagari
        /// </summary>
        Devanagari = 32768,
        /// <summary>
        /// Bengali
        /// </summary>
        Bengali = 65536,
        /// <summary>
        /// Gurmukhi
        /// </summary>
        Gurmukhi = 131072,
        /// <summary>
        /// Gujarati
        /// </summary>
        Gujarati = 262144,
        /// <summary>
        /// Oriya
        /// </summary>
        Oriya = 524288,
        /// <summary>
        /// Tamil
        /// </summary>
        Tamil = 1048576,
        /// <summary>
        /// Telugu
        /// </summary>
        Telugu = 2097152,
        /// <summary>
        /// Kannada
        /// </summary>
        Kannada = 4194304,
        /// <summary>
        /// Malayalam
        /// </summary>
        Malayalam = 8388608,
        /// <summary>
        /// Thai
        /// </summary>
        Thai = 16777216,
        /// <summary>
        /// Lao
        /// </summary>
        Lao = 33554432,
        /// <summary>
        ///  Georgian 
        /// </summary>
        Georgian = 67108864,
        /// <summary>
        ///  Balinese 
        /// </summary>
        Balinese = 134217728,
        /// <summary>
        ///  Hangul Jamo 
        /// </summary>
        HangulJamo = 268435456,
        /// <summary>
        /// Latin Extended Additional
        /// </summary>
        LatinExtendedAdditional = 536870912,
        /// <summary>
        /// Greek Extended
        /// </summary>
        GreekExtended = 1073741824,
        /// <summary>
        /// General Punctuation
        /// </summary>
        GeneralPunctuation = 2147483648
    }
}
