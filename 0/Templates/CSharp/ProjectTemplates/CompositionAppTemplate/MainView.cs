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
        private ContainerVisual root;
        private SpriteVisual background;
        private SpriteVisual target;

        private void InitializeComposition()
        {
            // setup compositor and root visual
            this.compositor = new Compositor();
            this.root = this.compositor.CreateContainerVisual();

            // associate with the CoreWindow
            this.compositionTarget = this.compositor.CreateTargetForCurrentView();
            this.compositionTarget.Root = this.root;

            // add a solid color background
            this.background = this.compositor.CreateSpriteVisual();
            this.background.Brush = this.compositor.CreateColorBrush(Colors.LightGreen);
            this.root.Children.InsertAtBottom(this.background);

            // create green square
            var colorVisual = this.compositor.CreateSpriteVisual();
            colorVisual.Brush = this.compositor.CreateColorBrush(Colors.Green);
            colorVisual.Size = new Vector2(150.0f, 150.0f);
            colorVisual.Offset = new Vector3(250.0f, 250.0f, 0.0f);
            colorVisual.CenterPoint = new Vector3(75.0f, 75.0f, 0.0f);
            this.target = colorVisual;
            this.root.Children.InsertAtTop(this.target);

            // animate square
            Animate(this.target);

            UpdateSize();
        }

        private void Animate(Visual visual)
        {
            var easing = this.compositor.CreateCubicBezierEasingFunction(new Vector2(.5f, .1f), new Vector2(.5f, .75f));
            var animation = this.compositor.CreateScalarKeyFrameAnimation();

            animation.InsertKeyFrame(0.00f, 0.00f, easing);
            animation.InsertKeyFrame(1.00f, 360.0f, easing);

            animation.Duration = TimeSpan.FromMilliseconds(2000);
            animation.IterationBehavior = AnimationIterationBehavior.Forever;

            visual.StartAnimation("RotationAngleinDegrees", animation);
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
