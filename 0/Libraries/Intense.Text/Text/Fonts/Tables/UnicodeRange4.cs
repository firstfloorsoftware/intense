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
    public enum UnicodeRange4 : UInt32
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Buginese
        /// </summary>
        Buginese = 1,
        /// <summary>
        /// Glagolitic
        /// </summary>
        Glagolitic = 2,
        /// <summary>
        /// Tifinagh
        /// </summary>
        Tifinagh = 4,
        /// <summary>
        /// Yijing Hexagram Symbols
        /// </summary>
        YijingHexagramSymbols = 8,
        /// <summary>
        /// Syloti Nagri
        /// </summary>
        SylotiNagri = 16,
        /// <summary>
        /// Linear B Syllabary
        /// </summary>
        LinearBSyllabary = 32,
        /// <summary>
        /// Ancient Greek Numbers 
        /// </summary>
        AncientGreekNumbers = 64,
        /// <summary>
        /// Ugaritic
        /// </summary>
        Ugaritic = 128,
        /// <summary>
        /// Old Persian
        /// </summary>
        OldPersian = 256,
        /// <summary>
        /// Shavian
        /// </summary>
        Shavian = 512,
        /// <summary>
        /// Osmanya
        /// </summary>
        Osmanya = 1024,
        /// <summary>
        /// Cypriot Syllabary
        /// </summary>
        CypriotSyllabary = 2048,
        /// <summary>
        /// Kharoshthi
        /// </summary>
        Kharoshthi = 4096,
        /// <summary>
        /// Tai Xuan Jing Symbols 
        /// </summary>
        TaiXuanJingSymbols = 8192,
        /// <summary>
        /// Cuneiform
        /// </summary>
        Cuneiform = 16384,
        /// <summary>
        /// Counting Rod Numerals
        /// </summary>
        CountingRodNumerals = 32768,
        /// <summary>
        /// Sundanese
        /// </summary>
        Sundanese = 65536,
        /// <summary>
        /// Lepcha
        /// </summary>
        Lepcha = 131072,
        /// <summary>
        /// Ol Chiki
        /// </summary>
        OlChiki = 262144,
        /// <summary>
        /// Saurashtra
        /// </summary>
        Saurashtra = 524288,
        /// <summary>
        /// Kayah Li 
        /// </summary>
        KayahLi = 1048576,
        /// <summary>
        /// Rejang
        /// </summary>
        Rejang = 2097152,
        /// <summary>
        /// Cham
        /// </summary>
        Cham = 4194304,
        /// <summary>
        /// Ancient Symbols
        /// </summary>
        AncientSymbols = 8388608,
        /// <summary>
        /// Phaistos Disc 
        /// </summary>
        PhaistosDisc = 16777216,
        /// <summary>
        /// Carian
        /// </summary>
        Carian = 33554432,
        /// <summary>
        /// Domino Tiles
        /// </summary>
        DominoTiles = 67108864
    }
}
