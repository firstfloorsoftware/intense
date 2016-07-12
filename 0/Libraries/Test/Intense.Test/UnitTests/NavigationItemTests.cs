using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using FluentAssertions;
using Intense.Presentation;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;

namespace Intense.Test.UnitTests
{
    [TestClass]
    public class NavigationItemTests
    {
        [UITestMethod]
        public void NavigationStructure_GetDescendants_should_have_count_27()
        {
            var structure = new NavigationStructure();
            structure.GetDescendants().Should().HaveCount(27);
        }

        [UITestMethod]
        public void NavigationStructure_GetDescendantsAndSelf_should_have_count_28()
        {
            var structure = new NavigationStructure();
            structure.GetDescendantsAndSelf().Should().HaveCount(28);
        }

        [UITestMethod]
        public void NavigationStructure_root_should_have_5_children()
        {
            var structure = new NavigationStructure();
            structure.Items.Should().HaveCount(5);
        }

        [UITestMethod]
        public void NavigationStructure_root_should_have_grandchildren()
        {
            var structure = new NavigationStructure();
            structure.HasGrandchildren().Should().BeTrue();
        }
    }
}
