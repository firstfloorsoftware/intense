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
    /// Extension methods for <see cref="ILinkNavigator"/>.
    /// </summary>
    public static class ILinkNavigatorExtensions
    {
        /// <summary>
        /// Attempts to parse a command and parameter from given navigate uri.
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="navigateUri"></param>
        /// <param name="command"></param>
        /// <param name="commandParameter"></param>
        /// <returns></returns>
        public static bool TryParseCommand(this ILinkNavigator navigator, Uri navigateUri, out ICommand command, out string commandParameter)
        {
            if (navigator == null) {
                throw new ArgumentNullException(nameof(navigator));
            }

            command = null;
            commandParameter = null;

            string commandKey;
            if (Navigation.TryParseCommandUri(navigateUri, out commandKey, out commandParameter)) {
                // find command
                return navigator.Commands.TryGetValue(commandKey, out command);
            }
            return false;
        }
    }
}
