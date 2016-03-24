using Intense.Presentation;
using Intense.UI;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Intense.TestApp.ViewModels
{
    /// <summary>
    /// The view model for managing the accent color.
    /// </summary>
    public class ColorViewModel
        : NotifyPropertyChanged, IAppearanceManagerEventSink
    {
        private SolidColorBrush selectedBrush;
        private bool useSystemAccentColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorViewModel"/>.
        /// </summary>
        public ColorViewModel()
        {
            this.Brushes = AccentColors.Windows10.Select(c => new SolidColorBrush(c)).ToImmutableList();

            SyncSelectedBrush();
        }

        private void SyncSelectedBrush()
        {
            if (this.UseSystemAccentColor = !AppearanceManager.AccentColor.HasValue) {
                this.SelectedBrush = null;
            }
            else {
                this.SelectedBrush = this.Brushes.FirstOrDefault(b => b.Color == AppearanceManager.AccentColor);
            }
        }

        void IAppearanceManagerEventSink.OnAccentColorChanged(object source, EventArgs e)
        {
            SyncSelectedBrush();
        }

        void IAppearanceManagerEventSink.OnSystemAccentColorChanged(object source, EventArgs e)
        {
        }

        void IAppearanceManagerEventSink.OnThemeChanged(object source, EventArgs e)
        {
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
                if (Set(ref this.selectedBrush, value)) {
                    AppearanceManager.AccentColor = this.useSystemAccentColor ? null : value?.Color;
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
                if (Set(ref this.useSystemAccentColor, value)) {
                    AppearanceManager.AccentColor = value ? null : this.SelectedBrush?.Color;
                }
            }
        }
    }
}
