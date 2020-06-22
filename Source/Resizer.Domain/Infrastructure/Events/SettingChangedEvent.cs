using Resizer.Domain.Infrastructure.Messenger;
using Resizer.Domain.Settings;

namespace Resizer.Domain.Infrastructure.Events
{
    /// <summary>
    /// Defines a class that acts as a contract for when a settings have changed
    /// </summary>
    public class SettingChangedEvent<TSetting> : PubSubEvent<ISetting<TSetting>>
    {
    }
}
