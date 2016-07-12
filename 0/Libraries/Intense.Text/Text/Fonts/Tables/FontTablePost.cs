using Intense.Text.Fonts.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Contains additional information needed to use TrueType or OpenType™ fonts on PostScript printers
    /// </summary>
    public class FontTablePost
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTablePost"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTablePost(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadFixedVersion(0);
            this.ItalicAngle = buffer.ReadFixedFloat(4);
            this.UnderlinePosition = buffer.ReadInt16(8);
            this.UnderlineThickness = buffer.ReadInt16(10);
            this.IsFixedPitch = buffer.ReadUInt32(12);
            this.MinMemType42 = buffer.ReadUInt32(16);
            this.MaxMemType42 = buffer.ReadUInt32(20);
            this.MinMemType1 = buffer.ReadUInt32(24);
            this.MaxMemType1 = buffer.ReadUInt32(28);

            if (this.Version.Major == 1 && this.Version.Minor == 0) {
                VerifyTableLength(32);
            }
            if (this.Version.Major == 2 && this.Version.Minor == 0) {
                this.NumberOfGlyphs = buffer.ReadUInt16(32);

                var indices = ImmutableArray.CreateBuilder<UInt16>();
                for (var i = 0; i < this.NumberOfGlyphs; i++) {
                    indices.Add(buffer.ReadUInt16(34 + i * 2));
                }
                this.GlyphNameIndices = indices.ToImmutable();

                var offset = this.NumberOfGlyphs * 2 + 34;
                var postNames = new List<string>();
                while (offset < buffer.Length) {
                    // read pascal strings
                    int length;
                    var name = buffer.ReadPascalString(offset, out length);
                    postNames.Add(name);
                    offset += length;
                }

                var names = ImmutableArray.CreateBuilder<string>();
                for (var i = 0; i < this.NumberOfGlyphs; i++) {
                    var index = indices[i];
                    if (index < MacintoshGlyphNames.Count) {
                        names.Add(MacintoshGlyphNames.Get(index));
                    }
                    else {
                        index -= MacintoshGlyphNames.Count;
                        names.Add(postNames[index]); 
                    }
                }
                this.GlyphNames = names.ToImmutable();
            }
            else if (this.Version.Major == 2 && this.Version.Minor == 5) {
                this.NumberOfGlyphs = buffer.ReadUInt16(32);

                // deprecated, not implemented
            }
            else if (this.Version.Major == 3 && this.Version.Minor == 0) {
                VerifyTableLength(32);
            }
        }

        /// <summary>
        /// Gets the table version number.
        /// </summary>
        public Version Version { get; }
        /// <summary>
        /// Italic angle in counter-clockwise degrees from the vertical.
        /// </summary>
        public float ItalicAngle { get; }
        /// <summary>
        /// This is the suggested distance of the top of the underline from the baseline.
        /// </summary>
        public Int16 UnderlinePosition { get; }
        /// <summary>
        /// Suggested values for the underline thickness.
        /// </summary>
        public Int16 UnderlineThickness { get; }
        /// <summary>
        /// Set to 0 if the font is proportionally spaced.
        /// </summary>
        public UInt32 IsFixedPitch { get; }
        /// <summary>
        /// Minimum memory usage when an OpenType font is downloaded.
        /// </summary>
        public UInt32 MinMemType42 { get; }
        /// <summary>
        ///  Maximum memory usage when an OpenType font is downloaded. 
        /// </summary>
        public UInt32 MaxMemType42 { get; }
        /// <summary>
        /// Minimum memory usage when an OpenType font is downloaded as a Type 1 font.
        /// </summary>
        public UInt32 MinMemType1 { get; }
        /// <summary>
        /// Maximum memory usage when an OpenType font is downloaded as a Type 1 font.
        /// </summary>
        public UInt32 MaxMemType1 { get; }
        /// <summary>
        /// Number of glyphs.
        /// </summary>
        public UInt16 NumberOfGlyphs { get; }
        /// <summary>
        /// The collection of glyph name indices.
        /// </summary>
        public ImmutableArray<UInt16> GlyphNameIndices { get; }
        /// <summary>
        /// The glyph names.
        /// </summary>
        public ImmutableArray<string> GlyphNames { get; }
    }
}
