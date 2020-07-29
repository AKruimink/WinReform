using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Resizer.Gui.Infrastructure.Converters
{
    /// <summary>
    /// Defines a class that calculates a given ratio of a given <see cref="double"/>
    /// </summary>
    public class RatioConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Instance of the <see cref="RatioConverter"/>
        /// </summary>
        private RatioConverter? _instance;

        /// <summary>
        /// Provides an instance of the <see cref="RatioConverter"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>Returns an instance of the <see cref="RatioConverter"/></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => _instance ?? (_instance = new RatioConverter());

        /// <summary>
        /// Calculate the ratio of a given <see cref="double"/>
        /// </summary>
        /// <param name="value">The <see cref="double"/> to calculate the ratio of</param>
        /// <param name="targetType">The type of the bound target property</param>
        /// <param name="parameter">The ratio to convert</param>
        /// <param name="culture">The <see cref="CultureInfo"/> used during the conversion</param>
        /// <returns>Returns a <see cref="double"/> containing the calculated ratio</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = 0d;
            if (value != null)
            {
                size = System.Convert.ToDouble(value, CultureInfo.InvariantCulture) * System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            }

            return size;
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
