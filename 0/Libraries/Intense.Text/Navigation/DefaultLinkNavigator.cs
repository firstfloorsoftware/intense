using Intense.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace Intense.Navigation
{
    /// <summary>
    /// The default link navigator with support for navigating frame pages, external pages, and executing commands.
    /// </summary>
    public class DefaultLinkNavigator
        : ILinkNavigator
    {
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        /// <summary>
        /// Represents a collection of commands keyed by a uri.
        /// </summary>
        public Dictionary<string, ICommand> Commands
        {
            get { return this.commands; }
        }

        /// <summary>
        /// Determines whether navigation to specified link is enabled.
        /// </summary>
        /// <param name="navigateUri"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool CanNavigate(Uri navigateUri, DependencyObject context)
        {
            if (navigateUri == null) {
                throw new ArgumentNullException(nameof(navigateUri));
            }
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            if (!navigateUri.IsAbsoluteUri) {
                return false;
            }

            // internal page
            if (navigateUri.Scheme == Navigation.UriSchemeMsAppx) {
                return context != null;
            }
            // commmand
            if (navigateUri.Scheme == Navigation.UriSchemeCommand) {
                ICommand command;
                string commandParameter;
                if (this.TryParseCommand(navigateUri, out command, out commandParameter)) {
                    return command.CanExecute(commandParameter);
                }
            }

            // external page
            return navigateUri.Scheme != Navigation.UriSchemeFile;        // file navigation is explicitly forbidden (UWP limitation)
        }

        /// <summary>
        /// Navigates to specified link.
        /// </summary>
        /// <param name="navigateUri"></param>
        /// <param name="context"></param>
        public async virtual Task NavigateAsync(Uri navigateUri, DependencyObject context)
        {
            if (navigateUri == null) {
                throw new ArgumentNullException(nameof(navigateUri));
            }
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (!navigateUri.IsAbsoluteUri) {
                throw new ArgumentException(ResourceHelper.GetString("RelativeUriNotSupported", navigateUri));
            }

            if (navigateUri.Scheme == Navigation.UriSchemeMsAppx) {
                // internal
                if (context == null) {
                    throw new ArgumentException(ResourceHelper.GetString("InternalNavigationFailedNoContext"));
                }

                Type pageType;
                string pageParameter;
                string targetFrame;

                if (Navigation.TryParsePageUri(navigateUri, out pageType, out pageParameter, out targetFrame)) {
                    var frame = Navigation.FindFrame(targetFrame, context);

                    if (frame == null) {
                        throw new InvalidOperationException(ResourceHelper.GetString("FrameNotFound", targetFrame));
                    }

                    frame.Navigate(pageType, pageParameter);
                }
                else {
                    throw new ArgumentException(ResourceHelper.GetString("InvalidUri", navigateUri));
                }
            }
            else if (navigateUri.Scheme == Navigation.UriSchemeCommand) {
                // command
                ICommand command;
                string commandParameter;
                if (this.TryParseCommand(navigateUri, out command, out commandParameter)) {
                    command.Execute(commandParameter);
                }
                else {
                    throw new ArgumentException(ResourceHelper.GetString("InvalidUri", navigateUri));
                }
            }
            else {
                // external
                await Launcher.LaunchUriAsync(navigateUri);
            }
        }
    }
}
