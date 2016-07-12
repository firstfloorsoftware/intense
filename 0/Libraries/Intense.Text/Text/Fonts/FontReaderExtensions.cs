using Intense.Text.Fonts.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Media;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Extension methods for <see cref="FontReader"/>.
    /// </summary>
    public static class FontReaderExtensions
    {
        /// <summary>
        /// Reads the typeface from given font reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Typeface ReadTypeface(this FontReader reader)
        {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            // taken from "WPF Font Selection model.pdf"
            var head = reader.ReadTable<FontTableHead>();
            var os2 = reader.ReadTable<FontTableOS2>();
            var name = reader.ReadTable<FontTableName>();

            if (head == null || name == null) {
                throw new ArgumentException("InvalidFont");
            }

            var names = GetFontNames(name);

            var typeface = new Typeface {
                FamilyName = GetFontFamilyName(names, os2)?.Trim(),
                FaceName = GetFaceName(names, os2)?.Trim(),
                Style = GetFontStyle(head, os2),
                Stretch = GetFontStrech(head, os2),
                Weight = GetFontWeight(head, os2),
                Pictorial = os2.FamilyClass == IBMFontClass.Symbolic|| os2.Panose.FamilyType == PanoseFamilyType.Pictorial
            };

            typeface.FullName = typeface.FamilyName;
            if (!string.IsNullOrEmpty(typeface.FaceName)) {
                typeface.FullName += ' ' + typeface.FaceName;
            }

            // font differentiation, applying a correct font family to the typeface
            ApplyFontDifferentiation(typeface);

            return typeface;
        }

        private static void ApplyFontDifferentiation(Typeface typeface)
        {
            var familyName = typeface.FamilyName;
            var faceName = typeface.FaceName;

            var options = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;

            // A.1. Remove the rightmost occurrence of a regular face name from FaceName
            var match_A1 = Regex.Match(faceName, "(Book|Normal|Regular|Roman|Upright)$", options);
            if (match_A1.Success) {
                faceName = faceName.Substring(0, match_A1.Index);
            }

            // A.2. Append FaceName to FontFamily, unless FontFamily already includes the same text as FaceName
            if (!string.IsNullOrWhiteSpace(faceName)) {
                var match_A2 = Regex.Match(familyName, "\\b" + faceName + "\\b", options);
                if (!match_A2.Success) {
                    familyName += ' ' + faceName;
                }
            }

            // B. Extract terms for style
            FontStyle? extractedStyle = null;
            var match_B = Regex.Match(familyName, "\\b(ita|ital|italic|cursive|kursiv|inclined|oblique|backslanted|backslant|slanted)\\b", options | RegexOptions.RightToLeft);
            if (match_B.Success) {
                familyName = familyName.Substring(0, match_B.Index).Trim() + ' ' + familyName.Substring(match_B.Index + match_B.Length).Trim();

                var term = match_B.Value.ToLowerInvariant();
                if (term == "ita" || term == "ital" || term == "italic" || term == "cursive" || term == "kursiv") {
                    extractedStyle = FontStyle.Italic;
                }
                else {
                    extractedStyle = FontStyle.Oblique;
                }
            }

            // C. Extract terms for stretch
            var match_C = Regex.Match(familyName, "\\b(extra[\\s.,-_]*compressed|ext[\\s.,-_]*compressed|ultra[\\s.,-_]*compressed|ultra[\\s.,-_]*condensed|ultra[\\s.,-_]*cond|compressed|extra[\\s.,-_]*condensed|ext[\\s.,-_]*condensed|extra[\\s.,-_]*cond|ext[\\s.,-_]*cond|narrow|compact|semi[\\s.,-_]*condensed|semi[\\s.,-_]*cond|wide|semi[\\s.,-_]*expanded|semi[\\s.,-_]*extended|extra[\\s.,-_]*expanded|ext[\\s.,-_]*expanded|extra[\\s.,-_]*extended|ext[\\s.,-_]*extended|ultra[\\s.,-_]*expanded|ultra[\\s.,-_]*extended|condensed|cond|expanded|extended)\\b", options | RegexOptions.RightToLeft);
            if (match_C.Success) {
                familyName = familyName.Substring(0, match_C.Index).Trim() + ' ' + familyName.Substring(match_C.Index + match_C.Length).Trim();
            }

            // D. Extract terms for weight 
            var match_D = Regex.Match(familyName, "\\b(extra[\\s.,-_]*thin|ext[\\s.,-_]*thin|ultra[\\s.,-_]*thin|extra[\\s.,-_]*light|ext[\\s.,-_]*light|ultra[\\s.,-_]*light|semi[\\s.,-_]*bold|demi[\\s.,-_]*bold|extra[\\s.,-_]*bold|ext[\\s.,-_]*bold|ultra[\\s.,-_]*bold|extra[\\s.,-_]*black|ext[\\s.,-_]*black|ultra[\\s.,-_]*black|bold|thin|light|medium|black|heavy|nord|demi|ultra)([\\s.,-_]*face)?\\b", options | RegexOptions.RightToLeft);
            if (match_D.Success) {
                familyName = familyName.Substring(0, match_D.Index).Trim() + ' ' + familyName.Substring(match_D.Index + match_D.Length).Trim();
            }

            // E. Determine resolved weight

            // F. Determine resolved stretch 

            // G. Determine resolved style 
            if (extractedStyle.HasValue) {
                typeface.Style = extractedStyle.Value;
            }

            // H. Extract numbers that describe font style, weight and stretch from face names.

            // I. Determine final FontFamily and FaceName 

            typeface.FamilyName = familyName;
            typeface.FaceName = faceName;
        }

        private static FontNames GetFontNames(FontTableName name)
        {
            // in order of priority
            // 1) platform Windows, language 1033
            var encodingIDs = (from record in name.NameRecords
                               where record.PlatformID == PlatformID.Windows && record.LanguageID == 1033
                               select record.EncodingID).Distinct();

            if (encodingIDs.Any()) {
                return new FontNames(name, PlatformID.Windows, encodingIDs.First(), 1033);
            }

            // 2) platform Macinthosh, encoding 0, language 0
            return new FontNames(name, PlatformID.Macintosh, 0, 0);
        }

        private static string GetFontFamilyName(FontNames names, FontTableOS2 os2)
        {
            if ((os2.Selection & FontSelection.WWS) > 0) {
                return names.TypographicFamilyName ?? names.FontFamilyName;
            }
            return names.WWSFamilyName ?? names.TypographicFamilyName ?? names.FontFamilyName;
        }

        private static string GetFaceName(FontNames names, FontTableOS2 os2)
        {
            if ((os2.Selection & FontSelection.WWS) > 0) {
                return names.TypographicSubfamilyName ?? names.FontFamilySubname;
            }
            return names.WWSSubfamilyName ?? names.TypographicSubfamilyName ?? names.FontFamilySubname;
        }

        private static FontStyle GetFontStyle(FontTableHead head, FontTableOS2 os2)
        {
            if (os2 != null) {
                if ((os2.Selection & FontSelection.Oblique) > 0) {
                    return FontStyle.Oblique;
                }
                else if ((os2.Selection & FontSelection.Italic) > 0) {
                    return FontStyle.Italic;
                }
                else {
                    return FontStyle.Normal;
                }
            }
            else if ((head.MacStyle & MacStyle.Italic) > 0) {
                return FontStyle.Italic;
            }
            return FontStyle.Normal;
        }

        private static FontStretch GetFontStrech(FontTableHead head, FontTableOS2 os2)
        {
            if (os2 != null) {
                return (FontStretch)os2.WidthClass;
            }
            else if ((head.MacStyle & MacStyle.Condensed) > 0) {
                return FontStretch.Condensed;
            }
            return FontStretch.Normal;
        }

        private static FontWeight GetFontWeight(FontTableHead head, FontTableOS2 os2)
        {
            if (os2 != null) {
                switch (os2.WeightClass) {
                    case 100: return FontWeights.Thin;
                    case 200: return FontWeights.ExtraLight;
                    case 300: return FontWeights.Light;
                    case 400: return FontWeights.Normal;
                    case 500: return FontWeights.Medium;
                    case 600: return FontWeights.SemiBold;
                    case 700: return FontWeights.Bold;
                    case 800: return FontWeights.ExtraBold;
                    case 900: return FontWeights.Black;
                    case 950: return FontWeights.ExtraBlack;
                    default: return FontWeights.Normal;     // should fail?
                }
            }
            else if ((head.MacStyle & MacStyle.Bold) > 0) {
                return FontWeights.Bold;
            }

            return FontWeights.Normal;
        }
    }
}
