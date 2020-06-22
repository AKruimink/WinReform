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
        /// Save the current settings
        /// </summary>
        void Save();
    }
}
