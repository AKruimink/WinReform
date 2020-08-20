using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WinReform.Gui.Infrastructure.Converters
{
    /// <summary>
    /// Defines a class that Calulates a value by subtracting a given value from the base value
    /// </summary>
    public class SubtractionConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Instance of the <see cref="RatioConverter"/>
        /// </summary>
        private SubtractionConverter? _instance;

        /// <summary>
        /// Provides an instance of the <see cref="SubtractionConverter"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>Returns an instance of the <see cref="SubtractionConverter"/></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => _instance ?? (_instance = new SubtractionConverter());

        /// <summary>
        /// Calculate a new <see cref="int"/> by subtracting a value
        /// </summary>
        /// <param name="value">The <see cref="int"/> to subtract from</param>
        /// <param name="targetType">The type of the bound target property</param>
        /// <param name="parameter">The <see cref="int"/> to subtract</param>
        /// <param name="culture">The <see cref="CultureInfo"/> used during the conversion</param>
        /// <returns>Returns a <see cref="int"/> containing the new value after subtraction</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var newValue = 0;
            if (value != null)
            {
                newValue = System.Convert.ToInt32(value, CultureInfo.InvariantCulture) - System.Convert.ToInt32(parameter, CultureInfo.InvariantCulture);
            }

            return newValue < 0 ? 0 : newValue;
        }

        /// <summary>
        /// Calculate a new <see cref="int"/> by adding a value
        /// </summary>
        /// <param name="value">The <see cref="int"/> to add too</param>
        /// <param name="targetType">The type of the bound target property</param>
        /// <param name="parameter">The <see cref="int"/> to be added</param>
        /// <param name="culture">The <see cref="CultureInfo"/> used during the conversion</param>
        /// <returns>Returns a <see cref="int"/> containing the new value after addition</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var newValue = 0;
            if (value != null)
            {
                newValue = System.Convert.ToInt32(value, CultureInfo.InvariantCulture) + System.Convert.ToInt32(parameter, CultureInfo.InvariantCulture);
            }

            return newValue < 0 ? 0 : newValue;
        }
    }
}
