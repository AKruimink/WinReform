using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WinReform.Infrastructure.Converters
{
    /// <summary>
    /// Defines a class that converts <see cref="bool"/> to <see cref="Visibility"/>
    /// </summary>
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Instance of the <see cref="BooleanToVisibilityConverter"/>
        /// </summary>
        private BooleanToVisibilityConverter? _instance;

        /// <summary>
        /// Provides an instance of the <see cref="BooleanToVisibilityConverter"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>Returns an instance of the <see cref="BooleanToVisibilityConverter"/></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => _instance ?? (_instance = new BooleanToVisibilityConverter());

        /// <summary>
        /// Convert a <see cref="bool"/> to a <see cref="Visibility"/>
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a <see cref="Visibility"/> to a <see cref="bool"/>
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }

    /// <summary>
    /// Defines a class that converts <see cref="bool"/> to inverted <see cref="Visibility"/>
    /// </summary>
    public class InvertedBooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Instance of the <see cref="InvertedBooleanToVisibilityConverter"/>
        /// </summary>
        private InvertedBooleanToVisibilityConverter? _instance;

        /// <summary>
        /// Provides an instance of the <see cref="InvertedBooleanToVisibilityConverter"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>Returns an instance of the <see cref="InvertedBooleanToVisibilityConverter"/></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => _instance ?? (_instance = new InvertedBooleanToVisibilityConverter());

        /// <summary>
        /// Convert a <see cref="bool"/> to a inverted <see cref="Visibility"/>
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            return (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Convert a <see cref="Visibility"/> to a inverted <see cref="bool"/>
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return value is Visibility && (Visibility)value == Visibility.Collapsed;
        }
    }
}
