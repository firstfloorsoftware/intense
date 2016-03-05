using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Intense.Presentation
{
    /// <summary>
    /// A set of commands for managing the current application view.
    /// </summary>
    public class ApplicationViewCommands
    {
        class WeakSizeChangedEventHandler
        {
            private WeakReference<ApplicationViewCommands> reference;

            public WeakSizeChangedEventHandler(Window source, ApplicationViewCommands target)
            {
                this.reference = new WeakReference<ApplicationViewCommands>(target);
                source.SizeChanged += OnSizeChanged;
            }

            private void OnSizeChanged(object source, WindowSizeChangedEventArgs e)
            {
                ApplicationViewCommands target;
                if (this.reference.TryGetTarget(out target)) {
                    target.UpdateCommandStates();
                }
                else {
                    ((Window)source).SizeChanged -= OnSizeChanged;
                }
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewCommands"/> class.
        /// </summary>
        public ApplicationViewCommands()
        {
            var view = ApplicationView.GetForCurrentView();

            this.EnterFullScreenModeCommand = new RelayCommand(o => view.TryEnterFullScreenMode(), o => !view.IsFullScreenMode);
            this.ExitFullScreenModeCommand = new RelayCommand(o => view.ExitFullScreenMode(), o => view.IsFullScreenMode);

            new WeakSizeChangedEventHandler(Window.Current, this);
        }

        private void UpdateCommandStates()
        {
            this.EnterFullScreenModeCommand.OnCanExecuteChanged();
            this.ExitFullScreenModeCommand.OnCanExecuteChanged();
        }

        /// <summary>
        /// The command for entering full screen mode.
        /// </summary>
        public Command EnterFullScreenModeCommand { get; }
        /// <summary>
        /// The command for exiting full screen mode.
        /// </summary>
        public Command ExitFullScreenModeCommand { get; }
    }
}
