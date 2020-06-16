using Resizer.Gui.Infrastructure.Common.ViewModel;

namespace Resizer.Gui.Settings
{
    /// <summary>
    /// Defines a class that provides general application settings data
    /// </summary>
    public class ApplicationSettingsViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or Sets an idicator that defines if dark theme should be used
        /// </summary>
        public bool UseDarkTheme
        {
            get => _useDarkTheme;
            set => SetProperty(ref _useDarkTheme, value);
        }

        private bool _useDarkTheme;

        /// <summary>
        /// Gets or Sets an idicator that defines if the window should be minimized when closed
        /// </summary>
        public bool MinimizeOnClose
        {
            get => _minimizeOnClose;
            set => SetProperty(ref _minimizeOnClose, value);
        }

        private bool _minimizeOnClose;

        /// <summary>
        /// Gets or Sets an idicator that defines if the window should be minimized to the system tray
        /// </summary>
        public bool MinimizeToSystemTray
        {
            get => _minimizeToSystemTray;
            set => SetProperty(ref _minimizeToSystemTray, value);
        }

        private bool _minimizeToSystemTray;

        /// <summary>
        /// Create a new instance of the <see cref="ApplicationSettingsViewModel"/>
        /// </summary>
        public ApplicationSettingsViewModel()
        {

        }
    }
}
