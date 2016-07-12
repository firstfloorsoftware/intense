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
    /// The contract for BBCode parser settings.
    /// </summary>
    public interface IBBCodeParserSettings
    {
        /// <summary>
        /// Gets the link navigator.
        /// </summary>
        ILinkNavigator Navigator { get; }
        /// <summary>
        /// Get the base uri for resolving relative content.
        /// </summary>
        Uri BaseUri { get; }
    }
}
