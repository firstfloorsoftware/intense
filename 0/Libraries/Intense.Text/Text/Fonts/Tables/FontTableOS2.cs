using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// The OS/2 table consists of a set of metrics that are required in OpenType fonts.
    /// </summary>
    public class FontTableOS2
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableOS2"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableOS2(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadUInt16(0);

            // verify table length (depends on version)
            var length = GetExpectedTableLength();
            VerifyTableLength(length);

            // version 0
            this.XAvgCharWidth = buffer.ReadInt16(2);
            this.WeightClass = buffer.ReadUInt16(4);
            this.WidthClass = buffer.ReadUInt16(6);
            this.Type = (FontEmbeddingRight)buffer.ReadUInt16(8);
            this.YSubscriptXSize = buffer.ReadInt16(10);
            this.YSubscriptYSize = buffer.ReadInt16(12);
            this.YSubscriptXOffset = buffer.ReadInt16(14);
            this.YSubscriptYOffset = buffer.ReadInt16(16);
            this.YSuperscriptXSize = buffer.ReadInt16(18);
            this.YSuperscriptYSize = buffer.ReadInt16(20);
            this.YSuperscriptXOffset = buffer.ReadInt16(22);
            this.YSuperscriptYOffset = buffer.ReadInt16(24);
            this.YStrikoutSize = buffer.ReadInt16(26);
            this.YStrikoutPosition = buffer.ReadInt16(28);
            this.FamilyClass = (IBMFontClass)buffer.ReadInt16(30);
            this.Panose = new Panose(buffer, 32);
            this.UnicodeRange1 = (UnicodeRange1)buffer.ReadUInt32(42);
            this.UnicodeRange2 = (UnicodeRange2)buffer.ReadUInt32(46);
            this.UnicodeRange3 = (UnicodeRange3)buffer.ReadUInt32(50);
            this.UnicodeRange4 = (UnicodeRange4)buffer.ReadUInt32(54);
            this.VendID = buffer.ReadString(58, 4, Encoding.ASCII).Trim();
            this.Selection = (FontSelection)buffer.ReadUInt16(62);
            this.FirstCharIndex = buffer.ReadUInt16(64);
            this.LastCharIndex = buffer.ReadUInt16(66);
            this.TypoAscender = buffer.ReadInt16(68);
            this.TypoDescender = buffer.ReadInt16(70);
            this.TypoLineGap = buffer.ReadInt16(72);
            this.WinAscent = buffer.ReadUInt16(74);
            this.WinDescent = buffer.ReadUInt16(76);

            if (this.Version > 0) {
                // version 1
                this.CodePageRange1 = (CodePageRange1)buffer.ReadUInt32(78);
                this.CodePageRange2 = (CodePageRange2)buffer.ReadUInt32(82);
            }
            if (this.Version > 1) {
                // version 2, 3 and 4
                this.XHeight = buffer.ReadInt16(86);
                this.CapHeight = buffer.ReadInt16(88);
                this.DefaultChar = buffer.ReadUInt16(90);
                this.BreakChar = buffer.ReadUInt16(92);
                this.MaxContext = buffer.ReadUInt16(94);
            }
            if (this.Version > 4) {
                // version 5
                this.LowerOpticalPointSize = buffer.ReadUInt16(96);
                this.UpperOpticalPointSize = buffer.ReadUInt16(98);
            }
        }

        private int GetExpectedTableLength()
        {
            if (this.Version == 0) {
                return 78;
            }
            if (this.Version == 1) {
                return 86;
            }
            if (this.Version == 2 || this.Version == 3 || this.Version == 4) {
                return 96;
            }
            if (this.Version == 5) {
                return 100;
            }

            // should not happen
            throw new NotSupportedException("UnknownOS2TableVersion");
        }

        /// <summary>
        /// Gets the table version number.
        /// </summary>
        public UInt16 Version { get; }
        /// <summary>
        /// Average weighted escapement.
        /// </summary>
        public Int16 XAvgCharWidth { get; }
        /// <summary>
        /// Weight class.
        /// </summary>
        public UInt16 WeightClass { get; }
        /// <summary>
        /// Width class.
        /// </summary>
        public UInt16 WidthClass { get; }
        /// <summary>
        /// Indicates font embedding licensing rights for the font.
        /// </summary>
        public FontEmbeddingRight Type { get; }
        /// <summary>
        /// Subscript horizontal font size.
        /// </summary>
        public Int16 YSubscriptXSize { get; }
        /// <summary>
        ///  Subscript vertical font size. 
        /// </summary>
        public Int16 YSubscriptYSize { get; }
        /// <summary>
        /// Subscript x offset.
        /// </summary>
        public Int16 YSubscriptXOffset { get; }
        /// <summary>
        ///  Subscript y offset. 
        /// </summary>
        public Int16 YSubscriptYOffset { get; }
        /// <summary>
        /// Superscript horizontal font size.
        /// </summary>
        public Int16 YSuperscriptXSize { get; }
        /// <summary>
        /// Superscript vertical font size.
        /// </summary>
        public Int16 YSuperscriptYSize { get; }
        /// <summary>
        /// Superscript x offset.
        /// </summary>
        public Int16 YSuperscriptXOffset { get; }
        /// <summary>
        /// Superscript y offset.
        /// </summary>
        public Int16 YSuperscriptYOffset { get; }
        /// <summary>
        /// Strikeout size.
        /// </summary>
        public Int16 YStrikoutSize { get; }
        /// <summary>
        /// Strikeout position.
        /// </summary>
        public Int16 YStrikoutPosition { get; }
        /// <summary>
        /// Font-family class and subclass.
        /// </summary>
        public IBMFontClass FamilyClass { get; }
        /// <summary>
        /// PANOSE classification.
        /// </summary>
        public Panose Panose { get; }
        /// <summary>
        /// Unicode Character Range 
        /// </summary>
        public UnicodeRange1 UnicodeRange1 { get; }
        /// <summary>
        /// Unicode Character Range 
        /// </summary>
        public UnicodeRange2 UnicodeRange2 { get; }
        /// <summary>
        /// Unicode Character Range 
        /// </summary>
        public UnicodeRange3 UnicodeRange3 { get; }
        /// <summary>
        /// Unicode Character Range 
        /// </summary>
        public UnicodeRange4 UnicodeRange4 { get; }
        /// <summary>
        /// Font Vendor Identification
        /// </summary>
        public string VendID { get; }
        /// <summary>
        ///  Font selection flags. 
        /// </summary>
        public FontSelection Selection { get; }
        /// <summary>
        /// The minimum Unicode index (character code) in this font.
        /// </summary>
        public UInt16 FirstCharIndex { get; }
        /// <summary>
        /// The maximum Unicode index (character code) in this font.
        /// </summary>
        public UInt16 LastCharIndex { get; }
        /// <summary>
        /// The typographic ascender for this font. 
        /// </summary>
        public Int16 TypoAscender { get; }
        /// <summary>
        /// The typographic descender for this font.
        /// </summary>
        public Int16 TypoDescender { get; }
        /// <summary>
        /// The typographic line gap for this font. 
        /// </summary>
        public Int16 TypoLineGap { get; }
        /// <summary>
        /// The ascender metric for Windows.
        /// </summary>
        public UInt16 WinAscent { get; }
        /// <summary>
        /// The descender metric for Windows.
        /// </summary>
        public UInt16 WinDescent { get; }
        /// <summary>
        /// Code Page Character Range
        /// </summary>
        public CodePageRange1 CodePageRange1 { get; }
        /// <summary>
        /// Code Page Character Range
        /// </summary>
        public CodePageRange2 CodePageRange2 { get; }
        /// <summary>
        /// This metric specifies the distance between the baseline and the approximate height of non-ascending lowercase letters measured in FUnits
        /// </summary>
        public Int16 XHeight { get; }
        /// <summary>
        /// This metric specifies the distance between the baseline and the approximate height of uppercase letters measured in FUnits
        /// </summary>
        public Int16 CapHeight { get; }
        /// <summary>
        /// Whenever a request is made for a character that is not in the font, Windows provides this default character.
        /// </summary>
        public UInt16 DefaultChar { get; }
        /// <summary>
        /// This is the Unicode encoding of the glyph that Windows uses as the break character.
        /// </summary>
        public UInt16 BreakChar { get; }
        /// <summary>
        /// The maximum length of a target glyph context for any feature in this font. 
        /// </summary>
        public UInt16 MaxContext { get; }
        /// <summary>
        /// This value is the lower value of the size range for which this font has been designed
        /// </summary>
        public UInt16 LowerOpticalPointSize { get; }
        /// <summary>
        /// This value is the upper value of the size range for which this font has been designed
        /// </summary>
        public UInt16 UpperOpticalPointSize { get; }
    }
}
