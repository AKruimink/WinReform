namespace WinReform.Domain.Settings
{
    /// <summary>
    /// Represents a class that loads and saves settings
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        /// Load settings
        /// <remarks>A new file with default settings will be created if non exists</remarks>
        /// </summary>
        /// <typeparam name="TSetting">The type of the settings to load</typeparam>
        /// <returns>Returns the settings of type <see cref="TSettings"/></returns>
        TSetting Load<TSetting>() where TSetting : new();

        /// <summary>
        /// Saves settings
        /// </summary>
        /// <typeparam name="TSetting">The type of the settings to save</typeparam>
        /// <param name="settings">The settings to be saved</param>
        void Save<TSetting>(TSetting settings);
    }
}
