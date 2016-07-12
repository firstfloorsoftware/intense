using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Intense.UI.Controls
{
    /// <summary>
    /// Represents a page capable of displaying rich text formatted BBCode.
    /// </summary>
    public sealed partial class BBCodePage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BBCodePage"/>.
        /// </summary>
        public BBCodePage()
        {
            this.InitializeComponent();
            Debug.WriteLine("BBCodePage()");
        }

        /// <summary>
        /// Invoked when the <see cref="BBCodePage"/> is loaded and becomes the current source of a parent <see cref="Frame"/>.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var source = e.Parameter as Uri;
            if (source == null) {
                var uriStr = e.Parameter as string;
                if (uriStr != null) {
                    Uri.TryCreate(uriStr, UriKind.Absolute, out source);
                }
            }
            if (source != null) {
                this.BBCodeBlock.BBCodeSource = source;
            }
            else {
                this.BBCodeBlock.BBCode = "No or invalid page parameter";
            }
        }
    }
}
