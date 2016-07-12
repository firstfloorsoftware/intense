using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Identifies predefined font name ids.
    /// </summary>
    public enum NameID : UInt16
    {
        /// <summary>
        /// Copyright notice.
        /// </summary>
        Copyright = 0,
        /// <summary>
        /// Font Family name.
        /// </summary>
        FontFamilyName = 1,
        /// <summary>
        /// The Font Subfamily name distiguishes the font in a group with the same Font Family name.
        /// </summary>
        FontFamilySubname = 2,
        /// <summary>
        /// Unique font identifier.
        /// </summary>
        FontIdentifier = 3,
        /// <summary>
        /// A combination of font familyname and subfamily name.
        /// </summary>
        FullName = 4,
        /// <summary>
        /// Font version.
        /// </summary>
        Version = 5,
        /// <summary>
        /// PostScript language font name.
        /// </summary>
        PostscriptName = 6,
        /// <summary>
        /// Trademark notice/information.
        /// </summary>
        Trademark = 7,
        /// <summary>
        /// Manufacturer Name
        /// </summary>
        Manufacturer = 8,
        /// <summary>
        /// Name of the designer of the typeface
        /// </summary>
        Designer = 9,
        /// <summary>
        /// Description of the typeface
        /// </summary>
        Description = 10,
        /// <summary>
        /// URL of font vendor 
        /// </summary>
        UrlVendor = 11,
        /// <summary>
        /// URL of typeface designer 
        /// </summary>
        UrlDesigner = 12,
        /// <summary>
        /// Description of how the font may be legally used
        /// </summary>
        LicenseDescription = 13,
        /// <summary>
        /// URL where additional licensing information can be found.
        /// </summary>
        LicenseInfoUrl = 14,
        /// <summary>
        /// Specifies a family name within the typographic family grouping.
        /// </summary>
        TypographicFamilyName = 16,
        /// <summary>
        /// Specifies a subfamily name within the typographic family grouping.
        /// </summary>
        TypographicSubfamilyName = 17,
        /// <summary>
        /// Compatible full name (Mac only)
        /// </summary>
        MacFullname = 18,
        /// <summary>
        /// This can be the font name, or any other text that the designer thinks is the best sample to display the font in.
        /// </summary>
        SampleText = 19,
        /// <summary>
        /// PostScript CID findfont name.
        /// </summary>
        PostscriptCIDFindFontName = 20,
        /// <summary>
        /// Provide a WWS-conformant family name 
        /// </summary>
        WWSFamilyName = 21,
        /// <summary>
        /// Provides a WWS-conformant subfamily name 
        /// </summary>
        WWSSubfamilyName = 22,
        /// <summary>
        /// Specifies that the corresponding color palette in the CPAL table is appropriate to use with the font when displaying it on a light background such as white.
        /// </summary>
        LightBackgroundPalette = 23,
        /// <summary>
        /// Specifies that the corresponding color palette in the CPAL table is appropriate to use with the font when displaying it on a dark background such as black.
        /// </summary>
        DarkBackgroundPalette = 24
    }
}
