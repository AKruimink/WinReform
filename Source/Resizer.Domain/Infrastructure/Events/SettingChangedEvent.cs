using System;
using Resizer.Domain.Infrastructure.Messenger;

namespace Resizer.Domain.Infrastructure.Events
{
    /// <summary>
    /// Defines a class that acts as a contract for when a settings have changed
    /// </summary>
    public class SettingChangedEvent : PubSubEvent<Type>
    {
    }
}
