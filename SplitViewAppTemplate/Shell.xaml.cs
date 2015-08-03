using $safeprojectname$.Pages;
using $safeprojectname$.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace $safeprojectname$
{
    public sealed partial class Shell : UserControl
    {
        public Shell()
        {
            this.InitializeComponent();

            var vm = new ShellViewModel();
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Welcome", PageType = typeof(WelcomePage) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 1", PageType = typeof(Page1) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 2", PageType = typeof(Page2) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 3", PageType = typeof(Page3) });

            // select the first menu item
            vm.SelectedMenuItem = vm.MenuItems.First();

            this.ViewModel = vm;
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                return this.Frame;
            }
        }
    }
}
