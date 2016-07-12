using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Represents a name record.
    /// </summary>
    public struct NameRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameRecord"/>.
        /// </summary>
        /// <param name="platformID"></param>
        /// <param name="encodingID"></param>
        /// <param name="languageID"></param>
        /// <param name="nameID"></param>
        /// <param name="value"></param>
        public NameRecord(PlatformID platformID, UInt16 encodingID, UInt16 languageID, NameID nameID, string value)
        {
            this.PlatformID = platformID;
            this.EncodingID = encodingID;
            this.LanguageID = languageID;
            this.NameID = nameID;
            this.Value = value;
        }

        /// <summary>
        /// Platform ID.
        /// </summary>
        public PlatformID PlatformID { get; }
        /// <summary>
        /// Platform-specific encoding ID.
        /// </summary>
        public UInt16 EncodingID { get; }
        /// <summary>
        /// The language ID.
        /// </summary>
        public UInt16 LanguageID { get; }
        /// <summary>
        /// The name ID.
        /// </summary>
        public NameID NameID { get; }
        /// <summary>
        /// The actual name value.
        /// </summary>
        public string Value { get; }

    }
}
