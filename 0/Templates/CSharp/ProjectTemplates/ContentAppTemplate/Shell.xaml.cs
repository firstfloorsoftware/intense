using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using $safeprojectname$.Presentation;

namespace $safeprojectname$
{
    public sealed partial class Shell : UserControl
    {
        public Shell()
        {
            this.InitializeComponent();

            this.ViewModel = new ContentViewModel();
        }

        public ContentViewModel ViewModel { get; }
    }
}
