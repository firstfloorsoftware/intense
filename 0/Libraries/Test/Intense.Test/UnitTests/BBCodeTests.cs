using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Intense.Text;

namespace Intense.Test.UnitTests
{
    [TestClass]
    public class BBCodeTests
    {
        [TestMethod]
        public void BBCode_SupportedTags_should_contain_all_supported_tags()
        {
            var expectedTags = new[] {
                "b",
                "base",
                "body",
                "bold",
                "button",
                "c",
                "caption",
                "code",
                "color",
                "f",
                "font",
                "h1",
                "h2",
                "h3",
                "h4",
                "header",
                "i",
                "icon",
                "img",
                "image",
                "italic",
                "path",
                "size",
                "style",
                "sub",
                "subheader",
                "subtitle",
                "super",
                "title",
                "u",
                "underline",
                "url" };

            var supportedTags = BBCode.GetSupportedTags();
            supportedTags.Should().Contain(expectedTags);
            supportedTags.Should().HaveCount(expectedTags.Length);
        }

        [TestMethod]
        public void Tags_Bold_and_BoldAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagBold, BBCode.TagBoldAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagBoldAcronym, BBCode.TagBold).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Color_and_ColorAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagColor, BBCode.TagColorAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagColorAcronym, BBCode.TagColor).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Font_and_FontAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagFont, BBCode.TagFontAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagFontAcronym, BBCode.TagFont).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Header_and_HeaderAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagHeader, BBCode.TagHeaderAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagHeaderAcronym, BBCode.TagHeader).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Image_and_ImageAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagImage, BBCode.TagImageAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagImageAcronym, BBCode.TagImage).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Italic_and_ItalicAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagItalic, BBCode.TagItalicAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagItalicAcronym, BBCode.TagItalic).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Subheader_and_SubheaderAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagSubheader, BBCode.TagSubheaderAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagSubheaderAcronym, BBCode.TagSubheader).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Subtitle_and_SubtitleAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagSubtitle, BBCode.TagSubtitleAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagSubtitleAcronym, BBCode.TagSubtitle).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Title_and_TitleAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagTitle, BBCode.TagTitleAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagTitleAcronym, BBCode.TagTitle).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Underline_and_UnderlineAcronym_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagUnderline, BBCode.TagUnderlineAcronym).Should().BeTrue();
            BBCode.AreEquivalentTags(BBCode.TagUnderlineAcronym, BBCode.TagUnderline).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Bold_and_Bold_are_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagBold, BBCode.TagBold).Should().BeTrue();
        }

        [TestMethod]
        public void Tags_Bold_and_Italic_are_not_equivalent()
        {
            BBCode.AreEquivalentTags(BBCode.TagBold, BBCode.TagItalic).Should().BeFalse();
        }
    }
}
