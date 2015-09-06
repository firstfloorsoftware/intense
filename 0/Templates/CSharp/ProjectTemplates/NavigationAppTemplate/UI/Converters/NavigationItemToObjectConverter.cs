using $safeprojectname$.Presentation;

namespace $safeprojectname$.UI.Converters
{
    /// <summary>
    /// Converts a <see cref="NavigationItem"/> instance to object and vice versa.
    /// </summary>
    public class NavigationItemToObjectConverter
        : ValueConverter<NavigationItem, object>
    {
        /// <summary>
        /// Converts a source value to the target type.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        protected override object Convert(NavigationItem value, object parameter, string language)
        {
            return value;
        }

        /// <summary>
        /// Converts a target value back to the source type.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        protected override NavigationItem ConvertBack(object value, object parameter, string language)
        {
            return (NavigationItem)value;
        }
    }
}
