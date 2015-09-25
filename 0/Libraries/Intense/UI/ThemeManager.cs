using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Intense.UI
{
    /// <summary>
    /// Manages the app's theme.
    /// </summary>
    public static class ThemeManager
    {
        private static FrameworkElement GetRoot()
        {
            return Window.Current.Content.GetAncestorsAndSelf().OfType<FrameworkElement>().Last();
        }

        /// <summary>
        /// Gets or sets the current theme.
        /// </summary>
        public static ApplicationTheme Theme
        {
            get
            {
                var root = GetRoot();
                if (root.RequestedTheme == ElementTheme.Default) {
                    return Application.Current.RequestedTheme;
                }
                if (root.RequestedTheme == ElementTheme.Dark) {
                    return ApplicationTheme.Dark;
                }
                return ApplicationTheme.Light;
            }
            set
            {
                var elementTheme = ElementTheme.Default;

                if (Application.Current.RequestedTheme != value) {
                    elementTheme = ElementTheme.Light;
                    if (value == ApplicationTheme.Dark) {
                        elementTheme = ElementTheme.Dark;
                    }
                }
                var root = GetRoot();
                root.RequestedTheme = elementTheme;
            }
        }
    }
}
