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
    /// Provides data for the <see cref="BBCodeBlock.NavigationFailed"/> event.
    /// </summary>
    public class LinkNavigationFailedEventArgs
        : LinkNavigationEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkNavigationFailedEventArgs"/> class.
        /// </summary>
        /// <param name="navigateUri"></param>
        /// <param name="source"></param>
        /// <param name="exception"></param>
        public LinkNavigationFailedEventArgs(Uri navigateUri, DependencyObject source, Exception exception)
            : base(navigateUri, source)
        {
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the exception that occured.
        /// </summary>
        public Exception Exception { get; private set; }
    }
}
