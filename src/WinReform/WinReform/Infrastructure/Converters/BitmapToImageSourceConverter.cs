using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace WinReform.Infrastructure.Converters
{
    /// <summary>
    /// Defines a clas that converts a <see cref="BitMap"/> to a <see cref="BitMapSource"/>
    /// </summary>
    public class BitmapToImageSourceConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Instance of the <see cref="BitmapToImageSourceConverter"/>
        /// </summary>
        private BitmapToImageSourceConverter? _instance;

        /// <summary>
        /// Provides an instance of the <see cref="BitmapToImageSourceConverter"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>Returns an instance of the <see cref="RatioConverter"/></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => _instance ?? (_instance = new BitmapToImageSourceConverter());

        /// <summary>
        /// Convert a <see cref="Bitmap"/> to a bindable <see cref="BitMapSource"/>
        /// </summary>
        /// <param name="value">The <see cref="double"/> to calculate the ratio of</param>
        /// <param name="targetType">The type of the bound target property</param>
        /// <param name="parameter">The ratio to convert</param>
        /// <param name="culture">The <see cref="CultureInfo"/> used during the conversion</param>
        /// <returns>Returns a <see cref="double"/> containing the calculated ratio</returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var bitmap = (Bitmap)value;
            var ptr = bitmap.GetHbitmap();

            return Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
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
