using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Intense.Navigation
{
    /// <summary>
    /// The hyperlink navigator contract
    /// </summary>
    public interface ILinkNavigator
    {
        /// <summary>
        /// Represents a collection of commands keyed by a uri.
        /// </summary>
        Dictionary<string, ICommand> Commands { get; }
        /// <summary>
        /// Determines whether navigation to specified link is enabled.
        /// </summary>
        /// <param name="navigateUri"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        bool CanNavigate(Uri navigateUri, DependencyObject context);
        /// <summary>
        /// Navigates to specified link.
        /// </summary>
        /// <param name="navigateUri"></param>
        /// <param name="context"></param>
        Task NavigateAsync(Uri navigateUri, DependencyObject context);
    }
}
