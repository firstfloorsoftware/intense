using Intense.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Intense.Text.Parsers.BBCode
{
    /// <summary>
    /// Specifies a set of features for the BBCode parser.
    /// </summary>
    public class BBCodeParserSettings : IBBCodeParserSettings
    {
        /// <summary>
        /// Gets or sets the link navigator.
        /// </summary>
        public ILinkNavigator Navigator { get; set; }
        /// <summary>
        /// Gets or sets the base uri for resolving relative content.
        /// </summary>
        public Uri BaseUri { get; set; }
    }
}
