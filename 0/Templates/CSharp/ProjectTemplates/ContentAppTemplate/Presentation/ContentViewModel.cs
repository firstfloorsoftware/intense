using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace $safeprojectname$.Presentation
{
    public class ContentViewModel : NotifyPropertyChanged
    {
        private bool isPaneOpen;
        private bool isPanePinned;

        public ContentViewModel()
        {
            this.ShowPaneCommand = new Command(() => this.IsPaneOpen = true);
            this.HidePaneCommand = new Command(() => this.IsPaneOpen = false);
            this.PinPaneCommand = new Command(() => this.IsPanePinned = true);
        }

        public ICommand ShowPaneCommand { get; }
        public ICommand HidePaneCommand { get; }
        public ICommand PinPaneCommand { get; }

        public bool IsPaneOpen
        {
            get { return this.isPaneOpen; }
            set
            {
                if (Set(ref this.isPaneOpen, value) && !value) {
                    // unpane pane when closed
                    this.IsPanePinned = false;
                }
            }
        }

        public bool IsPanePinned
        {
            get { return this.isPanePinned; }
            set
            {
                if (Set(ref this.isPanePinned, value)) {
                    OnPropertyChanged("PaneDisplayMode");
                }
            }
        }

        public SplitViewDisplayMode PaneDisplayMode
        {
            get
            {
                return this.isPanePinned ? SplitViewDisplayMode.Inline : SplitViewDisplayMode.Overlay;
            }
        }
    }
}
