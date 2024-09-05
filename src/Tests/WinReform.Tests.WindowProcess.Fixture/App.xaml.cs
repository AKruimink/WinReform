using System;
using System.Timers;
using System.Windows;

namespace WinReform.Tests.WindowProcess.Fixture
{
    /// <summary>
    /// Defines a class that represents a fixture of a process with a window
    /// NOTE: The application automatically closes after x seconds, this to make sure that if the test failed and the dispose isn't triggered the app won't linger in the background forever.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// <see cref="Timer"/> that triggers an automatic close of the application
        /// </summary>
        private System.Timers.Timer? _autoCloseTimer;

        /// <summary>
        /// Sets up the project
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _autoCloseTimer = new System.Timers.Timer(20000); // 20 seconds
            _autoCloseTimer.Elapsed += OnAutoCloseTimer;
            _autoCloseTimer.Start();

            var window = new MainWindow();
            window.Show();
        }

        /// <summary>
        /// Raised when the <see cref="_autoCloseTimer"/> elapsed and automatically closes the application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="ElapsedEventArgs"/> that contains the event data.</param>
        private void OnAutoCloseTimer(object? sender, ElapsedEventArgs e)
        {
            if(sender == null)
            {
                return;
            }

            if (_autoCloseTimer != null)
            {
                _autoCloseTimer.Close();
                _autoCloseTimer.Dispose();
                _autoCloseTimer = null;
            }

            Environment.Exit(0);
        }
    }
}
