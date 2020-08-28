using System.Collections.Generic;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Windows;

namespace WinReform.Domain.Infrastructure.Messanger
{
    /// <summary>
    /// Defines a class thats acts as a contract for when the selected active windows have changed
    /// </summary>
    public class ActiveWindowsSelectionChangedEvent : PubSubEvent<List<Window>>
    {
    }
}
