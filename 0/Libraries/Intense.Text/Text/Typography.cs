using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace Intense.Text
{
    /// <summary>
    /// Universal Windows Platform typography definitions.
    /// </summary>
    public static class Typography
    {
        /// <summary>
        /// Identifies the HeaderTextBlockStyle style key.
        /// </summary>
        public const string HeaderTextBlockStyleKey = "HeaderTextBlockStyle";
        /// <summary>
        /// Identifies the SubheaderTextBlockStyle style key.
        /// </summary>
        public const string SubheaderTextBlockStyleKey = "SubheaderTextBlockStyle";
        /// <summary>
        /// Identifies the TitleTextBlockStyle key.
        /// </summary>
        public const string TitleTextBlockStyleKey = "TitleTextBlockStyle";
        /// <summary>
        /// Identifies the SubtitleTextBlockStyle key.
        /// </summary>
        public const string SubtitleTextBlockStyleKey = "SubtitleTextBlockStyle";
        /// <summary>
        /// Identifies the BaseTextBlockStyle key.
        /// </summary>
        public const string BaseTextBlockStyleKey = "BaseTextBlockStyle";
        /// <summary>
        /// Identifies the BodyTextBlockStyle key.
        /// </summary>
        public const string BodyTextBlockStyleKey = "BodyTextBlockStyle";
        /// <summary>
        /// Identifies the CaptionTextBlockStyle key.
        /// </summary>
        public const string CaptionTextBlockStyleKey = "CaptionTextBlockStyle";

        /// <summary>
        /// Identifies the Segoe MDL2 Assets font.
        /// </summary>
        public static readonly string FontSegoeMDL2Assets = "Segoe MDL2 Assets";
        /// <summary>
        /// Identifies the Courier New font.
        /// </summary>
        public static readonly string FontCourierNew = "Courier New";

        /// <summary>
        /// Identifies the Style attached property.
        /// </summary>
        public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached("Style", typeof(Style), typeof(Typography), new PropertyMetadata(null, OnStyleChanged));
        /// <summary>
        /// Identifies the FontSize attached property.
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.RegisterAttached("FontSize", typeof(string), typeof(Typography), new PropertyMetadata(null, OnFontSizeChanged));

        private static Dictionary<DependencyProperty, DependencyProperty> TextBlockPropertyMap;

        private static void OnStyleChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var element = o as TextElement;
            if (element == null) {
                // ignore
                return;
            }

            // apply the style setters to the element
            ApplyStyle(element, (Style)args.NewValue);
        }

        private static double ToPixel(double pt)
        {
            var display = DisplayInformation.GetForCurrentView();
            return display.LogicalDpi / 72 * pt;
        }

        private static void OnFontSizeChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            QualifiedSize newValue;
            if (!QualifiedSize.TryParse((string)args.NewValue, out newValue)) {
                // ignore invalid values
                return;
            }

            var px = newValue.ToPixelSize();

            var textBlock = o as TextBlock;
            if (textBlock != null) {
                textBlock.FontSize = px;
                return;
            }
            var richTextBlock = o as RichTextBlock;
            if (richTextBlock != null) {
                richTextBlock.FontSize = px;
                return;
            }
            var control = o as Control;
            if (control != null) {
                control.FontSize = px;
                return;
            }
        }

        private static void ApplyStyle(TextElement element, Style style)
        {
            // apply base style first (if there)
            if (style.BasedOn != null) {
                ApplyStyle(element, style.BasedOn);
            }

            foreach (var setter in style.Setters.OfType<Windows.UI.Xaml.Setter>()) {
                var property = setter.Property;

                if (style.TargetType == typeof(TextBlock)) {
                    // make sure TextBlock properties are translated to TextElement properties
                    property = TranslateTextBlockProperty(setter.Property);
                    if (property == null) {
                        continue;
                    }
                }

                // BUG in x64?
                if (property == TextElement.FontWeightProperty) {
                    continue;
                }

                var value = setter.Value;

                // WORKAROUND: avoid InvalidCastException for FontWeight values
                if (property == TextElement.FontWeightProperty) {
                    value = ParseFontWeight(value);
                }
                element.SetValue(property, value);
            }
        }

        private static DependencyProperty TranslateTextBlockProperty(DependencyProperty property)
        {
            if (TextBlockPropertyMap == null)
            {
                TextBlockPropertyMap = new Dictionary<DependencyProperty, DependencyProperty>
                {
                    [TextBlock.CharacterSpacingProperty] = TextElement.CharacterSpacingProperty,
                    [TextBlock.FontFamilyProperty] = TextElement.FontFamilyProperty,
                    [TextBlock.FontSizeProperty] = TextElement.FontSizeProperty,
                    [TextBlock.FontStretchProperty] = TextElement.FontStretchProperty,
                    [TextBlock.FontStyleProperty] = TextElement.FontStyleProperty,
                    [TextBlock.FontWeightProperty] = TextElement.FontWeightProperty,
                    [TextBlock.ForegroundProperty] = TextElement.ForegroundProperty,
                    [TextBlock.IsTextScaleFactorEnabledProperty] = TextElement.IsTextScaleFactorEnabledProperty,
                    [TextBlock.LanguageProperty] = TextElement.LanguageProperty
                };
            }

            DependencyProperty result;
            if (TextBlockPropertyMap.TryGetValue(property, out result)) {
                return result;
            }

            // not supported
            return null;
        }

        private static FontWeight ParseFontWeight(object value)
        {
            var w = (int)value;
            return (from property in typeof(FontWeights).GetRuntimeProperties()
                    let weight = (FontWeight)property.GetValue(null)
                    where weight.Weight == w
                    select weight).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the style for given object.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Style GetStyle(DependencyObject o)
        {
            return (Style)o?.GetValue(StyleProperty);
        }

        /// <summary>
        /// Assigns a style to specified object.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="style"></param>
        public static void SetStyle(DependencyObject o, Style style)
        {
            o?.SetValue(StyleProperty, style);
        }

        /// <summary>
        /// Retrieves the font size.
        /// </summary>
        /// <param name="o"></param>
        public static string GetFontSize(DependencyObject o)
        {
            return (string)o?.GetValue(FontSizeProperty);
        }

        /// <summary>
        /// Sets the font size.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="value"></param>
        public static void SetFontSize(DependencyObject o, string value)
        {
            o?.SetValue(FontSizeProperty, value);
        }
    }
}
