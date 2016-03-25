using Intense.Presentation;
using Intense.UI;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace $safeprojectname$.Presentation
{
    /// <summary>
    /// A sample settings view model.
    /// </summary>
    public class SettingsViewModel
        : NotifyPropertyChanged
    {
        private DisplayableTheme selectedTheme;
        private SolidColorBrush selectedBrush;
        private bool useSystemAccentColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/>.
        /// </summary>
        public SettingsViewModel()
        {
            this.Brushes = AccentColors.Windows10.Select(c => new SolidColorBrush(c)).ToImmutableList();
            this.Themes = ImmutableList.Create(
                new DisplayableTheme("Dark", ApplicationTheme.Dark),
                new DisplayableTheme("Light", ApplicationTheme.Light));

            // ensure viewmodel state reflects actual appearance
            var manager = AppearanceManager.GetForCurrentView();
            this.selectedTheme = this.Themes.FirstOrDefault(t => t.Theme == manager.Theme);

            if (AppearanceManager.AccentColor == null)
            {
                this.useSystemAccentColor = true;
            }
            else
            {
                this.selectedBrush = this.Brushes.FirstOrDefault(b => b.Color == AppearanceManager.AccentColor);
            }
        }

        /// <summary>
        /// Gets the brushes.
        /// </summary>
        public IReadOnlyList<SolidColorBrush> Brushes { get; }

        /// <summary>
        /// Gets or sets the selected brush.
        /// </summary>
        public SolidColorBrush SelectedBrush
        {
            get { return this.selectedBrush; }
            set
            {
                if (Set(ref this.selectedBrush, value) && !this.useSystemAccentColor && value != null)
                {
                    AppearanceManager.AccentColor = value.Color;
                }
            }
        }

        /// <summary>
        /// Gets the collection of themes.
        /// </summary>
        public IReadOnlyList<DisplayableTheme> Themes { get; }
        /// <summary>
        /// Gets or sets the selected theme.
        /// </summary>
        public DisplayableTheme SelectedTheme
        {
            get { return this.selectedTheme; }
            set
            {
                if (Set(ref this.selectedTheme, value) && value != null)
                {
                    AppearanceManager.GetForCurrentView().Theme = value.Theme;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the system accent color.
        /// </summary>
        public bool UseSystemAccentColor
        {
            get { return this.useSystemAccentColor; }
            set
            {
                if (Set(ref this.useSystemAccentColor, value))
                {
                    if (value)
                    {
                        AppearanceManager.AccentColor = null;
                    }
                    else if (this.SelectedBrush != null) 
                    {
                        AppearanceManager.AccentColor = this.SelectedBrush.Color;
                    }
                }
            }
        }
    }
}
