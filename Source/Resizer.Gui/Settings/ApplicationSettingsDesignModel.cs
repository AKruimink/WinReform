namespace Resizer.Gui.Settings
{
    /// <summary>
    /// Defines a class that provides design time general application settings data
    /// </summary>
    public class ApplicationSettingsDesignModel
    {
        /// <summary>
        /// Gets or Sets an idicator that defines if dark theme should be used
        /// </summary>
        public bool UseDarkTheme { get; set; }

        /// <summary>
        /// Gets or Sets an idicator that defines if the window should be minimized when closed
        /// </summary>
        public bool MinimizeOnClose { get; set; }

        /// <summary>
        /// Gets or Sets an idicator that defines if the window should be minimized to the system tray
        /// </summary>
        public bool MinimizeToSystemTray { get; set; }

        /// <summary>
        /// Create a new instance of the <see cref="ApplicationSettingsDesignModel"/>
        /// </summary>
        public ApplicationSettingsDesignModel()
        {
            UseDarkTheme = true;
            MinimizeOnClose = false;
            MinimizeToSystemTray = true;
        }
    }
}
