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
    /// Naming Table
    /// </summary>
    public class FontTableName
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableName"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableName(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Format = buffer.ReadUInt16(0);
            this.Count = buffer.ReadUInt16(2);
            var stringOffset = buffer.ReadUInt16(4);

            var records = ImmutableArray.CreateBuilder<NameRecord>();
            var offset = 6;
            for (var i = 0; i < this.Count; i++) {
                var platformID = (PlatformID)buffer.ReadUInt16(offset);
                var encodingID = buffer.ReadUInt16(offset + 2);
                var languageID = buffer.ReadUInt16(offset + 4);
                var nameID = (NameID)buffer.ReadUInt16(offset + 6);
                var length = buffer.ReadUInt16(offset + 8);
                var valueOffset = buffer.ReadUInt16(offset + 10);

                // retrieve encoding
                var encoding = GetEncoding(platformID, encodingID);
                var value = buffer.ReadString(stringOffset + valueOffset, length, encoding);

                records.Add(new NameRecord(platformID, encodingID, languageID, nameID, value));

                offset += 12;
            }

            this.NameRecords = records.ToImmutable();

            // Format 1 Naming Table 
            if (this.Format == 1) {
                var langTags = ImmutableArray.CreateBuilder<string>();

                this.LangTagCount = buffer.ReadUInt16(offset);
                offset += 2;
                var storageAreaOffset = offset + this.LangTagCount * 4;
                for(var i = 0; i< this.LangTagCount; i++) {
                    var length = buffer.ReadUInt16(offset + i * 2);
                    var tagOffset = buffer.ReadUInt16(offset + i * 2 + 2);

                    // seek to string value
                    var tag = buffer.ReadString(storageAreaOffset + tagOffset, length, Encoding.BigEndianUnicode);
                    langTags.Add(tag); 
                }

                this.LangTags = langTags.ToImmutable();
            }
        }

        private static Encoding GetEncoding(PlatformID platformID, UInt16 encodingID)
        {
            // TODO: determine encoding
            return Encoding.BigEndianUnicode;
        }

        /// <summary>
        /// Format selector.
        /// </summary>
        public UInt16 Format { get; }
        /// <summary>
        /// Number of name records.
        /// </summary>
        public UInt16 Count { get; }
        /// <summary>
        /// Gets the name records.
        /// </summary>
        public ImmutableArray<NameRecord> NameRecords { get; }
        /// <summary>
        /// Number of language-tags.
        /// </summary>
        public UInt16 LangTagCount { get; }
        /// <summary>
        /// Gets the language-tags.
        /// </summary>
        public ImmutableArray<string> LangTags { get; }
    }
}
