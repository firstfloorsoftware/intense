using Intense.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;

namespace Intense.Text.Parsers.BBCode
{
    /// <summary>
    /// Represents the BBCode parser.
    /// </summary>
    internal class BBCodeParser
        : Parser
    {
        class Setter
        {
            public string Tag { get; set; }
            public XObject Value { get; set; }
        }
        private static readonly XNamespace XNsIntenseNavigation = XNamespace.Get("using:Intense.Navigation");
        private static readonly XNamespace XNsIntenseText = XNamespace.Get("using:Intense.Text");
        private static readonly XName XNameNavigationCommand = XNsIntenseNavigation + "NavigationCommand";
        private static readonly XName XNameNavigationCommandProperty = XNsIntenseNavigation + "Navigation.Command";
        private static readonly XName XNameStyleProperty = XNsIntenseText + "Typography.Style";

        private IBBCodeParserSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BBCodeParser"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        public BBCodeParser(string value, IBBCodeParserSettings settings)
            : base(new BBCodeLexer(value))
        {
            this.settings = settings ?? new BBCodeParserSettings();
        }

        private string TryConsumeAttribute()
        {
            var token = LA(1);
            if (token.TokenType == BBCodeLexer.TokenAttribute) {
                Consume();

                return token.Value;
            }
            return null;
        }

        private string TryConsumeTextAndEndTag(string tag)
        {
            var textToken = LA(1);
            var endToken = LA(2);
            if (textToken.TokenType == BBCodeLexer.TokenText && endToken.TokenType == BBCodeLexer.TokenEndTag && endToken.Value == tag) {
                Consume();
                Consume();
                return textToken.Value;
            }
            return null;
        }

        private XElement ParseStartTag(string tag, Stack<Setter> setters)
        {
            var attr = TryConsumeAttribute();
            string text = null;
            var selfClosed = false;

            var setter = new Setter { Tag = tag };
            if (tag == Text.BBCode.TagBase) {
                setter.Value = new XAttribute(XNameStyleProperty, "{StaticResource " + Typography.BaseTextBlockStyleKey + "}");
            }
            else if (tag == Text.BBCode.TagBody) {
                setter.Value = new XAttribute(XNameStyleProperty, "{StaticResource " + Typography.BodyTextBlockStyleKey + "}");
            }
            else if (tag == Text.BBCode.TagBold || tag == Text.BBCode.TagBoldAcronym) {
                setter.Value = new XAttribute("FontWeight", "Bold");
            }
            else if (tag == Text.BBCode.TagButton) {
                var button = CreateElement("Button",
                    CreateElement("Button.Content",
                        CreateElement("RichTextBlock",
                            new XAttribute("IsTextSelectionEnabled", "false"),
                            CreateElement("Paragraph"))));

                var navigateUri = CreateNavigateUri(attr);
                if (navigateUri != null) {
                    button.Add(CreateElement("Button.Command",
                        new XElement(XNameNavigationCommand,
                            new XAttribute("NavigateUri", navigateUri))));

                    // set command parameter to button itself
                    button.SetAttributeValue("CommandParameter", "{Binding RelativeSource={RelativeSource Mode=Self}}");
                }

                setter.Value = CreateElement("InlineUIContainer", button);
            }
            else if (tag == Text.BBCode.TagCaption) {
                setter.Value = new XAttribute(XNameStyleProperty, "{StaticResource " + Typography.CaptionTextBlockStyleKey + "}");
            }
            else if (tag == Text.BBCode.TagCode) {
                setter.Value = new XAttribute("FontFamily", Typography.FontCourierNew);

                if (attr != null) {
                    // todo parse language for syntax coloring
                }
            }
            else if (tag == Text.BBCode.TagColor || tag == Text.BBCode.TagColorAcronym) {
                if (attr != null) {
                    setter.Value = new XAttribute("Foreground", attr);
                }
            }
            else if (tag == Text.BBCode.TagFont || tag == Text.BBCode.TagFontAcronym) {
                if (attr != null) {
                    setter.Value = new XAttribute("FontFamily", attr);
                }
            }
            else if (tag == Text.BBCode.TagHeader || tag == Text.BBCode.TagHeaderAcronym) {
                setter.Value = new XAttribute(XNameStyleProperty, "{StaticResource " + Typography.HeaderTextBlockStyleKey + "}");
            }
            else if (tag == Text.BBCode.TagIcon) {
                selfClosed = true;

                setter.Value = new XAttribute("FontFamily", Typography.FontSegoeMDL2Assets);

                text = attr;
                Symbol symbol;

                // attempt to parse icon from Symbol enum
                if (attr != null && Enum.TryParse<Symbol>(attr, true, out symbol)) {
                    text = ((char)symbol).ToString();
                }
            }
            else if (tag == Text.BBCode.TagImage || tag == Text.BBCode.TagImageAcronym) {
                string width = null;
                string height = null;

                // parse width and height
                if (attr != null) {
                    var parts = attr.Split(new[] { 'x' }, 2);
                    if (parts.Length == 1) {
                        width = parts[0];
                    }
                    else if (parts.Length == 2) {
                        width = parts[0];
                        height = parts[1];
                    }
                }

                var url = TryConsumeTextAndEndTag(tag);
                if (url != null) {
                    selfClosed = true;
                }

                // TODO: implement DecodePixelWith
                setter.Value = CreateElement("InlineUIContainer",
                    CreateElement("Image",
                        url != null ? new XAttribute("Source", url) : null,
                        new XAttribute("Stretch", width != null ? "Uniform" : "None"),
                        width != null ? new XAttribute("Width", width) : null,
                        height != null ? new XAttribute("Height", height) : null));
            }
            else if (tag == Text.BBCode.TagItalic || tag == Text.BBCode.TagItalicAcronym) {
                setter.Value = new XAttribute("FontStyle", "Italic");
            }
            else if (tag == Text.BBCode.TagPath) {
                var data = TryConsumeTextAndEndTag(tag);
                selfClosed = data != null;

                var fill = attr ?? "Black";

                setter.Value = CreateElement("InlineUIContainer",
                    CreateElement("Path",
                        new XAttribute("Data", data),
                        new XAttribute("Fill", fill)));

                // TODO: use fill from Foreground
            }
            else if (tag == Text.BBCode.TagSize) {
                if (attr != null) {
                    setter.Value = new XAttribute("FontSize", attr);
                }
            }
            else if (tag == Text.BBCode.TagStyle) {
                if (attr != null) {
                    setter.Value = new XAttribute(XNameStyleProperty, "{StaticResource " + attr + "}");
                }
            }
            else if (tag == Text.BBCode.TagSubheader || tag == Text.BBCode.TagSubheaderAcronym) {
                setter.Value = new XAttribute(XNameStyleProperty, "{StaticResource " + Typography.SubheaderTextBlockStyleKey + "}");
            }
            else if (tag == Text.BBCode.TagSubscript) {
                setter.Value = new XAttribute("Typography.Variants", "Subscript");
            }
            else if (tag == Text.BBCode.TagSubtitle || tag == Text.BBCode.TagSubtitleAcronym) {
                setter.Value = new XAttribute(XNameStyleProperty, "{StaticResource " + Typography.SubtitleTextBlockStyleKey + "}");
            }
            else if (tag == Text.BBCode.TagSuperscript) {
                setter.Value = new XAttribute("Typography.Variants", "Superscript");
            }
            else if (tag == Text.BBCode.TagTitle || tag == Text.BBCode.TagTitleAcronym) {
                setter.Value = new XAttribute(XNameStyleProperty, "{StaticResource " + Typography.TitleTextBlockStyleKey + "}");
            }
            else if (tag == Text.BBCode.TagUnderline || tag == Text.BBCode.TagUnderlineAcronym) {
                // don't emit if there's already an underline on the stack
                if (!setters.Select(s => s.Value).OfType<XElement>().Any(e => e.Name.LocalName == "Underline")) {
                    setter.Value = CreateElement("Underline");
                }
            }
            else if (tag == Text.BBCode.TagUrl) {
                var link = CreateElement("Hyperlink");

                // first attempt to parse format [url=xx]
                string url = attr;
                if (url == null) {
                    // otherwise try parse format [url]xx[/url]
                    url = TryConsumeTextAndEndTag(tag);
                    if (url != null) {
                        text = url;

                        // url is now self-closed
                        selfClosed = true;
                    }
                }
                var navigateUri = CreateNavigateUri(url);
                if (navigateUri != null) {
                    link.Add(new XElement(XNameNavigationCommandProperty,
                       new XElement(XNameNavigationCommand,
                           new XAttribute("NavigateUri", navigateUri))));
                }
                setter.Value = link;
            }
            else {
                // unknown tag
                return null;
            }

            setters.Push(setter);

            // close self-closed tags immediately
            if (selfClosed) {
                var inline = Finalize(CreateText(text, setters), setters);
                var parent = PopSetter(setters);

                return parent ?? inline;
            }

            return null;
        }

        private XElement PopSetter(Stack<Setter> setters)
        {
            var setter = setters.Pop();

            // return the parent (if any)
            return setter.Value as XElement;
        }

        private XElement CreateText(string text, Stack<Setter> setters)
        {
            // if text is empty, do not create anything
            if (string.IsNullOrEmpty(text)) {
                return null;
            }

            var run = CreateElement("Run",
                new XAttribute("Text", text));

            // apply setters from outer to inner, since inner trumps outer
            // outer-to-inner order achieved using Reverse()
            var filtered = (from s in setters
                            select s.Value).OfType<XAttribute>().Reverse();
            foreach (var setter in filtered) {
                run.SetAttributeValue(setter.Name, setter.Value);
            }

            return run;
        }

        private Token LA(int count, Stack<Setter> setters)
        {
            var token = LA(count);

            // translate unsupported and unexpected start and end tags into text

            if (token.TokenType == BBCodeLexer.TokenStartTag) {
                // translate start tag to text when
                // 1) tag is not supported
                if (!Text.BBCode.IsSupportedTag(token.Value)) {
                    token.TokenType = BBCodeLexer.TokenText;
                    token.Value = '[' + token.Value;

                    // check if next token is attribute, if so change that one to text as well
                    var nextToken = LA(count + 1);
                    if (nextToken.TokenType == BBCodeLexer.TokenAttribute) {
                        nextToken.TokenType = BBCodeLexer.TokenText;
                        nextToken.Value = '=' + nextToken.Value + ']';
                    }
                    else {
                        token.Value += ']';
                    }
                }
            }
            else if (token.TokenType == BBCodeLexer.TokenEndTag) {
                Setter peek = null;
                if (setters.Count > 0) {
                    peek = setters.Peek();
                }

                // translate end tag to text when
                // 1) there are no start tags on the stack (peek == null)
                // 2) tag is not supported
                // 3) end tag does not match start tag

                if (peek == null || !Text.BBCode.IsSupportedTag(token.Value) || !Text.BBCode.AreEquivalentTags(peek.Tag, token.Value)) {
                    token.TokenType = BBCodeLexer.TokenText;
                    token.Value = "[/" + token.Value + ']';
                }
            }

            return token;
        }

        private XElement Finalize(XElement inline, Stack<Setter> setters)
        {
            if (inline != null) {
                // find parent element
                var parent = setters.Select(s => s.Value).OfType<XElement>().Where(e => IsContainerElement(e)).FirstOrDefault();

                // find deepest descendant that is a container text element
                parent = parent?.DescendantsAndSelf().Where(e => IsContainerTextElement(e)).LastOrDefault();

                if (parent != null) {
                    parent.Add(inline);
                    return null;
                }

                // no parent, this is a root inline
                return inline;
            }

            return null;
        }

        private bool IsContainerElement(XElement element)
        {
            var name = element.Name.LocalName;

            return name == "InlineUIContainer" || IsContainerTextElement(element);
        }

        private bool IsContainerTextElement(XElement element)
        {
            var name = element.Name.LocalName;

            return name == "Bold" || name == "Hyperlink" || name == "Italic" || name == "Underline" || name == "Paragraph";
        }

        private string CreateNavigateUri(string url)
        {
            Uri navigateUri = null;
            if (url != null && Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out navigateUri)) {
                // make relative uris absolute
                if (!navigateUri.IsAbsoluteUri && this.settings.BaseUri != null) {
                    navigateUri = new Uri(this.settings.BaseUri, navigateUri);

                    return navigateUri.OriginalString;
                }
            }
            // return as-is
            return url;
        }

        private static XElement CreateElement(string localName, params object[] content)
        {
            return new XElement(Xmlns.XamlPresentation + localName, content);
        }
        /// <summary>
        /// Parses the BBCode and returns the XAML representation of a Paragraph text element.
        /// </summary>
        /// <returns></returns>
        public string Parse()
        {
            var start = DateTime.Now;
            var doc = new XDocument(CreateElement("Paragraph",
                new XAttribute(XNamespace.Xmlns + "nav", XNsIntenseNavigation),
                new XAttribute(XNamespace.Xmlns + "text", XNsIntenseText),
                    ParseElements()));

            var result = doc.ToString(SaveOptions.DisableFormatting);

            Debug.WriteLine("Parse took {0}ms", (DateTime.Now - start).TotalMilliseconds);

            return result;
        }

        private IEnumerable<XElement> ParseElements()
        {
            var setters = new Stack<Setter>();

            while (true) {
                XElement inline = null;

                var token = LA(1, setters);
                Consume();

                if (token.TokenType == BBCodeLexer.TokenStartTag) {
                    inline = ParseStartTag(token.Value, setters);
                }
                else if (token.TokenType == BBCodeLexer.TokenEndTag) {
                    inline = PopSetter(setters);
                }
                else if (token.TokenType == BBCodeLexer.TokenText) {
                    var text = token.Value;

                    // combine all consecutive text tokens into a single text element
                    var nextToken = LA(1, setters);
                    while (nextToken.TokenType == BBCodeLexer.TokenText) {
                        text += nextToken.Value;

                        Consume();
                        nextToken = LA(1, setters);
                    }

                    inline = CreateText(text, setters);
                }
                else if (token.TokenType == BBCodeLexer.TokenLineBreak) {
                    inline = CreateElement("LineBreak");
                }
                else if (token.TokenType == BBCodeLexer.TokenAttribute) {
                    throw new ParseException(ResourceHelper.GetString("UnexpectedToken"));
                }
                else if (token.TokenType == BBCodeLexer.TokenEnd) {
                    // auto-close all open setters
                    while (setters.Count > 0) {
                        inline = Finalize(PopSetter(setters), setters);
                        if (inline != null) {
                            yield return inline;
                        }
                    }

                    break;
                }
                else {
                    throw new ParseException(ResourceHelper.GetString("UnknownTokenType"));
                }

                inline = Finalize(inline, setters);
                if (inline != null) {
                    yield return inline;
                }
            }
        }
    }
}

