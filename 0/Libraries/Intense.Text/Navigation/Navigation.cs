using Intense.Resources;
using Intense.UI;
using Intense.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Documents;

namespace Intense.Navigation
{
    /// <summary>
    /// Provides hyperlink navigation support.
    /// </summary>
    public static class Navigation
    {
        /// <summary>
        /// Identifies the current frame.
        /// </summary>
        public const string FrameSelf = "_self";
        /// <summary>
        /// Identifies the top frame.
        /// </summary>
        public const string FrameTop = "_top";
        /// <summary>
        /// Identifies the parent of the current frame.
        /// </summary>
        public const string FrameParent = "_parent";

        /// <summary>
        /// Specifies that the URI is a pointer to a file.
        /// </summary>
        public const string UriSchemeFile = "file";
        /// <summary>
        /// Specifies that the URI is accessed through the File Transfer Protocol (FTP).
        /// </summary>
        public const string UriSchemeFtp = "ftp";
        /// <summary>
        /// Specifies that the URI is accessed through the Hypertext Transfer Protocol (HTTP). 
        /// </summary>
        public const string UriSchemeHttp = "http";
        /// <summary>
        /// Specifies that the URI is accessed through the Secure Hypertext Transfer Protocol (HTTPS)
        /// </summary>
        public const string UriSchemeHttps = "https";
        /// <summary>
        /// Specifies that the URI is an e-mail address and is accessed through the Simple Mail Transport Protocol (SMTP). 
        /// </summary>
        public const string UriSchemeMailto = "mailto";
        /// <summary>
        /// Specifies that the URI is a pointer to a file in the app's data folders.
        /// </summary>
        public const string UriSchemeMsAppData = "ms-appdata";
        /// <summary>
        /// Specifies that the URI is a pointer to a file in the app's package.
        /// </summary>
        public const string UriSchemeMsAppx = "ms-appx";
        /// <summary>
        /// Specifies that the URI is a pointer to an app resource.
        /// </summary>
        public const string UriSchemeMsResource = "ms-resource";
        /// <summary>
        /// Specifies that the URI is a pointer to a command.
        /// </summary>
        public const string UriSchemeCommand = "cmd";

        /// <summary>
        /// Identifies the Command attached property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(NavigationCommand), typeof(Navigation), new PropertyMetadata(null, OnCommandChanged));

        private static void OnCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var hyperlink = o as Hyperlink;
            if (hyperlink == null) {
                // ignore
                return;
            }

            if (args.NewValue == null) {
                hyperlink.Click -= OnHyperlinkClick;
            }
            else {
                hyperlink.Click += OnHyperlinkClick;
            }
        }

        private static void OnHyperlinkClick(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            var command = GetCommand(sender);
            if (command?.CanExecute(sender) ?? false) {
                command.Execute(sender);
            }
        }

        /// <summary>
        /// Retrieves the command for specified object.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static NavigationCommand GetCommand(DependencyObject o)
        {
            return (NavigationCommand)o?.GetValue(CommandProperty);
        }

        /// <summary>
        /// Assigns a command to specified object.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="value"></param>
        public static void SetCommand(DependencyObject o, NavigationCommand value)
        {
            o?.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Finds the frame identified with given name in the specified context.
        /// </summary>
        /// <param name="name">The frame name.</param>
        /// <param name="context">The framework element providing the context for finding a frame.</param>
        /// <returns>The frame or null if the frame could not be found.</returns>

        public static Frame FindFrame(string name, DependencyObject context)
        {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            // collect all ancestor frames
            var frames = context.GetAncestorsAndSelf().OfType<Frame>().ToArray();

            if (name == null || name == FrameSelf) {
                // find first ancestor frame
                return frames.FirstOrDefault();
            }
            if (name == FrameParent) {
                // find parent frame
                return frames.Skip(1).FirstOrDefault();
            }
            if (name == FrameTop) {
                // find top-most frame
                return frames.LastOrDefault();
            }

            // find ancestor frame having a name matching the target
            return frames.FirstOrDefault(f => f.Name == name);
        }

        /// <summary>
        /// Attempts to parse a page uri.
        /// </summary>
        /// <param name="navigateUri"></param>
        /// <param name="pageType"></param>
        /// <param name="pageParameter"></param>
        /// <param name="targetFrame"></param>
        /// <returns></returns>
        public static bool TryParsePageUri(Uri navigateUri, out Type pageType, out string pageParameter, out string targetFrame)
        {
            if (navigateUri == null) {
                throw new ArgumentNullException(nameof(navigateUri));
            }

            pageType = null;
            pageParameter = null;
            targetFrame = null;

            if (!navigateUri.IsAbsoluteUri || navigateUri.Scheme != UriSchemeMsAppx) {
                return false;
            }

            var pageUriStr = navigateUri.OriginalString;

            var i = pageUriStr.IndexOf('?');
            if (i != -1) {
                var query = ParseQueryString(navigateUri.Query);
                pageParameter = query["parameter"] ?? query["param"] ?? query["p"];
                targetFrame = query["target"] ?? query["t"];

                pageUriStr = pageUriStr.Substring(0, i);
            }

            var extension = Path.GetExtension(pageUriStr);
            if (extension == ".xaml") {
                pageType = FindPageType(pageUriStr);
            }
            else if (extension == ".bbcode" || extension == ".bb") {
                pageType = typeof(BBCodePage);
                pageParameter = pageUriStr;            // any parameter in navigateUri is ignored
            }

            return pageType != null;
        }

        private static Dictionary<string, Type> pageTypeCache = new Dictionary<string, Type>();

        private static Type FindPageType(string pageUriStr)
        {
            // check page type cache first
            Type result;
            if (pageTypeCache.TryGetValue(pageUriStr, out result)) {
                return result;
            }

            // check if the page exists
            var pageUri = new Uri(pageUriStr, UriKind.Absolute);

            // find the XBF file in the resource map
            var map = ResourceManager.Current.MainResourceMap;
            var filesTree = map.GetSubtree("Files");
            var resourceName = pageUri.LocalPath;

            // remove forward slash prefix and replace .xaml extension with .xbf
            var resourceNameNoExtension = resourceName.Substring(1, resourceName.Length - 6);
            resourceName = resourceNameNoExtension + ".xbf";

            NamedResource resource;
            if (!filesTree.TryGetValue(resourceName, out resource)) {
                throw new Exception(ResourceHelper.GetString("PageNotFound", pageUriStr));
            }

            // okay, we now know the file exists, determine the typename
            var partialTypeName = resourceNameNoExtension.Replace('/', '.');

            var appAssembly = Application.Current.GetType().GetTypeInfo().Assembly;
            var pageTypes = (from type in appAssembly.GetExportedTypes()
                             where typeof(Page).IsAssignableFrom(type)
                             select type);

            var pageType = pageTypes.FirstOrDefault(t => t.FullName.EndsWith(partialTypeName));

            // cache it (null values as well)
            pageTypeCache[pageUriStr] = pageType;

            return pageType;
        }

        /// <summary>
        /// Attempts to parse a command uri.
        /// </summary>
        /// <param name="navigateUri"></param>
        /// <param name="commandKey"></param>
        /// <param name="commandParameter"></param>
        /// <returns></returns>
        public static bool TryParseCommandUri(Uri navigateUri, out string commandKey, out string commandParameter)
        {
            if (navigateUri == null) {
                throw new ArgumentNullException(nameof(navigateUri));
            }

            commandKey = null;
            commandParameter = null;

            if (!navigateUri.IsAbsoluteUri) {
                return false;
            }
            
            commandKey = navigateUri.OriginalString;

            var i = commandKey.IndexOf('?');
            if (i != -1) {
                var query = ParseQueryString(navigateUri.Query);
                commandParameter = query["parameter"] ?? query["param"] ?? query["p"];
                commandKey = commandKey.Substring(0, i);
            }

            return true;
        }

        private static NameValueCollection ParseQueryString(string query)
        {
            return ParseQueryString(query, Encoding.UTF8);
        }

        private static NameValueCollection ParseQueryString(string query, Encoding encoding)
        {
            if (query == null) {
                throw new ArgumentNullException(nameof(query));
            }
            if (encoding == null) {
                throw new ArgumentNullException(nameof(encoding));
            }
            if (query.Length == 0 || (query.Length == 1 && query[0] == '?')) {
                return new NameValueCollection();
            }

            var result = new NameValueCollection();

            var decoded = HtmlDecode(query);
            var decodedLength = decoded.Length;
            var namePos = 0;
            var first = true;
            while (namePos <= decodedLength) {
                var valuePos = -1;
                var valueEnd = -1;
                for (var q = namePos; q < decodedLength; q++) {
                    if (valuePos == -1 && decoded[q] == '=') {
                        valuePos = q + 1;
                    }
                    else if (decoded[q] == '&') {
                        valueEnd = q;
                        break;
                    }
                }

                if (first) {
                    first = false;
                    if (decoded[namePos] == '?')
                        namePos++;
                }

                string name, value;
                if (valuePos == -1) {
                    name = null;
                    valuePos = namePos;
                }
                else {
                    name = UrlDecode(decoded.Substring(namePos, valuePos - namePos - 1), encoding);
                }
                if (valueEnd < 0) {
                    namePos = -1;
                    valueEnd = decoded.Length;
                }
                else {
                    namePos = valueEnd + 1;
                }
                value = UrlDecode(decoded.Substring(valuePos, valueEnd - valuePos), encoding);

                result.Add(name, value);
                if (namePos == -1)
                    break;
            }


            return result;
        }

        private static string HtmlDecode(string value)
        {
            return value;
        }

        private static string UrlDecode(string value, Encoding encoding)
        {
            return Uri.UnescapeDataString(value);
        }
    }
}
