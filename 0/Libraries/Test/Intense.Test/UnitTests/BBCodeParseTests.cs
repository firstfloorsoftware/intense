using Intense.Text;
using Intense.Text.Parsers;
using Intense.Text.Parsers.BBCode;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;


namespace Intense.Test.UnitTests
{
    [TestClass]
    public class BBCodeParserTests
    {
        [UITestMethod]
        public void Parse_null_should_throw_ArgumentNullException()
        {
            Action a = () => BBCode.Parse(null);
            a.ShouldThrow<ArgumentNullException>();
        }

        [UITestMethod]
        public void Parse_empty_string_should_match()
        {
            ParseResultShouldMatch("", "");
        }

        [UITestMethod]
        public void Parse_space_should_match()
        {
            ParseResultShouldMatch(" ", "<Run Text=\" \" />");
        }

        [UITestMethod]
        public void Parse_bold_endbold_should_match()
        {
            ParseResultShouldMatch("[b][/b]", "");
        }

        [UITestMethod]
        public void Parse_bold_space_endbold_should_match()
        {
            ParseResultShouldMatch("[b] [/b]", "<Run Text=\" \" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_cr_should_throw_ParseException()
        {
            Action a = () => ParseResultShouldMatch("\r", null);
            a.ShouldThrow<ParseException>().Which.Message.Should().Be("Character mismatch");
        }

        [UITestMethod]
        public void Parse_lf_should_match()
        {
            ParseResultShouldMatch("\n", "<LineBreak />");
        }

        [UITestMethod]
        public void Parse_crlf_should_match()
        {
            ParseResultShouldMatch("\r\n", "<LineBreak />");
        }

        [UITestMethod]
        public void Parse_lf_lf_should_match()
        {
            ParseResultShouldMatch("\n\n", "<LineBreak /><LineBreak />");
        }

        [UITestMethod]
        public void Parse_crlf_crlf_should_match()
        {
            ParseResultShouldMatch("\r\n\r\n", "<LineBreak /><LineBreak />");
        }

        [UITestMethod]
        public void Parse_foo_crlf_foo_should_match()
        {
            ParseResultShouldMatch("foo\r\nfoo", "<Run Text=\"foo\" /><LineBreak /><Run Text=\"foo\" />");
        }

        [UITestMethod]
        public void Parse_foo_crlf_foo_lf_should_match()
        {
            ParseResultShouldMatch("foo\r\nfoo\n", "<Run Text=\"foo\" /><LineBreak /><Run Text=\"foo\" /><LineBreak />");
        }

        [UITestMethod]
        public void Parse_bold_foo_crlf_endbold_foo_lf_should_match()
        {
            ParseResultShouldMatch("[b]foo\r\n[/b]foo\n", "<Run Text=\"foo\" FontWeight=\"Bold\" /><LineBreak /><Run Text=\"foo\" /><LineBreak />");
        }

        [UITestMethod]
        public void Parse_bold_foo_crlf_endbold_bold_foo_lf_endbold_should_match()
        {
            ParseResultShouldMatch("[b]foo\r\n[/b][b]foo\n[/b]", "<Run Text=\"foo\" FontWeight=\"Bold\" /><LineBreak /><Run Text=\"foo\" FontWeight=\"Bold\" /><LineBreak />");
        }

        [UITestMethod]
        public void Parse_bold_lf_endbold_should_match()
        {
            ParseResultShouldMatch("[b]\n[/b]", "<LineBreak />");
        }

        [UITestMethod]
        public void Parse_bold_crlf_endbold_should_match()
        {
            ParseResultShouldMatch("[b]\r\n[/b]", "<LineBreak />");
        }

        [UITestMethod]
        public void Parse_foo_should_match()
        {
            ParseResultShouldMatch("foo", "<Run Text=\"foo\" />");
        }

        [UITestMethod]
        public void Parse_bold_foo_endbold_should_match()
        {
            ParseResultShouldMatch("[b]foo[/b]", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_foo_endb_should_match()
        {
            ParseResultShouldMatch("[bold]foo[/b]", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_b_foo_endbold_should_match()
        {
            ParseResultShouldMatch("[b]foo[/bold]", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_f_endbold_o_italic_o_enditalic_should_match()
        {
            ParseResultShouldMatch("[b]f[/b]o[i]o[/i]", "<Run Text=\"f\" FontWeight=\"Bold\" /><Run Text=\"o\" /><Run Text=\"o\" FontStyle=\"Italic\" />");
        }

        [UITestMethod]
        public void Parse_bold_italic_foo_enditalic_endbold_should_match()
        {
            ParseResultShouldMatch("[b][i]foo[/i][/b]", "<Run Text=\"foo\" FontWeight=\"Bold\" FontStyle=\"Italic\" />");
        }

        [UITestMethod]
        public void Parse_bold_italic_foo_enditalic_foo_endbold_foo_should_match()
        {
            ParseResultShouldMatch("[b][i]foo[/i]foo[/b]foo", "<Run Text=\"foo\" FontWeight=\"Bold\" FontStyle=\"Italic\" /><Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"foo\" />");
        }

        [UITestMethod]
        public void Parse_underline_foo_endunderline_should_match()
        {
            ParseResultShouldMatch("[u]foo[/u]", "<Underline><Run Text=\"foo\" /></Underline>");
        }

        [UITestMethod]
        public void Parse_underline_underline_foo__endunderline_endunderline_should_match()
        {
            ParseResultShouldMatch("[u][u]foo[/u][/u]", "<Underline><Run Text=\"foo\" /></Underline>");
        }

        [UITestMethod]
        public void Parse_underline_underline_underline_foo_endunderline_endunderline_endunderline_should_match()
        {
            ParseResultShouldMatch("[u][u][u]foo[/u][/u][/u]", "<Underline><Run Text=\"foo\" /></Underline>");
        }

        [UITestMethod]
        public void Parse_underline_bold_foo_endbold_foo_endunderline_should_match()
        {
            ParseResultShouldMatch("[u][b]foo[/b]foo[/u]", "<Underline><Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"foo\" /></Underline>");
        }

        [UITestMethod]
        public void Parse_underline_bold_underline_foo_endunderline_endbold_endunderline_should_match()
        {
            ParseResultShouldMatch("[u][b][u]foo[/u][/b][/u]", "<Underline><Run Text=\"foo\" FontWeight=\"Bold\" /></Underline>");
        }

        [UITestMethod]
        public void Parse_underline_bold_underline_foo_endunderline_foo_endbold_endunderline_should_match()
        {
            ParseResultShouldMatch("[u][b][u]foo[/u]foo[/b][/u]", "<Underline><Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"foo\" FontWeight=\"Bold\" /></Underline>");
        }

        [UITestMethod]
        public void Parse_underline_bold_underline_foo_endunderline_endbold_foo_endunderline_should_match()
        {
            ParseResultShouldMatch("[u][b][u]foo[/u][/b]foo[/u]", "<Underline><Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"foo\" /></Underline>");
        }

        [UITestMethod]
        public void Parse_bold_italic_foo_enditalic_endbold_bold_foo_endbold_foo_should_match()
        {
            ParseResultShouldMatch("[b][i]foo[/i][/b][b]foo[/b]foo", "<Run Text=\"foo\" FontWeight=\"Bold\" FontStyle=\"Italic\" /><Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"foo\" />");
        }

        [UITestMethod]
        public void Parse_bold_bold_foo_endbold_endbold_should_match()
        {
            ParseResultShouldMatch("[b][b]foo[/b][/b]", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_bold_bold_foo_endbold_endbold_endbold_should_match()
        {
            ParseResultShouldMatch("[b][b][b]foo[/b][/b][/b]", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_bold_bold_f_endbold_o_endboldo_endbold_should_match()
        {
            ParseResultShouldMatch("[b][b][b]f[/b]o[/b]o[/b]", "<Run Text=\"f\" FontWeight=\"Bold\" /><Run Text=\"o\" FontWeight=\"Bold\" /><Run Text=\"o\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_sub_foo_endsub_should_match()
        {
            ParseResultShouldMatch("[sub]foo[/sub]", "<Run Text=\"foo\" Typography.Variants=\"Subscript\" />");
        }

        [UITestMethod]
        public void Parse_super_foo_endsuper_should_match()
        {
            ParseResultShouldMatch("[super]foo[/super]", "<Run Text=\"foo\" Typography.Variants=\"Superscript\" />");
        }

        [UITestMethod]
        public void Parse_super_sub_foo_endsub_endsuper_should_match()
        {
            ParseResultShouldMatch("[super][sub]foo[/sub][/super]", "<Run Text=\"foo\" Typography.Variants=\"Subscript\" />");
        }

        [UITestMethod]
        public void Parse_unknown_tag_foo_should_match()
        {
            ParseResultShouldMatch("[foo]", "<Run Text=\"[foo]\" />");
        }

        [UITestMethod]
        public void Parse_unknown_tag_foo_endfoo_should_match()
        {
            ParseResultShouldMatch("[foo][/foo]", "<Run Text=\"[foo][/foo]\" />");
        }

        [UITestMethod]
        public void Parse_foo_foo_endfoo_should_match()
        {
            ParseResultShouldMatch("[foo]foo[/foo]", "<Run Text=\"[foo]foo[/foo]\" />");
        }

        [UITestMethod]
        public void Parse_escapedfoo_foo_endescapedfoo_should_match()
        {
            ParseResultShouldMatch("[[foo]foo[[/foo]", "<Run Text=\"[foo]foo[/foo]\" />");
        }

        [UITestMethod]
        public void Parse_bold_foo_foo_endfoo_endbold_should_match()
        {
            ParseResultShouldMatch("[b][foo]foo[/foo][/b]", "<Run Text=\"[foo]foo[/foo]\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_foo_bold_foo_endbold_endfoo_should_match()
        {
            ParseResultShouldMatch("[foo][b]foo[/b][/foo]", "<Run Text=\"[foo]\" /><Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"[/foo]\" />");
        }

        [UITestMethod]
        public void Parse_foo_bold_foo_endfoo_endbold_should_match()
        {
            ParseResultShouldMatch("[foo][b]foo[/foo][/b]", "<Run Text=\"[foo]\" /><Run Text=\"foo[/foo]\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_color_red_foo_endcolor_should_match()
        {
            ParseResultShouldMatch("[color=red]foo[/color]", "<Run Text=\"foo\" Foreground=\"red\" />");
        }

        [UITestMethod]
        public void Parse_color_red_bold_foo_endbold_foo_endcolor_should_match()
        {
            ParseResultShouldMatch("[color=red][b]foo[/b]foo[/color]", "<Run Text=\"foo\" Foreground=\"red\" FontWeight=\"Bold\" /><Run Text=\"foo\" Foreground=\"red\" />");
        }

        [UITestMethod]
        public void Parse_color_red_foo_endcolor_color_orange_foo_endcolor_color_yellow_foo_endcolor_should_match()
        {
            ParseResultShouldMatch("[color=red]foo[/color][color=orange]foo[/color][color=yellow]foo[/color]", "<Run Text=\"foo\" Foreground=\"red\" /><Run Text=\"foo\" Foreground=\"orange\" /><Run Text=\"foo\" Foreground=\"yellow\" />");
        }

        [UITestMethod]
        public void Parse_color_red_foo_color_orange_foo_color_yellow_foo_endcolor_endcolor_endcolor_should_match()
        {
            ParseResultShouldMatch("[color=red]foo[color=orange]foo[color=yellow]foo[/color][/color][/color]", "<Run Text=\"foo\" Foreground=\"red\" /><Run Text=\"foo\" Foreground=\"orange\" /><Run Text=\"foo\" Foreground=\"yellow\" />");
        }

        [UITestMethod]
        public void Parse_url_google_attr_foo_endurl_should_match()
        {
            ParseResultShouldMatch("[url=http://www.google.com]foo[/url]", "<Hyperlink nav:Navigation.NavigateUri=\"http://www.google.com/\"><Run Text=\"foo\" /></Hyperlink>");
        }

        [UITestMethod]
        public void Parse_url_google_endurl_should_match()
        {
            ParseResultShouldMatch("[url]http://www.google.com[/url]", "<Hyperlink nav:Navigation.NavigateUri=\"http://www.google.com/\"><Run Text=\"http://www.google.com\" /></Hyperlink>");
        }

        [UITestMethod]
        public void Parse_url_google_should_match()
        {
            ParseResultShouldMatch("[url]http://www.google.com", "<Hyperlink><Run Text=\"http://www.google.com\" /></Hyperlink>");
        }

        [UITestMethod]
        public void Parse_bold_url_google_endurl_endbold_should_match()
        {
            ParseResultShouldMatch("[b][url]http://www.google.com[/url][/b]", "<Hyperlink nav:Navigation.NavigateUri=\"http://www.google.com/\"><Run Text=\"http://www.google.com\" FontWeight=\"Bold\" /></Hyperlink>");
        }

        [UITestMethod]
        public void Parse_url_bold_google_endbold_endurl_should_match()
        {
            ParseResultShouldMatch("[url][b]http://www.google.com[/b][/url]", "<Hyperlink><Run Text=\"http://www.google.com\" FontWeight=\"Bold\" /></Hyperlink>");
        }

        [UITestMethod]
        public void Parse_empty_icon_should_match()
        {
            ParseResultShouldMatch("[icon]", "");
        }

        [UITestMethod]
        public void Parse_endicon_should_match()
        {
            ParseResultShouldMatch("[/icon]", "<Run Text=\"[/icon]\" />");
        }

        [UITestMethod]
        public void Parse_empty_icon_endicon_should_match()
        {
            ParseResultShouldMatch("[icon][/icon]", "<Run Text=\"[/icon]\" />");
        }

        [UITestMethod]
        public void Parse_empty_icon_foo_endicon_should_match()
        {
            ParseResultShouldMatch("[icon]foo[/icon]", "<Run Text=\"foo[/icon]\" />");
        }

        [UITestMethod]
        public void Parse_empty_icon_bold_foo_endbold_endicon_should_match()
        {
            ParseResultShouldMatch("[icon][b]foo[/b][/icon]", "<Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"[/icon]\" />");
        }

        [UITestMethod]
        public void Parse_empty_icon_bold_foo_endicon_endbold_should_match()
        {
            ParseResultShouldMatch("[icon][b]foo[/icon][/b]", "<Run Text=\"foo[/icon]\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_icon_should_match()
        {
            ParseResultShouldMatch("[icon=]", "<Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" />");
        }

        [UITestMethod]
        public void Parse_icon_check_should_match()
        {
            ParseResultShouldMatch("[icon=check]", "<Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" />");
        }

        [UITestMethod]
        public void Parse_icon_check2_should_match()
        {
            ParseResultShouldMatch("[icon=check2]", "<Run Text=\"check2\" FontFamily=\"Segoe MDL2 Assets\" />");
        }

        [UITestMethod]
        public void Parse_icon_foo_should_match()
        {
            ParseResultShouldMatch("[icon=]foo", "<Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\"foo\" />");
        }

        [UITestMethod]
        public void Parse_icon_bold_foo_endbold_endicon_should_match()
        {
            ParseResultShouldMatch("[icon=][b]foo[/b][/icon]", "<Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"[/icon]\" />");
        }

        [UITestMethod]
        public void Parse_icon_bold_foo_endicon_endbold_should_match()
        {
            ParseResultShouldMatch("[icon=][b]foo[/icon][/b]", "<Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\"foo[/icon]\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_icon_foo_endicon_endbold_should_match()
        {
            ParseResultShouldMatch("[b][icon=]foo[/icon][/b]", "<Run Text=\"\" FontWeight=\"Bold\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\"foo[/icon]\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_icon_foo_endbold_endicon_should_match()
        {
            ParseResultShouldMatch("[b][icon=]foo[/b][/icon]", "<Run Text=\"\" FontWeight=\"Bold\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"[/icon]\" />");
        }

        [UITestMethod]
        public void Parse_icon_endicon_should_match()
        {
            ParseResultShouldMatch("[icon=][/icon]", "<Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\"[/icon]\" />");
        }

        [UITestMethod]
        public void Parse_icon_foo_endicon_should_match()
        {
            ParseResultShouldMatch("[icon=]foo[/icon]", "<Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\"foo[/icon]\" />");
        }

        [UITestMethod]
        public void Parse_bold_icon_endbold_should_match()
        {
            ParseResultShouldMatch("[b][icon=][/b]", "<Run Text=\"\" FontWeight=\"Bold\" FontFamily=\"Segoe MDL2 Assets\" />");
        }

        [UITestMethod]
        public void Parse_font_SegoeMDL2Assets_endfont_should_match()
        {
            ParseResultShouldMatch("[font=\"Segoe MDL2 Assets\"][/font]", "<Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" />");
        }

        [UITestMethod]
        public void Parse_bold_font_SegoeMDL2Assets_endfont_endbold_should_match()
        {
            ParseResultShouldMatch("[b][font=\"Segoe MDL2 Assets\"][/font][/b]", "<Run Text=\"\" FontWeight=\"Bold\" FontFamily=\"Segoe MDL2 Assets\" />");
        }

        [UITestMethod]
        public void Parse_header_foo_endheader_should_match()
        {
            ParseResultShouldMatch("[header]foo[/header]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource HeaderTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_h1_foo_endh1_should_match()
        {
            ParseResultShouldMatch("[h1]foo[/h1]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource HeaderTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_style_HeaderTextBlockStyle_foo_endstyle_should_match()
        {
            ParseResultShouldMatch("[style=HeaderTextBlockStyle]foo[/style]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource HeaderTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_header_icon_endheader_should_match()
        {
            ParseResultShouldMatch("[header][icon=][/header]", "<Run Text=\"\" text:Typography.Style=\"{StaticResource HeaderTextBlockStyle}\" FontFamily=\"Segoe MDL2 Assets\" />");
        }

        [UITestMethod]
        public void Parse_header_icon_foo_endheader_should_match()
        {
            ParseResultShouldMatch("[header][icon=]foo[/header]", "<Run Text=\"\" text:Typography.Style=\"{StaticResource HeaderTextBlockStyle}\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\"foo\" text:Typography.Style=\"{StaticResource HeaderTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_subheader_foo_endsubheader_should_match()
        {
            ParseResultShouldMatch("[subheader]foo[/subheader]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource SubheaderTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_style_SubheaderTextBlockStyle_foo_endstyle_should_match()
        {
            ParseResultShouldMatch("[style=SubheaderTextBlockStyle]foo[/style]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource SubheaderTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_h2_foo_endh2_should_match()
        {
            ParseResultShouldMatch("[h2]foo[/h2]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource SubheaderTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_title_foo_endtitle_should_match()
        {
            ParseResultShouldMatch("[title]foo[/title]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource TitleTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_style_TitleTextBlockStyle_foo_endstyle_should_match()
        {
            ParseResultShouldMatch("[style=TitleTextBlockStyle]foo[/style]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource TitleTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_h3_foo_endh3_should_match()
        {
            ParseResultShouldMatch("[h3]foo[/h3]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource TitleTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_subtitle_foo_endsubtitle_should_match()
        {
            ParseResultShouldMatch("[subtitle]foo[/subtitle]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource SubtitleTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_style_SubtitleTextBlockStyle_foo_endstyle_should_match()
        {
            ParseResultShouldMatch("[style=SubtitleTextBlockStyle]foo[/style]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource SubtitleTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_h4_foo_endh4_should_match()
        {
            ParseResultShouldMatch("[h4]foo[/h4]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource SubtitleTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_base_foo_endbase_should_match()
        {
            ParseResultShouldMatch("[base]foo[/base]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource BaseTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_style_BaseTextBlockStyle_foo_endstyle_should_match()
        {
            ParseResultShouldMatch("[style=BaseTextBlockStyle]foo[/style]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource BaseTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_body_foo_endbody_should_match()
        {
            ParseResultShouldMatch("[body]foo[/body]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource BodyTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_style_BodyTextBlockStyle_foo_endstyle_should_match()
        {
            ParseResultShouldMatch("[style=BodyTextBlockStyle]foo[/style]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource BodyTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_caption_foo_endcaption_should_match()
        {
            ParseResultShouldMatch("[caption]foo[/caption]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource CaptionTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_style_CaptionTextBlockStyle_foo_endstyle_should_match()
        {
            ParseResultShouldMatch("[style=CaptionTextBlockStyle]foo[/style]", "<Run Text=\"foo\" text:Typography.Style=\"{StaticResource CaptionTextBlockStyle}\" />");
        }

        [UITestMethod]
        public void Parse_openbracket_openbracket_foo_closebracket_should_match()
        {
            ParseResultShouldMatch("[[foo]", "<Run Text=\"[foo]\" />");
        }

        [UITestMethod]
        public void Parse_openbracket_openbracket_foo_closebracket_closebracket_should_match()
        {
            ParseResultShouldMatch("[[foo]]", "<Run Text=\"[foo]]\" />");
        }

        [UITestMethod]
        public void Parse_closebracket_should_match()
        {
            ParseResultShouldMatch("]", "<Run Text=\"]\" />");
        }

        [UITestMethod]
        public void Parse_bold_closebracket_endbold_should_match()
        {
            ParseResultShouldMatch("[b]][/b]", "<Run Text=\"]\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_openbracket_openbracket_endbold_should_match()
        {
            ParseResultShouldMatch("[b][[[/b]", "<Run Text=\"[\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_openbracket_endbold_should_match()
        {
            ParseResultShouldMatch("[b][[/b]", "<Run Text=\"[/b]\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_openbracket_should_throw_ParseException()
        {
            Action a = () => ParseResultShouldMatch("[", null);
            a.ShouldThrow<ParseException>().Which.Message.Should().Be("Invalid state");
        }

        [UITestMethod]
        public void Parse_bold_should_match()
        {
            ParseResultShouldMatch("[b]", "");
        }

        [UITestMethod]
        public void Parse_bold_foo_should_match()
        {
            ParseResultShouldMatch("[b]foo", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_italic_foo_should_match()
        {
            ParseResultShouldMatch("[b][i]foo", "<Run Text=\"foo\" FontWeight=\"Bold\" FontStyle=\"Italic\" />");
        }

        [UITestMethod]
        public void Parse_bold_foo_italic_should_match()
        {
            ParseResultShouldMatch("[b]foo[i]", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_foo_italic_foo_should_match()
        {
            ParseResultShouldMatch("[b]foo[i]foo", "<Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"foo\" FontWeight=\"Bold\" FontStyle=\"Italic\" />");
        }

        [UITestMethod]
        public void Parse_bold_foo_italic_foo_endbold_should_match()
        {
            ParseResultShouldMatch("[b]foo[i]foo[/b]", "<Run Text=\"foo\" FontWeight=\"Bold\" /><Run Text=\"foo[/b]\" FontWeight=\"Bold\" FontStyle=\"Italic\" />");
        }

        [UITestMethod]
        public void Parse_bold_foo_attr_should_match()
        {
            ParseResultShouldMatch("[b=foo]", "");
        }

        [UITestMethod]
        public void Parse_bold_quote_foo_attr_quote_should_match()
        {
            ParseResultShouldMatch("[b=\"foo\"]", "");
        }

        [UITestMethod]
        public void Parse_bold_quote_foo_space_bar_attr_quote_should_match()
        {
            ParseResultShouldMatch("[b=\"foo bar\"]", "");
        }

        [UITestMethod]
        public void Parse_bold_foo_space_bar_attr_should_throw_ParseException()
        {
            Action a = () => ParseResultShouldMatch("[b=foo bar]", null);
            a.ShouldThrow<ParseException>().Which.Message.Should().Be("Character mismatch");
        }

        [UITestMethod]
        public void Parse_bold_foo_attr_foo_should_match()
        {
            ParseResultShouldMatch("[b=foo]foo", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_quote_foo_attr_quote_foo_should_match()
        {
            ParseResultShouldMatch("[b=\"foo\"]foo", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_quote_foo_space_bar_attr_quote_foo_should_match()
        {
            ParseResultShouldMatch("[b=\"foo bar\"]foo", "<Run Text=\"foo\" FontWeight=\"Bold\" />");
        }

        [UITestMethod]
        public void Parse_bold_foo_space_bar_attr_foo_should_throw_ParseException()
        {
            Action a = () => ParseResultShouldMatch("[b=foo bar]foo", null);
            a.ShouldThrow<ParseException>().Which.Message.Should().Be("Character mismatch");
        }

        [UITestMethod]
        public void Parse_endbold_should_match()
        {
            ParseResultShouldMatch("[/b]", "<Run Text=\"[/b]\" />");
        }

        [UITestMethod]
        public void Parse_bold_italic_foo_endbold_enditalic_should_match()
        {
            ParseResultShouldMatch("[b][i]foo[/b][/i]", "<Run Text=\"foo[/b]\" FontWeight=\"Bold\" FontStyle=\"Italic\" />");
        }

        [UITestMethod]
        public void Parse_button_foo_endbutton_should_match()
        {
            ParseResultShouldMatch("[button]foo[/button]", "<InlineUIContainer><Button><Button.Content><RichTextBlock IsTextSelectionEnabled=\"false\"><Paragraph><Run Text=\"foo\" /></Paragraph></RichTextBlock></Button.Content></Button></InlineUIContainer>");
        }

        [UITestMethod]
        public void Parse_button_b_foo_endb_endbutton_should_match()
        {
            ParseResultShouldMatch("[button][b]foo[/b][/button]", "<InlineUIContainer><Button><Button.Content><RichTextBlock IsTextSelectionEnabled=\"false\"><Paragraph><Run Text=\"foo\" FontWeight=\"Bold\" /></Paragraph></RichTextBlock></Button.Content></Button></InlineUIContainer>");
        }

        [UITestMethod]
        public void Parse_button_button_foo_endbutton_endbutton_should_match()
        {
            ParseResultShouldMatch("[button][button]foo[/button][/button]", "<InlineUIContainer><Button><Button.Content><RichTextBlock IsTextSelectionEnabled=\"false\"><Paragraph><InlineUIContainer><Button><Button.Content><RichTextBlock IsTextSelectionEnabled=\"false\"><Paragraph><Run Text=\"foo\" /></Paragraph></RichTextBlock></Button.Content></Button></InlineUIContainer></Paragraph></RichTextBlock></Button.Content></Button></InlineUIContainer>");
        }

        [UITestMethod]
        public void Parse_button_icon_foo_endbutton_should_match()
        {
            ParseResultShouldMatch("[button][icon=star] foo[/button]", "<InlineUIContainer><Button><Button.Content><RichTextBlock IsTextSelectionEnabled=\"false\"><Paragraph><Run Text=\"\" FontFamily=\"Segoe MDL2 Assets\" /><Run Text=\" foo\" /></Paragraph></RichTextBlock></Button.Content></Button></InlineUIContainer>");
        }

        [UITestMethod]
        public void Parse_image_should_match()
        {
            ParseResultShouldMatch("[image]", "<InlineUIContainer><Image Stretch=\"None\" /></InlineUIContainer>");
        }

        [UITestMethod]
        public void Parse_image_foopng_image_should_match()
        {
            ParseResultShouldMatch("[image]ms-appx:///foo.png[/image]", "<InlineUIContainer><Image Source=\"ms-appx:///foo.png\" Stretch=\"None\" /></InlineUIContainer>");
        }

        [UITestMethod]
        public void Parse_image_200_foopng_image_should_match()
        {
            ParseResultShouldMatch("[image=200]ms-appx:///foo.png[/image]", "<InlineUIContainer><Image Source=\"ms-appx:///foo.png\" Stretch=\"Uniform\" Width=\"200\" /></InlineUIContainer>");
        }

        [UITestMethod]
        public void Parse_image_200x200_foopng_image_should_match()
        {
            ParseResultShouldMatch("[image=200x200]ms-appx:///foo.png[/image]", "<InlineUIContainer><Image Source=\"ms-appx:///foo.png\" Stretch=\"Uniform\" Width=\"200\" Height=\"200\" /></InlineUIContainer>");
        }

        private void ParseResultShouldMatch(string bbcode, string expectedXaml)
        {
            // wrap expected xaml in Paragraph
            if (string.IsNullOrWhiteSpace(expectedXaml)) {
                expectedXaml = " />";
            }
            else {
                expectedXaml = ">" + expectedXaml + "</Paragraph>";
            }
            
            expectedXaml = "<Paragraph xmlns:nav=\"using:Intense.Navigation\" xmlns:text=\"using:Intense.Text\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"" + expectedXaml;

            var xaml = BBCode.Parse(bbcode);

            xaml.Should().Be(expectedXaml);

            // test if xaml is valid
            var paragraph = XamlReader.Load(xaml);
            paragraph.Should().BeOfType<Paragraph>();
        }
    }
}
