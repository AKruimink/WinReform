namespace WinReform.Domain.Settings
{
    /// <summary>
    /// Defines a class that acts as model for the application settings
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or Sets an idicator that defines if dark theme should be used
        /// <remarks>Defaults to <see langword="false"/></remarks>
        /// </summary>
        public bool UseDarkTheme { get; set; } = false;

        /// <summary>
        /// Gets or Sets an idicator that defines if the window should be minimized when closed
        /// <remarks>Defaults to <see langword="false"/></remarks>
        /// </summary>
        public bool MinimizeOnClose { get; set; } = false;

        /// <summary>
        /// Gets or Sets an indicator that defines if the Active Windows list should automaticly be updated
        /// <remarks>Defaults to <see langword="false"/></remarks>
        /// </summary>
        public bool AutoRefreshActiveWindows { get; set; } = false;
    }
}
