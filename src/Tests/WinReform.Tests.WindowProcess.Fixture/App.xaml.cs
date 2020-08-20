using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace WinReform.Tests.WindowProcess.Fixture
{
    /// <summary>
    /// Defines a class that represents a fixture of a process with a window
    /// NOTE: The application automaticly closes after x seconds, this to make sure that if the test failed and the dispose isnt't triggered the app wont linger in the background forever
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// <see cref="Timer"/> that triggers an automatic close of the application
        /// </summary>
        private  Timer _autoCloseTimer;

        /// <summary>
        /// Sets up the project
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _autoCloseTimer = new Timer(20000); // 20 seconds
            _autoCloseTimer.Elapsed += OnAutoCloseTimer;
            _autoCloseTimer.Start();

            var window = new MainWindow();
            window.Show();
        }

        /// <summary>
        /// Raised when the <see cref="_autoCloseTimer"/> elapsed and automaticly closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAutoCloseTimer(object sender, ElapsedEventArgs e)
        {
            _autoCloseTimer.Close();
            _autoCloseTimer.Dispose();
            Environment.Exit(0);
        }
    }
}
