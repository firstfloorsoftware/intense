using Intense.Presentation;
using Intense.UI;
using Intense.UI.Controls;
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
    /// The command for navigating links.
    /// </summary>
    public class NavigationCommand
        : Command
    {
        private BBCodeBlock owner;
        private Uri navigateUri;

        /// <summary>
        /// Gets or sets the navigate uri.
        /// </summary>
        /// <remarks>We really want NavigateUri to be of type Uri, but XamlReader doesn't play along.</remarks>
        public string NavigateUri
        {
            get { return this.navigateUri?.OriginalString; }
            set
            {
                // can only be assigned once
                if (this.navigateUri != null) {
                    throw new InvalidOperationException();
                }
                if (value == null || !Uri.TryCreate(value, UriKind.Absolute, out this.navigateUri)) {
                    this.navigateUri = null;
                }
            }
        }
        
        internal void Initialize(BBCodeBlock owner)
        {
            this.owner = owner;

            // if command, listen for commandstate changes
            ICommand command;
            string commandParameter;
            if (this.navigateUri != null && this.owner.Navigator.TryParseCommand(this.navigateUri, out command, out commandParameter)) {
                // TODO: weak event listener!
                command.CanExecuteChanged += OnCommandCanExecuteChanged;
            }

            OnCanExecuteChanged();
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            // propagate event
            OnCanExecuteChanged();
        }

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return this.owner?.CanNavigate(this.navigateUri, parameter as DependencyObject) ?? false;
        }

        /// <summary>
        /// Performs the navigation.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter)) {
                this.owner.Navigate(this.navigateUri, parameter as DependencyObject);
            }
        }
    }
}
