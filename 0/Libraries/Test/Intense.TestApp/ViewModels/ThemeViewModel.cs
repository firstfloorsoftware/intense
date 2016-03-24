using Intense.Presentation;
using Intense.UI;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Intense.TestApp.ViewModels
{
    /// <summary>
    /// The view model for managing the theme.
    /// </summary>
    public class ThemeViewModel
        : NotifyPropertyChanged, IAppearanceManagerEventSink
    {
        private DisplayableTheme selectedTheme;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeViewModel"/>.
        /// </summary>
        public ThemeViewModel()
        {
            this.Themes = ImmutableList.Create(
                new DisplayableTheme("Dark", ApplicationTheme.Dark),
                new DisplayableTheme("Light", ApplicationTheme.Light));

            SyncSelectedTheme();
        }

        private void SyncSelectedTheme()
        {
            var manager = AppearanceManager.GetForCurrentView();
            this.SelectedTheme = this.Themes.FirstOrDefault(t => t.Theme == manager.Theme);
        }

        void IAppearanceManagerEventSink.OnAccentColorChanged(object source, EventArgs e)
        {
        }

        void IAppearanceManagerEventSink.OnSystemAccentColorChanged(object source, EventArgs e)
        {
        }

        void IAppearanceManagerEventSink.OnThemeChanged(object source, EventArgs e)
        {
            SyncSelectedTheme();
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
                if (value != null && Set(ref this.selectedTheme, value)) {
                    AppearanceManager.GetForCurrentView().Theme = value.Theme;
                }
            }
        }
    }
}
