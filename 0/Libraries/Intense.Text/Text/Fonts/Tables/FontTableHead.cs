using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Provides global font information.
    /// </summary>
    public class FontTableHead
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableHead"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableHead(Stream stream, FontTableRecord record)
            : base(stream, record, 54)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadFixedVersion(0);
            this.FontRevision = buffer.ReadFixedVersion(4);
            this.ChecksumAdjustment = buffer.ReadUInt32(8);
            this.MagicNumber = buffer.ReadUInt32(12);
            this.Flags = (HeaderFlags)buffer.ReadUInt16(16);
            this.UnitsPerEm = buffer.ReadUInt16(18);
            this.Created = buffer.ReadDateTime(20);
            this.Modified = buffer.ReadDateTime(28);
            this.XMin = buffer.ReadInt16(36);
            this.YMin = buffer.ReadInt16(38);
            this.XMax = buffer.ReadInt16(40);
            this.YMax = buffer.ReadInt16(42);
            this.MacStyle = (MacStyle)buffer.ReadUInt16(44);
            this.LowestRecPPEM = buffer.ReadUInt16(46);
            this.FontDirectionHint = buffer.ReadInt16(48);
            this.IndexToLocFormat = buffer.ReadInt16(50);
            this.GlyphDataFormat = buffer.ReadInt16(52);
        }

        /// <summary>
        /// Gets the table version number.
        /// </summary>
        public Version Version { get; }
        /// <summary>
        /// Gets the font revision version.
        /// </summary>
        public Version FontRevision { get; }
        /// <summary>
        /// Gets the checksum adjustment.
        /// </summary>
        public UInt32 ChecksumAdjustment { get; }
        /// <summary>
        /// Gets the magic number.
        /// </summary>
        public UInt32 MagicNumber { get; }
        /// <summary>
        /// Gets the font flags.
        /// </summary>
        public HeaderFlags Flags { get; }
        /// <summary>
        /// Gets the units per em.
        /// </summary>
        public UInt16 UnitsPerEm { get; }
        /// <summary>
        /// Gets the creation date.
        /// </summary>
        public DateTime Created { get; }
        /// <summary>
        /// Gets the modified date.
        /// </summary>
        public DateTime Modified { get; }
        /// <summary>
        /// Gets the glyph bounding box min x.
        /// </summary>
        public Int16 XMin { get; }
        /// <summary>
        /// Gets the glyph bounding box min y.
        /// </summary>
        public Int16 YMin { get; }
        /// <summary>
        /// Gets the glyph bounding box max x.
        /// </summary>
        public Int16 XMax { get; }
        /// <summary>
        /// Gets the glyph bounding box max y.
        /// </summary>
        public Int16 YMax { get; }
        /// <summary>
        /// Gets the mac style flags.
        /// </summary>
        public MacStyle MacStyle { get; }
        /// <summary>
        /// Gets the smallest readable size in pixels.
        /// </summary>
        public UInt16 LowestRecPPEM { get; }
        /// <summary>
        /// Gets the font direction hint (deprecate).
        /// </summary>
        public Int16 FontDirectionHint { get; }
        /// <summary>
        /// Gets the index to loc format.
        /// </summary>
        public Int16 IndexToLocFormat { get; }
        /// <summary>
        /// Gets the glyph data format.
        /// </summary>
        public Int16 GlyphDataFormat { get; }
    }
}
