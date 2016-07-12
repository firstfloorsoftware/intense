using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// This table establishes the memory requirements for this font
    /// </summary>
    public class FontTableMaxp
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableMaxp"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableMaxp(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadFixedVersion(0);
            this.NumGlyphs = buffer.ReadUInt16(4);

            if (this.Version.Major == 1 && this.Version.Minor == 0) {
                VerifyTableLength(32);

                this.MaxPoints = buffer.ReadUInt16(6);
                this.MaxContours = buffer.ReadUInt16(8);
                this.MaxCompositePoints = buffer.ReadUInt16(10);
                this.MaxCompositeContours = buffer.ReadUInt16(12);
                this.MaxZones = buffer.ReadUInt16(14);
                this.MaxTwilightPoints = buffer.ReadUInt16(16);
                this.MaxStorage = buffer.ReadUInt16(18);
                this.MaxFunctionDefs = buffer.ReadUInt16(20);
                this.MaxInstructionDefs = buffer.ReadUInt16(22);
                this.MaxStackElements = buffer.ReadUInt16(24);
                this.MaxSizeOfInstructions = buffer.ReadUInt16(26);
                this.MaxComponentElements = buffer.ReadUInt16(28);
                this.MaxComponentDepth = buffer.ReadUInt16(30);
            }
        }

        /// <summary>
        /// Gets the table version number.
        /// </summary>
        public Version Version { get; }
        /// <summary>
        /// The number of glyphs in the font.
        /// </summary>
        public UInt16 NumGlyphs { get; }
        /// <summary>
        /// Maximum points in a non-composite glyph.
        /// </summary>
        public UInt16 MaxPoints { get; }
        /// <summary>
        /// Maximum contours in a non-composite glyph.
        /// </summary>
        public UInt16 MaxContours { get; }
        /// <summary>
        /// Maximum points in a composite glyph.
        /// </summary>
        public UInt16 MaxCompositePoints { get; }
        /// <summary>
        /// Maximum contours in a composite glyph.
        /// </summary>
        public UInt16 MaxCompositeContours { get; }
        /// <summary>
        /// 1 if instructions do not use the twilight zone 
        /// </summary>
        public UInt16 MaxZones { get; }
        /// <summary>
        /// Maximum points used in Z0.
        /// </summary>
        public UInt16 MaxTwilightPoints { get; }
        /// <summary>
        /// Number of Storage Area locations.
        /// </summary>
        public UInt16 MaxStorage { get; }
        /// <summary>
        ///  Number of FDEFs.
        /// </summary>
        public UInt16 MaxFunctionDefs { get; }
        /// <summary>
        ///  Number of IDEFs. 
        /// </summary>
        public UInt16 MaxInstructionDefs { get; }
        /// <summary>
        /// Maximum stack depth.
        /// </summary>
        public UInt16 MaxStackElements { get; }
        /// <summary>
        /// Maximum byte count for glyph instructions.
        /// </summary>
        public UInt16 MaxSizeOfInstructions { get; }
        /// <summary>
        /// Maximum number of components referenced at “top level” for any composite glyph.
        /// </summary>
        public UInt16 MaxComponentElements { get; }
        /// <summary>
        /// Maximum levels of recursion; 1 for simple components.
        /// </summary>
        public UInt16 MaxComponentDepth { get; }
    }
}
