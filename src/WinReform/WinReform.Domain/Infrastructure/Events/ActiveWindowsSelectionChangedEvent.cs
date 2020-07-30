using System;
using System.Collections.Generic;
using System.Text;
using Resizer.Domain.Infrastructure.Messenger;
using Resizer.Domain.Windows;

namespace Resizer.Domain.Infrastructure.Events
{
    /// <summary>
    /// Defines a class thats acts as a contract for when the selected active windows have changed
    /// </summary>
    public class ActiveWindowsSelectionChangedEvent : PubSubEvent<List<Window>>
    {
    }
}
