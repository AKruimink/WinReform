namespace WinReform.Gui.Settings
{
    /// <summary>
    /// Defines a class that provides design time general application settings data
    /// </summary>
    public class ApplicationSettingsDesignModel : IApplicationSettingsViewModel
    {
        ///<inheritdoc/>
        public bool UseDarkTheme { get; set; }

        ///<inheritdoc/>
        public bool MinimizeOnClose { get; set; }

        ///<inheritdoc/>
        public bool AutoRefreshActiveWindows { get; set; }

        /// <summary>
        /// Create a new instance of the <see cref="ApplicationSettingsDesignModel"/>
        /// </summary>
        public ApplicationSettingsDesignModel()
        {
            UseDarkTheme = true;
            MinimizeOnClose = false;
            AutoRefreshActiveWindows = true;
        }
    }
}
