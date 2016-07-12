using Intense.Text.Parsers.BBCode;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;

namespace Intense.Text
{
    /// <summary>
    /// Provides BBCode parsing and helper functionality.
    /// </summary>
    public static class BBCode
    {

#pragma warning disable 1591
        public const string TagBase = "base";
        public const string TagBody = "body";
        public const string TagBold = "bold";
        public const string TagBoldAcronym = "b";
        public const string TagButton = "button";
        public const string TagCaption = "caption";
        public const string TagCode = "code";
        public const string TagColor = "color";
        public const string TagColorAcronym = "c";
        public const string TagFont = "font";
        public const string TagFontAcronym = "f";
        public const string TagHeader = "header";
        public const string TagHeaderAcronym = "h1";
        public const string TagIcon = "icon";
        public const string TagImage = "image";
        public const string TagImageAcronym = "img";
        public const string TagItalic = "italic";
        public const string TagItalicAcronym = "i";
        public const string TagPath = "path";
        public const string TagSize = "size";
        public const string TagStyle = "style";
        public const string TagSubheader = "subheader";
        public const string TagSubheaderAcronym = "h2";
        public const string TagSubtitle = "subtitle";
        public const string TagSubtitleAcronym = "h4";
        public const string TagSubscript = "sub";
        public const string TagSuperscript = "super";
        public const string TagTitle = "title";
        public const string TagTitleAcronym = "h3";
        public const string TagUnderline = "underline";
        public const string TagUnderlineAcronym = "u";
        public const string TagUrl = "url";
#pragma warning restore 1591

        private static readonly HashSet<string> supportedTags = new HashSet<string>(new[] {
            TagBase, TagBody, TagBold, TagBoldAcronym, TagButton, TagCaption, TagCode, TagColor, TagColorAcronym, TagFont, TagFontAcronym, TagHeader, TagHeaderAcronym,
            TagIcon, TagImage, TagImageAcronym, TagItalic, TagItalicAcronym, TagPath, TagSize, TagStyle, TagSubheader, TagSubheaderAcronym, TagSubtitle, TagSubtitleAcronym,
            TagSubscript, TagSuperscript,TagTitle, TagTitleAcronym, TagUnderline, TagUnderlineAcronym, TagUrl
        });

        private static Dictionary<string, string> equivalentTags = CreateEquivalentTags();

        private static Dictionary<string, string> CreateEquivalentTags()
        {
            var tags = new Dictionary<string, string>();
            tags[TagBold] = TagBoldAcronym;
            tags[TagBoldAcronym] = TagBold;
            tags[TagColor] = TagColorAcronym;
            tags[TagColorAcronym] = TagColor;
            tags[TagFont] = TagFontAcronym;
            tags[TagFontAcronym] = TagFont;
            tags[TagHeader] = TagHeaderAcronym;
            tags[TagHeaderAcronym] = TagHeader;
            tags[TagImage] = TagImageAcronym;
            tags[TagImageAcronym] = TagImage;
            tags[TagItalic] = TagItalicAcronym;
            tags[TagItalicAcronym] = TagItalic;
            tags[TagSubheader] = TagSubheaderAcronym;
            tags[TagSubheaderAcronym] = TagSubheader;
            tags[TagSubtitle] = TagSubtitleAcronym;
            tags[TagSubtitleAcronym] = TagSubtitle;
            tags[TagTitle] = TagTitleAcronym;
            tags[TagTitleAcronym] = TagTitle;
            tags[TagUnderline] = TagUnderlineAcronym;
            tags[TagUnderlineAcronym] = TagUnderline;

            return tags;
        }

        /// <summary>
        /// Gets the collection of supported tags.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetSupportedTags()
        {
            return ImmutableList.CreateRange(supportedTags);
        }

        /// <summary>
        /// Determines whether specified tag is supported.
        /// </summary>
        public static bool IsSupportedTag(string tag)
        {
            return supportedTags.Contains(tag);
        }

        /// <summary>
        /// Determines whether the tags are equivalent.
        /// </summary>
        /// <param name="tag1"></param>
        /// <param name="tag2"></param>
        public static bool AreEquivalentTags(string tag1, string tag2)
        {
            if (tag1 == tag2) {
                return true;
            }

            string value;
            return equivalentTags.TryGetValue(tag1, out value) && tag2 == value;
        }

        /// <summary>
        /// Parses specified BBCode value and returns the XAML representation of a Paragraph text element.
        /// </summary>
        /// <param name="bbcode"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string Parse(string bbcode, IBBCodeParserSettings settings = null)
        {
            var parser = new BBCodeParser(bbcode, settings);
            return parser.Parse();
        }

        /// <summary>
        /// Parses specified BBCode value and returns the resulting Paragraph text element.
        /// </summary>
        /// <param name="bbcode"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static Paragraph ParseParagraph(string bbcode, IBBCodeParserSettings settings = null)
        {
            var xaml = Parse(bbcode, settings);
            return (Paragraph)XamlReader.Load(xaml);
        }

        /// <summary>
        /// Escapes bbcode tags found in the input string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Escape(string value)
        {
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return value.Replace("[", "[[");
        }

        /// <summary>
        /// Converts any escaped characters in the input string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Unescape(string value)
        {
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return value.Replace("[[", "[");
        }

        /// <summary>
        /// Formats specified BBCode template and ensures the provided arguments are properly escaped.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(string template, params object[] args)
        {
            if (args == null) {
                return template;
            }
            // escape args
            for(var i =0; i < args.Length; i++) {
                if (args[i] != null) {
                    args[i] = Escape(args[i].ToString());
                }
            }
            return string.Format(template, args);
        }
    }
}
