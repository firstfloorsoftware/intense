using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace $safeprojectname$.UI
{
    /// <summary>
    /// The page header control with support for search.
    /// </summary>
    public class PageHeader
        : Control
    {
        /// <summary>
        /// Identifies the Frame dependency property.
        /// </summary>
        public static readonly DependencyProperty FrameProperty = DependencyProperty.Register("Frame", typeof(Frame), typeof(PageHeader), new PropertyMetadata(null, OnFrameChanged));
        /// <summary>
        /// Identifies the Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(PageHeader), null);
        /// <summary>
        /// Identifies the IsSearchBoxVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSearchBoxVisibleProperty = DependencyProperty.Register("IsSearchBoxVisible", typeof(bool), typeof(PageHeader), new PropertyMetadata(true));
        /// <summary>
        /// Identifies the SearchTerm dependency property.
        /// </summary>
        public static readonly DependencyProperty SearchTermProperty = DependencyProperty.Register("SearchTerm", typeof(string), typeof(PageHeader), null);
        /// <summary>
        /// Identifies the Title dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(PageHeader), null);

        private Button iconButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageHeader"/> control.
        /// </summary>
        public PageHeader()
        {
            this.DefaultStyleKey = typeof(PageHeader);
        }

        private static void OnFrameChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            ((PageHeader)o).OnFrameChanged();
        }

        private void OnFrameChanged()
        {
            UpdateIconButtonState();
        }

        /// <summary>
        /// Occurs when the control's template has been applied to this instance.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.iconButton != null) {
                this.iconButton.Click -= OnIconButtonClick;
            }

            this.iconButton = GetTemplateChild("IconButton") as Button;
            if (this.iconButton != null) {
                this.iconButton.Click += OnIconButtonClick;
            }

            UpdateIconButtonState();
        }

        private void OnIconButtonClick(object sender, RoutedEventArgs e)
        {
            // attemp to navigate back to the first element in the frame's navigation stack
            while (this.Frame?.CanGoBack ?? false) {
                this.Frame.GoBack();
            }
        }

        private void UpdateIconButtonState()
        {
            if (this.iconButton == null) {
                return;
            }
            this.iconButton.IsEnabled = this.Frame?.CanGoBack ?? false;
        }

        /// <summary>
        /// Gets or sets the controlling frame.
        /// </summary>
        public Frame Frame
        {
            get { return (Frame)GetValue(FrameProperty); }
            set { SetValue(FrameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the search box is visible.
        /// </summary>
        public bool IsSearchBoxVisible
        {
            get { return (bool)GetValue(IsSearchBoxVisibleProperty); }
            set { SetValue(IsSearchBoxVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        public string SearchTerm
        {
            get { return (string)GetValue(SearchTermProperty); }
            set { SetValue(SearchTermProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}
