namespace Resizer.Gui.Common.Messenger
{
    /// <summary>
    /// Represents a class that gets the instance of a <see cref="EventBase"/>
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Gets an instance of a <see cref="EventBase"/>
        /// </summary>
        /// <typeparam name="TEventType">The type of the event to get</typeparam>
        /// <returns>Returns an instance of the <see cref="EventBase"/></returns>
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();
    }
}
