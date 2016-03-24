using Intense.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Intense.TestApp.ViewModels
{
    /// <summary>
    /// Represents a display theme.
    /// </summary>
    public class DisplayableTheme
        : Displayable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayableTheme"/>.
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="theme"></param>
        public DisplayableTheme(string displayName, ApplicationTheme theme)
        {
            this.DisplayName = displayName;
            this.Theme = theme;
        }

        /// <summary>
        /// Gets the them.
        /// </summary>
        public ApplicationTheme Theme { get; }
    }
}
