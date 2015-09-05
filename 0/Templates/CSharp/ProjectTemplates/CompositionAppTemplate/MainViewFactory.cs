using System;
using Windows.ApplicationModel.Core;

namespace $safeprojectname$
{
    public sealed class MainViewFactory : IFrameworkViewSource
    {
        IFrameworkView IFrameworkViewSource.CreateView()
        {
            return new MainView();
        }

        static int Main(string[] args)
        {
            CoreApplication.Run(new MainViewFactory());

            return 0;
        }
    }
}
