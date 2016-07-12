using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Intense.Text.Fonts.Utility;

namespace Intense.Test.UnitTests
{
    [TestClass]
    public class FontUtilityTests
    {
        [TestMethod]
        public void MacintoshGlyphs_Count_should_be_258()
        {
            MacintoshGlyphNames.Count.Should().Be(258);
        }
    }
}
