using Intense.Text.Fonts.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Provides strong typed access to font name entries for a particular platform, encoding, and language.
    /// </summary>
    public class FontNames
    {
        private Dictionary<NameID, string> names = new Dictionary<NameID, string>();

        internal FontNames(FontTableName name, PlatformID platformID, UInt16 encodingID, UInt16 languageID)
        {
            this.PlatformID = platformID;
            this.EncodingID = encodingID;
            this.LanguageID = languageID;

            var records = name.NameRecords.Where(r => r.PlatformID == platformID && r.EncodingID == encodingID && r.LanguageID == languageID);
            foreach (var record in records) {
                this.names[record.NameID] = record.Value;
            }
        }

        /// <summary>
        /// Retrieves specified name.
        /// </summary>
        /// <param name="nameID"></param>
        /// <returns></returns>
        public string GetName(NameID nameID)
        {
            string name;
            if (!this.names.TryGetValue(nameID, out name)) {
                return null;
            }
            return name;
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
        /// Copyright notice.
        /// </summary>
        public string Copyright
        {
            get { return GetName(NameID.Copyright); }
        }

        /// <summary>
        /// Specifies that the corresponding color palette in the CPAL table is appropriate to use with the font when displaying it on a dark background such as black.
        /// </summary>
        public string DarkBackgroundPalette
        {
            get { return GetName(NameID.DarkBackgroundPalette); }
        }

        /// <summary>
        /// Description of the typeface
        /// </summary>
        public string Description
        {
            get { return GetName(NameID.Description); }
        }

        /// <summary>
        /// Name of the designer of the typeface
        /// </summary>
        public string Designer
        {
            get { return GetName(NameID.Designer); }
        }

        /// <summary>
        /// Font Family name.
        /// </summary>
        public string FontFamilyName
        {
            get { return GetName(NameID.FontFamilyName); }
        }

        /// <summary>
        /// The Font Subfamily name distiguishes the font in a group with the same Font Family name.
        /// </summary>
        public string FontFamilySubname
        {
            get { return GetName(NameID.FontFamilySubname); }
        }

        /// <summary>
        /// Unique font identifier.
        /// </summary>
        public string FontIdentifier
        {
            get { return GetName(NameID.FontIdentifier); }
        }

        /// <summary>
        /// A combination of font familyname and subfamily name.
        /// </summary>
        public string FullName
        {
            get { return GetName(NameID.FullName); }
        }

        /// <summary>
        /// Description of how the font may be legally used
        /// </summary>
        public string LicenseDescription
        {
            get { return GetName(NameID.LicenseDescription); }
        }

        /// <summary>
        /// URL where additional licensing information can be found.
        /// </summary>
        public string LicenseInfoUrl
        {
            get { return GetName(NameID.LicenseInfoUrl); }
        }

        /// <summary>
        /// Specifies that the corresponding color palette in the CPAL table is appropriate to use with the font when displaying it on a light background such as white.
        /// </summary>
        public string LightBackgroundPalette
        {
            get { return GetName(NameID.LightBackgroundPalette); }
        }

        /// <summary>
        /// Compatible full name (Mac only)
        /// </summary>
        public string MacFullname
        {
            get { return GetName(NameID.MacFullname); }
        }

        /// <summary>
        /// Manufacturer Name
        /// </summary>
        public string Manufacturer
        {
            get { return GetName(NameID.Manufacturer); }
        }

        /// <summary>
        /// PostScript CID findfont name.
        /// </summary>
        public string PostscriptCIDFindFontName
        {
            get { return GetName(NameID.PostscriptCIDFindFontName); }
        }

        /// <summary>
        /// PostScript language font name.
        /// </summary>
        public string PostscriptName
        {
            get { return GetName(NameID.PostscriptName); }
        }

        /// <summary>
        /// This can be the font name, or any other text that the designer thinks is the best sample to display the font in.
        /// </summary>
        public string SampleText
        {
            get { return GetName(NameID.SampleText); }
        }

        /// <summary>
        /// Trademark notice/information.
        /// </summary>
        public string Trademark
        {
            get { return GetName(NameID.Trademark); }
        }

        /// <summary>
        /// Specifies a family name within the typographic family grouping.
        /// </summary>
        public string TypographicFamilyName
        {
            get { return GetName(NameID.TypographicFamilyName); }
        }

        /// <summary>
        /// Specifies a subfamily name within the typographic family grouping.
        /// </summary>
        public string TypographicSubfamilyName
        {
            get { return GetName(NameID.TypographicSubfamilyName); }
        }

        /// <summary>
        /// URL of typeface designer 
        /// </summary>
        public string UrlDesigner
        {
            get { return GetName(NameID.UrlDesigner); }
        }

        /// <summary>
        /// URL of font vendor 
        /// </summary>
        public string UrlVendor
        {
            get { return GetName(NameID.UrlVendor); }
        }

        /// <summary>
        /// Font version.
        /// </summary>
        public string Version
        {
            get { return GetName(NameID.Version); }
        }

        /// <summary>
        /// Provide a WWS-conformant family name 
        /// </summary>
        public string WWSFamilyName
        {
            get { return GetName(NameID.WWSFamilyName); }
        }

        /// <summary>
        /// Provides a WWS-conformant subfamily name 
        /// </summary>
        public string WWSSubfamilyName
        {
            get { return GetName(NameID.WWSSubfamilyName); }
        }
    }
}
