using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace $safeprojectname$.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoremPage : Page
    {
        public LoremPage()
        {
            this.InitializeComponent();
        }

        public string Title { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Title = (string)e.Parameter;
        }
    }
}
