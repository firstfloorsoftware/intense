using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;

namespace Intense.UI.Controls
{
    /// <summary>
    /// Provides data for the <see cref="BBCodeBlock.Navigated"/> event.
    /// </summary>
    public class LinkNavigationEventArgs
        : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkNavigationEventArgs"/> class.
        /// </summary>
        /// <param name="navigateUri"></param>
        /// <param name="source"></param>
        public LinkNavigationEventArgs(Uri navigateUri, DependencyObject source)
        {
            this.NavigateUri = navigateUri;
            this.Source = source;
        }

        /// <summary>
        /// Gets the url.
        /// </summary>
        public Uri NavigateUri { get; private set; }
        /// <summary>
        /// Gets the source object that initiated the navigation.
        /// </summary>
        public DependencyObject Source { get; private set; }
    }
}
