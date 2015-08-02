using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;

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
