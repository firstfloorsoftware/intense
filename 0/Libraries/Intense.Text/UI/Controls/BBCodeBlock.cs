using Intense.Navigation;
using Intense.Text.Parsers.BBCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;

namespace Intense.UI.Controls
{
    /// <summary>
    /// A lighweight control for displaying rich formatted BBCode content.
    /// </summary>
    [ContentProperty(Name = "BBCode")]
    public class BBCodeBlock
        : Control, IBBCodeParserSettings
    {
        /// <summary>
        /// Identifies the BBCode dependency property.
        /// </summary>
        public static readonly DependencyProperty BBCodeProperty = DependencyProperty.Register("BBCode", typeof(string), typeof(BBCodeBlock), new PropertyMetadata(null, OnPropertyChanged));
        /// <summary>
        /// Identifies the BBCodeSource dependency property.
        /// </summary>
        public static readonly DependencyProperty BBCodeSourceProperty = DependencyProperty.Register("BBCodeSource", typeof(Uri), typeof(BBCodeBlock), new PropertyMetadata(null, OnBBCodeSourceChanged));
        /// <summary>
        /// Identifies the Navigator dependency property.
        /// </summary>
        public static readonly DependencyProperty NavigatorProperty = DependencyProperty.Register("Navigator", typeof(ILinkNavigator), typeof(BBCodeBlock), new PropertyMetadata(new DefaultLinkNavigator(), OnNavigatorChanged));
        /// <summary>
        /// Identifies the RichTextBlockStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty RichTextBlockStyleProperty = DependencyProperty.Register("RichTextBlockStyle", typeof(Style), typeof(BBCodeBlock), null);

        /// <summary>
        /// Occurs when a BBCode parse operation has failed.
        /// </summary>
        public event EventHandler<ParseFailedEventArgs> ParseFailed;
        /// <summary>
        /// Occurs when a link navigation has occured.
        /// </summary>
        public event EventHandler<LinkNavigationEventArgs> Navigated;
        /// <summary>
        /// Occurs when a link navigation has failed.
        /// </summary>
        public event EventHandler<LinkNavigationFailedEventArgs> NavigationFailed;

        private RichTextBlock textBlock;

        /// <summary>
        /// Initializes a new instance of the <see cref="BBCodeBlock"/> class.
        /// </summary>
        public BBCodeBlock()
        {
            this.DefaultStyleKey = typeof(BBCodeBlock);
        }

        private static void OnPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((BBCodeBlock)o).Update();
        }

        private static void OnNavigatorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            // disallow null values
            if (e.NewValue == null) {
                throw new ArgumentNullException("Navigator");
            }
            ((BBCodeBlock)o).Update();
        }

        private static void OnBBCodeSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var source = (Uri)e.NewValue;
            ((BBCodeBlock)o).OnBBCodeSourceChanged(source);
        }

        private async void OnBBCodeSourceChanged(Uri source)
        {
            if (source == null) {
                this.BBCode = null;
            }
            else {
                try {
                    var file = await StorageFile.GetFileFromApplicationUriAsync(source);
                    using (var fileStream = await file.OpenReadAsync()) {
                        using (var reader = new StreamReader(fileStream.AsStreamForRead())) {
                            this.BBCode = reader.ReadToEnd();
                        }
                    }
                }
                catch(Exception error) {
                    this.BBCode = Text.BBCode.Escape(error.Message);
                }
            }
        }

        private void Update()
        {
            if (this.textBlock == null) {
                return;
            }

            // clear
            this.textBlock.Blocks.Clear();

            if (!string.IsNullOrEmpty(this.BBCode)) {
                Paragraph paragraph;

                try {
                    // always trim BBCode (might become a setting someday)
                    
                    // returns the explicit IBBCodeParserSettings interface implementation, it provides
                    // a correct implementation of BaseUri
                    paragraph = Text.BBCode.ParseParagraph(this.BBCode.Trim(), (IBBCodeParserSettings)this);

                    // find all buttons and hyperlinks and initialize associated navigation commands
                    InitializeNavigationCommands(paragraph);
                }
                catch (Exception e) {
                    // set bbcode as-is
                    paragraph = new Paragraph();
                    paragraph.Inlines.Add(new Run { Text = this.BBCode });

                    // and raise the ParseFailed event
                    OnParseFailed(e);
                }

                this.textBlock.Blocks.Add(paragraph);
            }
        }

        private void InitializeNavigationCommands(Paragraph paragraph)
        {
            var service = new TextElementHierarchyService();
            var buttonCmds = from element in service.GetDescendants(paragraph).OfType<InlineUIContainer>()
                             select (element.Child as Button)?.Command as NavigationCommand;

            var linkCmds = from element in service.GetDescendants(paragraph).OfType<Hyperlink>()
                           select Navigation.Navigation.GetCommand(element);

            var cmds = buttonCmds.Concat(linkCmds).Where(c => c != null);
            foreach (var cmd in cmds) {
                cmd.Initialize(this);
            }
        }

        private void OnParseFailed(Exception error)
        {
            this.ParseFailed?.Invoke(this, new ParseFailedEventArgs(error));
        }

        private void OnNavigated(Uri navigateUri, DependencyObject source)
        {
            this.Navigated?.Invoke(this, new LinkNavigationEventArgs(navigateUri, source));
        }

        private void OnNavigationFailed(Uri navigateUri, DependencyObject source, Exception error)
        {
            this.NavigationFailed?.Invoke(this, new LinkNavigationFailedEventArgs(navigateUri, source, error));
        }

        internal bool CanNavigate(Uri navigateUri, DependencyObject source)
        {
            return navigateUri != null && source != null && this.Navigator.CanNavigate(navigateUri, source);
        }

        internal async void Navigate(Uri navigateUri, DependencyObject source)
        {
            try {
                await this.Navigator.NavigateAsync(navigateUri, source);

                OnNavigated(navigateUri, source);
            }
            catch (Exception error) {
                OnNavigationFailed(navigateUri, source, error);
            }
        }

        /// <summary>
        /// Occurs after a template has been applied.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.textBlock = (RichTextBlock)GetTemplateChild("Text");

            Update();
        }

        /// <summary>
        /// Gets or sets the BBCode content.
        /// </summary>
        /// <value>The BB code.</value>
        public string BBCode
        {
            get { return (string)GetValue(BBCodeProperty); }
            set { SetValue(BBCodeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the source of the BBCode content.
        /// </summary>
        /// <value>The BBCode source.</value>
        public Uri BBCodeSource
        {
            get { return (Uri)GetValue(BBCodeSourceProperty); }
            set { SetValue(BBCodeSourceProperty, value); }
        }

        /// <summary>
        /// Gets or set the navigator for navigating hyperlinks.
        /// </summary>
        public ILinkNavigator Navigator
        {
            get { return (ILinkNavigator)GetValue(NavigatorProperty); }
            set { SetValue(NavigatorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style applied to the inner RichTextBlock.
        /// </summary>
        public Style RichTextBlockStyle
        {
            get { return (Style)GetValue(RichTextBlockStyleProperty); }
            set { SetValue(RichTextBlockStyleProperty, value); }
        }

        ILinkNavigator IBBCodeParserSettings.Navigator
        {
            get { return this.Navigator; }
        }

        Uri IBBCodeParserSettings.BaseUri
        {
            get
            {
                // either use BBCodeSource as base uri, otherwise fallback to control's BaseUri
                return this.BBCodeSource ?? this.BaseUri;
            }
        }
    }
}
