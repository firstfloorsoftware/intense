using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace $safeprojectname$
{
    /// <summary>
    /// A page with a container visual having a solid color background.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Compositor compositor;
        private Visual root;
        private SpriteVisual background;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void UpdateSize()
        {
            if (this.background != null && this.Host != null) {
                this.background.Size = new Vector2((float)this.Host.ActualWidth, (float)this.Host.ActualHeight);
            }
        }

        private void Host_Loaded(object sender, RoutedEventArgs e)
        {
            this.root = ElementCompositionPreview.GetElementVisual(this.Host);
            this.compositor = this.root.Compositor;

            this.background = this.compositor.CreateSpriteVisual();
            this.background.Brush = this.compositor.CreateColorBrush(Colors.LightGreen);
            ElementCompositionPreview.SetElementChildVisual(this.Host, this.background);

            UpdateSize();
        }

        private void Host_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSize();
        }
    }
}