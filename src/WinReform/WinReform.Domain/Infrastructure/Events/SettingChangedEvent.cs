using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Settings;

namespace WinReform.Domain.Infrastructure.Events
{
    /// <summary>
    /// Defines a class that acts as a contract for when a settings have changed
    /// </summary>
    public class SettingChangedEvent<TSetting> : PubSubEvent<ISetting<TSetting>>
    {
    }
}
