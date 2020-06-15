namespace Resizer.Domain.Settings
{
    /// <summary>
    /// Represents a class that loads and saves settings
    /// </summary>
    public interface ISettingsStore
    {
        /// <summary>
        /// Load settings
        /// <remarks>A new file with default settings will be created if non exists</remarks>
        /// </summary>
        /// <typeparam name="TSettings">The type of the settings to load</typeparam>
        /// <returns>Returns the settings of type <see cref="TSettings"/></returns>
        TSettings Load<TSettings>() where TSettings : new();

        /// <summary>
        /// Saves settings
        /// </summary>
        /// <typeparam name="TSettings">The type of the settings to save</typeparam>
        /// <param name="settings">The settings to be saved</param>
        void Save<TSettings>(TSettings settings);
    }
}
