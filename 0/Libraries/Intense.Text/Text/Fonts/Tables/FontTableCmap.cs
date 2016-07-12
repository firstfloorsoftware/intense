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
    /// Defines the mapping of character codes to the glyph index values used in the font
    /// </summary>
    public class FontTableCmap
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableCmap"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableCmap(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadUInt16(0);
            this.NumTables = buffer.ReadUInt16(2);

            var b = ImmutableArray.CreateBuilder<CmapEncodingTable>();
            for (var i = 0; i < this.NumTables; i++) {
                var platformID = (PlatformID)buffer.ReadUInt16(i * 8 + 4);
                var encodingID = buffer.ReadUInt16(i * 8 + 6);
                var offset = buffer.ReadUInt32(i * 8 + 8);
                
                // cast offset to int okay?
                var table = ReadEncodingTable(buffer, (int)offset, platformID, encodingID);

                b.Add(table);
            }

            this.EncodingTables = b.ToImmutable();
        }

        private CmapEncodingTable ReadEncodingTable(byte[] buffer, int offset, PlatformID platformID, UInt16 encodingID)
        {
            var format = buffer.ReadUInt16(offset);
            if (format == 0) {
                return new CmapEncodingTableFormat0(buffer, offset, platformID, encodingID);
            }
            if (format == 2) {
                return new CmapEncodingTableFormat2(buffer, offset, platformID, encodingID);
            }
            if (format == 4) {
                return new CmapEncodingTableFormat4(buffer, offset, platformID, encodingID);
            }
            if (format == 6) {
                return new CmapEncodingTableFormat6(buffer, offset, platformID, encodingID);
            }
            if (format == 8) {
                return new CmapEncodingTableFormat8(buffer, offset, platformID, encodingID);
            }
            if (format == 10) {
                return new CmapEncodingTableFormat10(buffer, offset, platformID, encodingID);
            }
            if (format == 12) {
                return new CmapEncodingTableFormat12(buffer, offset, platformID, encodingID);
            }
            if (format == 13) {
                return new CmapEncodingTableFormat13(buffer, offset, platformID, encodingID);
            }
            if (format == 14) {
                return new CmapEncodingTableFormat14(buffer, offset, platformID, encodingID);
            }

            // unknown format, return base encoding table
            return new CmapEncodingTable(format, platformID, encodingID);
        }

        /// <summary>
        /// Gets the table version number.
        /// </summary>
        public UInt16 Version { get; }
        /// <summary>
        ///  Number of encoding tables that follow. 
        /// </summary>
        public UInt16 NumTables { get; }
        /// <summary>
        /// Gets the encoding tables.
        /// </summary>
        public ImmutableArray<CmapEncodingTable> EncodingTables { get; }
    }
}
