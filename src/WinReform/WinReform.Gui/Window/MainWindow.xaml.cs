using System;
using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using WinReform.Gui.Window;

namespace WinReform.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Gets or Sets the scale value used to scale transform elements based on current application resolution
        /// </summary>
        public static readonly DependencyProperty ScaleValueProperty =
            DependencyProperty.Register(
                "ScaleValue",
                typeof(double),
                typeof(MainWindow),
                new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        /// <summary>
        /// Invoked every time <see cref="ScaleValueProperty"/> changed it value
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Invoked each time <see cref="ScaleValueProperty"/> changed regardless if the value is different then the previouse and validates the new value
        /// </summary>
        /// <param name="o"></param>
        /// <param name="value"></param>
        /// <returns>Returns <see cref="double"/> with the validated scale value</returns>
        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            var newValue = 1.0d;

            if (double.IsNaN((double)value))
            {
                return newValue;
            }

            newValue = Math.Min(MaximumScaleValue, (double)value);
            newValue = Math.Max(MinimumScaleValue, newValue);

            return newValue;
        }

        /// <summary>
        /// Gets the maximum scale value allowed
        /// </summary>
        public static double MaximumScaleValue { get; } = 1.0d;

        /// <summary>
        /// Gets the minimum scale value allowed
        /// </summary>
        public static double MinimumScaleValue { get; } = 0.7d;

        /// <summary>
        /// Gets or Sets the scale value used to scale transform the application
        /// </summary>
        public double ScaleValue
        {
            get => (double)GetValue(ScaleValueProperty);
            set => SetValue(ScaleValueProperty, value);
        }

        /// <summary>
        /// Create a new instance of <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calculates a new scale value based on the actual width and height of the application
        /// </summary>
        private double CalculateScale()
        {
            return Math.Min(ActualWidth / 500d, ActualHeight / 400d);
        }

        /// <summary>
        /// Triggers when the application is closed and handles closing or minimizing of the application
        /// TODO: Fix the Window Minimize on close to only work with the Close button, all other ways of closing the app should just close as expected
        /// WinForms has a CloseReason which seems to be non existing for WPF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            var vm = (WindowViewModel)DataContext;

            if (vm.MinimizeOnClose)
            {
                WindowState = WindowState.Minimized;
                e.Cancel = true;
                return;
            }

            Application.Current.Shutdown();
        }

        /// <summary>
        /// Fires each time the <see cref="MainWindow"/> changes size and updates the scale accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinReformSizeChanged(object sender, EventArgs e)
        {
            ScaleValue = (double)OnCoerceScaleValue(WinReformWindow, CalculateScale());
        }
    }
}
