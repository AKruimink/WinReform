namespace Resizer.Domain.Settings
{
    /// <summary>
    /// Represents a class handles the construction of <see cref="ISetting{T}"/>
    /// </summary>
    public interface ISettingFactory
    {
        /// <summary>
        /// Create a new <see cref="Setting{TSetting}"/>
        /// </summary>
        /// <typeparam name="TSetting">The type of the setting to create</typeparam>
        /// <returns>Returns an instance of <see cref="ISetting{TSetting}"/> of the given settings type</returns>
        ISetting<TSetting> Create<TSetting>() where TSetting : new();
    }
}
