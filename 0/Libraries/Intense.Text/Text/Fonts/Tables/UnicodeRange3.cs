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
    public enum UnicodeRange3 : UInt32
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Combining Half Marks
        /// </summary>
        CombiningHalfMarks = 1,
        /// <summary>
        /// Vertical Forms
        /// </summary>
        VerticalForms = 2,
        /// <summary>
        /// Small Form Variants
        /// </summary>
        SmallFormVariants = 4,
        /// <summary>
        /// Arabic Presentation Forms-B
        /// </summary>
        ArabicPresentationFormsB = 8,
        /// <summary>
        /// Halfwidth And Fullwidth Forms
        /// </summary>
        HalfwidthAndFullwidthForms = 16,
        /// <summary>
        /// Specials
        /// </summary>
        Specials = 32,
        /// <summary>
        /// Tibetan
        /// </summary>
        Tibetan = 64,
        /// <summary>
        /// Syriac
        /// </summary>
        Syriac = 128,
        /// <summary>
        /// Thaana
        /// </summary>
        Thaana = 256,
        /// <summary>
        /// Sinhala
        /// </summary>
        Sinhala = 512,
        /// <summary>
        /// Myanmar
        /// </summary>
        Myanmar = 1024,
        /// <summary>
        /// Ethiopic
        /// </summary>
        Ethiopic = 2048,
        /// <summary>
        /// Cherokee
        /// </summary>
        Cherokee = 4096,
        /// <summary>
        /// Unified Canadian Aboriginal Syllabics
        /// </summary>
        UnifiedCanadianAboriginalSyllabics = 8192,
        /// <summary>
        /// Ogham
        /// </summary>
        Ogham = 16384,
        /// <summary>
        /// Runic
        /// </summary>
        Runic = 32768,
        /// <summary>
        /// Khmer
        /// </summary>
        Khmer = 65536,
        /// <summary>
        /// Mongolian
        /// </summary>
        Mongolian = 131072,
        /// <summary>
        /// Braille Patterns
        /// </summary>
        BraillePatterns = 262144,
        /// <summary>
        /// Yi Syllables
        /// </summary>
        YiSyllables = 524288,
        /// <summary>
        /// Tagalog
        /// </summary>
        Tagalog = 1048576,
        /// <summary>
        /// Old Italic
        /// </summary>
        OldItalic = 2097152,
        /// <summary>
        /// Gothic
        /// </summary>
        Gothic = 4194304,
        /// <summary>
        /// Deseret
        /// </summary>
        Deseret = 8388608,
        /// <summary>
        /// Byzantine Musical Symbols
        /// </summary>
        ByzantineMusicalSymbols = 16777216,
        /// <summary>
        /// Mathematical Alphanumeric Symbols
        /// </summary>
        MathematicalAlphanumericSymbols = 33554432,
        /// <summary>
        /// Private Use (plane 15)
        /// </summary>
        PrivateUse = 67108864,
        /// <summary>
        /// Variation Selectors
        /// </summary>
        VariationSelectors = 134217728,
        /// <summary>
        /// Limbu
        /// </summary>
        Limbu = 536870912,
        /// <summary>
        /// Tai Le
        /// </summary>
        TaiLe = 1073741824,
        /// <summary>
        /// New Tai Lue
        /// </summary>
        NewTaiLue = 2147483648,
    }
}
