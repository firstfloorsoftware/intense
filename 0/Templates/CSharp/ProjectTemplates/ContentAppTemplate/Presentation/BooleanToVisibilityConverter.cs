using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace $safeprojectname$.Presentation
{
    public class BooleanToVisibilityConverter
        : IValueConverter
    {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bValue = (bool)value;
            if (this.Inverse) {
                bValue = !bValue;
            }
            return bValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var result = (Visibility)value == Visibility.Visible;
            if (this.Inverse) {
                result = !result;
            }
            return result;
        }
    }
}
