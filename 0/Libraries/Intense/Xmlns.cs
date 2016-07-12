using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Intense
{
    /// <summary>
    /// Identifies a set of well-known XML namespaces.
    /// </summary>
    public static class Xmlns
    {
        /// <summary>
        /// Identifies the XAML presentation namespace.
        /// </summary>
        public static XNamespace XamlPresentation => XNamespace.Get("http://schemas.microsoft.com/winfx/2006/xaml/presentation");
        /// <summary>
        /// Identifies the XAML namespace.
        /// </summary>
        public static XNamespace Xaml => XNamespace.Get("http://schemas.microsoft.com/winfx/2006/xaml");
    }
}
