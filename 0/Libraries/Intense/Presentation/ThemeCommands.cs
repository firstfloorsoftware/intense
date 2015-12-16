using Intense.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Intense.Presentation
{
    /// <summary>
    /// Provides commands for modifying the app's theme at runtime.
    /// </summary>
    public class ThemeCommands
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeCommands"/> class.
        /// </summary>
        public ThemeCommands()
        {
            this.SetDarkThemeCommand = new RelayCommand(o => ThemeManager.Theme = ApplicationTheme.Dark);
            this.SetLightThemeCommand = new RelayCommand(o => ThemeManager.Theme = ApplicationTheme.Light);
            this.ToggleThemeCommand = new RelayCommand(o => ThemeManager.Theme = ThemeManager.Theme == ApplicationTheme.Dark ? ApplicationTheme.Light : ApplicationTheme.Dark);
        }

        /// <summary>
        /// The command for setting the dark theme.
        /// </summary>
        public ICommand SetDarkThemeCommand { get; }
        /// <summary>
        /// The command for setting the light theme.
        /// </summary>
        public ICommand SetLightThemeCommand { get; }
        /// <summary>
        /// The command for toggling between the light and dark theme.
        /// </summary>
        public ICommand ToggleThemeCommand { get; }
    }
}
