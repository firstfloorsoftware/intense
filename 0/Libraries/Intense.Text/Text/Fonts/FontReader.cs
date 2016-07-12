using Intense.Text.Fonts.Tables;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts
{
    using FactoryMethod = Func<Stream, FontTableRecord, FontTable>;

    /// <summary>
    /// Provides low-level access to Open Type font tables.
    /// </summary>
    public class FontReader
    {
        // maps table type to name
        private static readonly Dictionary<Type, string> tableNameMap = new Dictionary<Type, string> {
            { typeof(FontTableBASE), FontTableNames.BASE },
            { typeof(FontTableCBDT), FontTableNames.CBDT },
            { typeof(FontTableCBLC), FontTableNames.CBLC },
            { typeof(FontTableCFF), FontTableNames.CFF },
            { typeof(FontTableCmap), FontTableNames.Cmap },
            { typeof(FontTableCOLR), FontTableNames.COLR },
            { typeof(FontTableCPAL), FontTableNames.CPAL },
            { typeof(FontTableCvt), FontTableNames.Cvt },
            { typeof(FontTableDSIG), FontTableNames.DSIG },
            { typeof(FontTableEBDT), FontTableNames.EBDT },
            { typeof(FontTableEBLC), FontTableNames.EBLC },
            { typeof(FontTableEBSC), FontTableNames.EBSC },
            { typeof(FontTableFpgm), FontTableNames.Fpgm },
            { typeof(FontTableGasp), FontTableNames.Gasp },
            { typeof(FontTableGDEF), FontTableNames.GDEF },
            { typeof(FontTableGlyf), FontTableNames.Glyf },
            { typeof(FontTableGPOS), FontTableNames.GPOS },
            { typeof(FontTableGSUB), FontTableNames.GSUB },
            { typeof(FontTableHdmx), FontTableNames.Hdmx },
            { typeof(FontTableHead), FontTableNames.Head },
            { typeof(FontTableHhea), FontTableNames.Hhea },
            { typeof(FontTableHmtx), FontTableNames.Hmtx },
            { typeof(FontTableJSTF), FontTableNames.JSTF },
            { typeof(FontTableKern), FontTableNames.Kern },
            { typeof(FontTableLoca), FontTableNames.Loca },
            { typeof(FontTableLTSH), FontTableNames.LTSH },
            { typeof(FontTableMATH), FontTableNames.MATH },
            { typeof(FontTableMaxp), FontTableNames.Maxp },
            { typeof(FontTableName), FontTableNames.Name },
            { typeof(FontTableOS2), FontTableNames.OS2 },
            { typeof(FontTablePCLT), FontTableNames.PCLT },
            { typeof(FontTablePost), FontTableNames.Post },
            { typeof(FontTablePrep), FontTableNames.Prep },
            { typeof(FontTableSVG), FontTableNames.SVG },
            { typeof(FontTableVDMX), FontTableNames.VDMX },
            { typeof(FontTableVhea), FontTableNames.Vhea },
            { typeof(FontTableVmtx), FontTableNames.Vmtx },
            { typeof(FontTableVORG), FontTableNames.VORG }
        };

        // table factory
        private static readonly Dictionary<string, FactoryMethod> tableFactory = new Dictionary<string, FactoryMethod> {
            { FontTableNames.BASE, (s, r) => new FontTableBASE(s, r) },
            { FontTableNames.CBDT, (s, r) => new FontTableCBDT(s, r) },
            { FontTableNames.CBLC, (s, r) => new FontTableCBLC(s, r) },
            { FontTableNames.CFF, (s, r) => new FontTableCFF(s, r) },
            { FontTableNames.Cmap, (s, r) => new FontTableCmap(s, r) },
            { FontTableNames.COLR, (s, r) => new FontTableCOLR(s, r) },
            { FontTableNames.CPAL, (s, r) => new FontTableCPAL(s, r) },
            { FontTableNames.Cvt, (s, r) => new FontTableCvt(s, r) },
            { FontTableNames.DSIG, (s, r) => new FontTableDSIG(s, r) },
            { FontTableNames.EBDT, (s, r) => new FontTableEBDT(s, r) },
            { FontTableNames.EBLC, (s, r) => new FontTableEBLC(s, r) },
            { FontTableNames.EBSC, (s, r) => new FontTableEBSC(s, r) },
            { FontTableNames.Fpgm, (s, r) => new FontTableFpgm(s, r) },
            { FontTableNames.Gasp, (s, r) => new FontTableGasp(s, r) },
            { FontTableNames.GDEF, (s, r) => new FontTableGDEF(s, r) },
            { FontTableNames.Glyf, (s, r) => new FontTableGlyf(s, r) },
            { FontTableNames.GPOS, (s, r) => new FontTableGPOS(s, r) },
            { FontTableNames.GSUB, (s, r) => new FontTableGSUB(s, r) },
            { FontTableNames.Hdmx, (s, r) => new FontTableHdmx(s, r) },
            { FontTableNames.Head, (s, r) => new FontTableHead(s, r) },
            { FontTableNames.Hhea, (s, r) => new FontTableHhea(s, r) },
            { FontTableNames.JSTF, (s, r) => new FontTableJSTF(s, r) },
            { FontTableNames.Kern, (s, r) => new FontTableKern(s, r) },
            { FontTableNames.Loca, (s, r) => new FontTableLoca(s, r) },
            { FontTableNames.LTSH, (s, r) => new FontTableLTSH(s, r) },
            { FontTableNames.MATH, (s, r) => new FontTableMATH(s, r) },
            { FontTableNames.Maxp, (s, r) => new FontTableMaxp(s, r) },
            { FontTableNames.Name, (s, r) => new FontTableName(s, r) },
            { FontTableNames.OS2, (s, r) => new FontTableOS2(s, r) },
            { FontTableNames.PCLT, (s, r) => new FontTablePCLT(s, r) },
            { FontTableNames.Post, (s, r) => new FontTablePost(s, r) },
            { FontTableNames.Prep, (s, r) => new FontTablePrep(s, r) },
            { FontTableNames.SVG, (s, r) => new FontTableSVG(s, r) },
            { FontTableNames.VDMX, (s, r) => new FontTableVDMX(s, r) },
            { FontTableNames.Vhea, (s, r) => new FontTableVhea(s, r) },
            { FontTableNames.VORG, (s, r) => new FontTableVORG(s, r) }
        };

        private FontFile fontFile;
        private Dictionary<string, FontTableRecord> tableRecords;
        private ImmutableArray<string> tableNames;
        private Dictionary<string, FontTable> tables = new Dictionary<string, FontTable>();

        internal FontReader(FontFile fontFile, int fontIndex)
        {
            if (fontFile == null) {
                throw new ArgumentNullException(nameof(fontFile));
            }
            if (fontIndex < 0 || fontIndex >= fontFile.FontCount) {
                throw new ArgumentOutOfRangeException(nameof(fontIndex));
            }

            this.fontFile = fontFile;
            this.FontIndex = fontIndex;

            var records = ReadTableRecords().ToArray();
            this.tableRecords = records.ToDictionary(r => r.Name);
            this.tableNames = ImmutableArray.CreateRange<string>(records.Select(r => r.Name));
        }

        /// <summary>
        /// Gets the index of the current font.
        /// </summary>
        public int FontIndex { get; }

        private IEnumerable<FontTableRecord> ReadTableRecords()
        {
            var stream = this.fontFile.Stream;

            byte[] buffer;
            uint offset = 0;
            if (this.fontFile.FontType == FontType.TrueTypeCollection) {
                // seek to start of offset table
                stream.Seek(12 + 4 * this.FontIndex, SeekOrigin.Begin);

                buffer = stream.ReadBytes(4);
                offset = buffer.ReadUInt32(0);
            }

            // seek to offset table and skip version (4 bytes)
            stream.Seek(offset + 4, SeekOrigin.Begin);

            // read numTables
            buffer = stream.ReadBytes(2);
            var numTables = buffer.ReadUInt16(0);

            // skip searchRange, entrySelector and rangeShift
            stream.Seek(6, SeekOrigin.Current);

            buffer = stream.ReadBytes(numTables * 16);

            for (var i = 0; i < numTables; i++) {
                var name = buffer.ReadString(i * 16, 4, Encoding.ASCII);

                // remove any space
                name = name.Trim();

                yield return new FontTableRecord(name,
                    buffer.ReadUInt32(i * 16 + 4),
                    buffer.ReadUInt32(i * 16 + 8),
                    buffer.ReadUInt32(i * 16 + 12));
            }
        }

        /// <summary>
        /// Retrieves the ordered collection of table names found in the font.
        /// </summary>
        public ImmutableArray<string> TableNames
        {
            get { return this.tableNames; }
        }

        /// <summary>
        /// Attempts to reads the specified font table.
        /// </summary>
        /// <param name="name">The name of the table to read.</param>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public bool TryReadTable(string name, out FontTable table)
        {
            // check if table has been created before
            if (this.tables.TryGetValue(name, out table)) {
                // found it, return immediately
                return true;
            }

            FontTableRecord record;
            if (!this.tableRecords.TryGetValue(name, out record)) {
                return false;
            }

            table = CreateTable(record);

            // store table
            this.tables[name] = table;

            return true;
        }

        private FontTable CreateTable(FontTableRecord record)
        {
            var stream = this.fontFile.Stream;

            FactoryMethod method;
            if (tableFactory.TryGetValue(record.Name, out method)) {
                return method(stream, record);
            }

            // special case for hmtx and vmtx, having dependencies on other tables
            if (record.Name == FontTableNames.Hmtx) {
                // htmx depends on hhea and maxp
                var hhea = ReadTable<FontTableHhea>();
                var maxp = ReadTable<FontTableMaxp>();

                return new FontTableHmtx(stream, record, hhea.NumberOfHMetrics, maxp.NumGlyphs);
            }
            if (record.Name == FontTableNames.Vmtx) {
                var maxp = ReadTable<FontTableMaxp>();
                var vhea = ReadTable<FontTableVhea>();

                return new FontTableVmtx(stream, record, vhea.NumberOfVMetrics, maxp.NumGlyphs);
            }
            
            // no implementation, return generic font table
            return new FontTable(stream, record);
        }

        /// <summary>
        /// Reads a strong type table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The table, or null if the table could not be found.</returns>
        public T ReadTable<T>() where T : FontTable
        {
            string name;
            if (!tableNameMap.TryGetValue(typeof(T), out name)) {
                throw new NotSupportedException();
            }

            FontTable table;
            if (TryReadTable(name, out table)) {
                return (T)table;
            }
            return null;
        }

        /// <summary>
        /// Determines whether specified stream contains an Open Type font.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static bool IsFontStream(Stream stream)
        {
            ThrowIfInvalidStream(stream);
            stream.Seek(0, SeekOrigin.Begin);

            var buffer = stream.ReadBytes(4);
            var version = buffer.ReadUInt32(0);
            return version == 0x10000 || version == 0x4f54544f || version == 0x74746366;
        }

        /// <summary>
        /// Reads the font file from given stream.
        /// </summary>
        /// <param name="stream">The font stream.</param>
        /// <returns>The font file found in the stream.</returns>
        public static FontFile ReadFontFile(Stream stream)
        {
            ThrowIfInvalidStream(stream);
            stream.Seek(0, SeekOrigin.Begin);

            var buffer = stream.ReadBytes(12);
            var version = buffer.ReadUInt32(0);

            if (version == 0x10000) {
                return new FontFile(stream) {
                    FontType = FontType.TrueType,
                    FontCount = 1
                };
            }
            if (version == 0x4f54544f) {
                return new FontFile(stream) {
                    FontType = FontType.PostScript,
                    FontCount = 1
                };
            }
            if (version == 0x74746366) {
                return new FontFile(stream) {
                    FontType = FontType.TrueTypeCollection,
                    FontCount = (int)buffer.ReadUInt32(8)
                };
            }
            throw new ArgumentException("InvalidOpenTypeFontStream");
        }

        private static void ThrowIfInvalidStream(Stream stream)
        {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }
            if (!stream.CanRead) {
                throw new ArgumentException("StreamReadNotSupported");
            }
            if (!stream.CanSeek) {
                throw new ArgumentException("StreamSeekNotSupported");
            }
        }
    }
}
