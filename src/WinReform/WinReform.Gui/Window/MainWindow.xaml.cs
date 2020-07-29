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
        /// Create a new instance of <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
