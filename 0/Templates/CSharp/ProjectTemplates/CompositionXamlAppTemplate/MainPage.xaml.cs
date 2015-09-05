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
        private ContainerVisual container;
        private SolidColorVisual background;
        private Compositor compositor;

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
            this.container = (ContainerVisual)ElementCompositionPreview.GetContainerVisual(this.Host);
            this.compositor = container.Compositor;

            this.background = this.compositor.CreateSolidColorVisual();
            this.background.Color = Colors.LightGreen;

            this.container.Children.InsertAtBottom(background);
            UpdateSize();
        }

        private void Host_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSize();
        }

        private void Host_Unloaded(object sender, RoutedEventArgs e)
        {
            this.background.Dispose();
            this.container.Dispose();
            this.compositor.Dispose();
        }
    }
}