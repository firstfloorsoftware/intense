using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace $safeprojectname$.Presentation
{
    public class ShellViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();
        private MenuItem selectedMenuItem;
        private ObservableCollection<MenuItem> bottomMenuItems = new ObservableCollection<MenuItem>();
        private MenuItem selectedBottomMenuItem;
        private bool isSplitViewPaneOpen;

        public ShellViewModel()
        {
            this.ToggleSplitViewPaneCommand = new Command(() => this.IsSplitViewPaneOpen = !this.IsSplitViewPaneOpen);

            // open splitview pane in wide state
            this.IsSplitViewPaneOpen = IsWideState();
        }

        public ICommand ToggleSplitViewPaneCommand { get; private set; }

        public bool IsSplitViewPaneOpen
        {
            get { return this.isSplitViewPaneOpen; }
            set { Set(ref this.isSplitViewPaneOpen, value); }
        }

        public MenuItem SelectedMenuItem
        {
            get { return this.selectedMenuItem; }
            set
            {
                if (Set(ref this.selectedMenuItem, value) && value != null) {
                    OnSelectedMenuItemChanged(true);
                }
            }
        }

        public MenuItem SelectedBottomMenuItem
        {
            get { return this.selectedBottomMenuItem; }
            set
            {
                if (Set(ref this.selectedBottomMenuItem, value) && value != null) {
                    OnSelectedMenuItemChanged(false);
                }
            }
        }

        public Type SelectedPageType
        {
            get
            {
                return (this.selectedMenuItem ?? this.selectedBottomMenuItem)?.PageType;
            }
            set
            {
                // select associated menu item
                this.SelectedMenuItem = this.menuItems.FirstOrDefault(m => m.PageType == value);
                this.SelectedBottomMenuItem = this.bottomMenuItems.FirstOrDefault(m => m.PageType == value);
            }
        }

        public ObservableCollection<MenuItem> MenuItems
        {
            get { return this.menuItems; }
        }

        public ObservableCollection<MenuItem> BottomMenuItems
        {
            get { return this.bottomMenuItems; }
        }

        private void OnSelectedMenuItemChanged(bool top)
        {
            if (top) {
                this.SelectedBottomMenuItem = null;
            }
            else {
                this.SelectedMenuItem = null;
            }
            OnPropertyChanged("SelectedPageType");

            // auto-close split view pane (only when not in widestate)
            if (!IsWideState()) {
                this.IsSplitViewPaneOpen = false;
            }
        }

        // a helper determining whether we are in a wide window state
        // mvvm purists probably don't appreciate this approach
        private bool IsWideState()
        {
            return Window.Current.Bounds.Width >= 1024;
        }
    }
}
