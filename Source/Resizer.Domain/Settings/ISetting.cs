namespace Resizer.Domain.Settings
{
    /// <summary>
    /// Represents a class that represents a setting
    /// </summary>
    /// <typeparam name="TSetting">The type of the setting to represent</typeparam>
    public interface ISetting<TSetting>
    {
        /// <summary>
        /// Gets the current settings
        /// </summary>
        TSetting CurrentSetting { get; }

        /// <summary>
        /// Save settings
        /// </summary>
        /// <param name="item">The setting to save</param>
        void Save(TSetting settings);
    }
}
