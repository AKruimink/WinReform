namespace Resizer.Domain.Settings
{
    /// <summary>
    /// Represents a class that holds settings
    /// </summary>
    /// <typeparam name="TSetting">The settings type</typeparam>
    public interface ISettings<TSetting>
    {
        /// <summary>
        /// Save settings
        /// </summary>
        /// <param name="item">The <see cref="Settings"/> to save</param>
        void Save(TSetting settings);
    }
}
