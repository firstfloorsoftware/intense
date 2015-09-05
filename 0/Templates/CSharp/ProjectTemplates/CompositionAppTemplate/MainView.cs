using System;
using System.Numerics;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;

namespace $safeprojectname$
{
    class MainView : IFrameworkView
    {
        private CoreApplicationView view;
        private CoreWindow window;
        private Compositor compositor;
        private CompositionTarget compositionTarget;
        private ContainerVisual rootVisual;
        private SolidColorVisual background;

        private void InitializeComposition()
        {
            // setup compositor and root visual
            this.compositor = new Compositor();
            this.rootVisual = this.compositor.CreateContainerVisual();

            // associate with the CoreWindow
            this.compositionTarget = this.compositor.CreateTargetForCurrentView();
            this.compositionTarget.Root = this.rootVisual;

            // add a solid color background
            this.background = this.compositor.CreateSolidColorVisual();
            this.background.Color = Colors.LightGreen;
            this.rootVisual.Children.InsertAtBottom(this.background);

            UpdateSize();
        }

        private void UpdateSize()
        {
            if (this.background != null && this.window != null) {
                this.background.Size = new Vector2((float)this.window.Bounds.Width, (float)this.window.Bounds.Height);
            }
        }

        private void OnWindowSizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            UpdateSize();
        }

        /// <summary>
        /// A method for app view initialization, which is called when an app object is launched.
        /// </summary>
        /// <param name="view">The default view provided by the app object.</param>
        void IFrameworkView.Initialize(CoreApplicationView view)
        {
            this.view = view;
        }

        /// <summary>
        /// Loads or activates any external resources used by the app view before Run is called.
        /// </summary>
        /// <param name="entryPoint">The name of the entry point method for the activated type.</param>
        void IFrameworkView.Load(string entryPoint)
        {
            // not used
        }

        /// <summary>
        /// Starts the app view.
        /// </summary>
        void IFrameworkView.Run()
        {
            this.window.Activate();
            this.window.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessUntilQuit);
        }

        /// <summary>
        /// Sets the current window for the app object's view.
        /// </summary>
        /// <param name="window">The current window for the app object.</param>
        void IFrameworkView.SetWindow(CoreWindow window)
        {
            this.window = window;
            this.window.SizeChanged += OnWindowSizeChanged;
            InitializeComposition();
        }

        /// <summary>
        /// Uninitializes the app view.
        /// </summary>
        void IFrameworkView.Uninitialize()
        {
            this.window = null;
            this.view = null;
        }
    }
}
