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
    public enum UnicodeRange2 : UInt32
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Superscripts And Subscripts
        /// </summary>
        SuperscriptsAndSubscripts = 1,
        /// <summary>
        /// Currency Symbols
        /// </summary>
        CurrencySymbols = 2,
        /// <summary>
        /// Combining Diacritical Marks For Symbols
        /// </summary>
        CombiningDiacriticalMarksForSymbols = 4,
        /// <summary>
        /// Letterlike Symbols
        /// </summary>
        LetterlikeSymbols = 8,
        /// <summary>
        /// Number Forms
        /// </summary>
        NumberForms = 16,
        /// <summary>
        /// Arrows
        /// </summary>
        Arrows = 32,
        /// <summary>
        /// Mathematical Operators
        /// </summary>
        MathematicalOperators = 64,
        /// <summary>
        /// Miscellaneous Technical
        /// </summary>
        MiscellaneousTechnical = 128,
        /// <summary>
        /// Control Pictures
        /// </summary>
        ControlPictures = 256,
        /// <summary>
        /// Optical Character Recognition
        /// </summary>
        OpticalCharacterRecognition = 512,
        /// <summary>
        /// Enclosed Alphanumerics
        /// </summary>
        EnclosedAlphanumerics = 1024,
        /// <summary>
        /// Box Drawing
        /// </summary>
        BoxDrawing = 2048,
        /// <summary>
        /// Block Elements
        /// </summary>
        BlockElements = 4096,
        /// <summary>
        /// Geometric Shapes
        /// </summary>
        GeometricShapes = 8192,
        /// <summary>
        /// Miscellaneous Symbols
        /// </summary>
        MiscellaneousSymbols = 16384,
        /// <summary>
        /// Dingbats
        /// </summary>
        Dingbats = 32768,
        /// <summary>
        /// CJK Symbols And Punctuation
        /// </summary>
        CJKSymbolsAndPunctuation = 65536,
        /// <summary>
        /// Hiragana
        /// </summary>
        Hiragana = 131072,
        /// <summary>
        /// Katakana
        /// </summary>
        Katakana = 262144,
        /// <summary>
        /// Bopomofo
        /// </summary>
        Bopomofo = 524288,
        /// <summary>
        /// Hangul Compatibility Jamo
        /// </summary>
        HangulCompatibilityJamo = 1048576,
        /// <summary>
        /// Phags-pa
        /// </summary>
        PhagsPa = 2097152,
        /// <summary>
        /// Enclosed CJK Letters And Months
        /// </summary>
        EnclosedCJKLettersAndMonths = 4194304,
        /// <summary>
        /// CJK Compatibility
        /// </summary>
        CJKCompatibility = 8388608,
        /// <summary>
        /// Hangul Syllables
        /// </summary>
        HangulSyllables = 16777216,
        /// <summary>
        /// Non-Plane 0
        /// </summary>
        NonPlane0 = 33554432,
        /// <summary>
        /// Phoenician
        /// </summary>
        Phoenician = 67108864,
        /// <summary>
        /// CJK Unified Ideographs
        /// </summary>
        CJKUnifiedIdeographs = 134217728,
        /// <summary>
        /// Private Use Area (plane 0)
        /// </summary>
        PrivateUseArea = 268435456,
        /// <summary>
        /// CJK Strokes
        /// </summary>
        CJKStrokes = 536870912,
        /// <summary>
        /// Alphabetic Presentation Forms
        /// </summary>
        AlphabeticPresentationForms = 1073741824,
        /// <summary>
        /// Arabic Presentation Forms-A
        /// </summary>
        ArabicPresentationFormsA = 2147483648

    }
}
