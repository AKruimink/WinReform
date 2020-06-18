using Resizer.Domain.Infrastructure.Messenger;
using System;

namespace Resizer.Domain.Tests.Mocks
{
    /// <summary>
    /// Mock implementation of <see cref="IEventAggregator"/>
    /// </summary>
    public class EventAggregatorMock : IEventAggregator
    {
        /// <summary>
        /// Indicates if the request has been executed
        /// </summary>
        public bool Executed { get; set; } = false;

        /// <summary>
        /// <see cref="Type"/> of the event requested
        /// </summary>
        public Type? EventType { get; set; }

        ///<inheritdoc/>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            Executed = true;
            EventType = typeof(TEventType);
            return new TEventType();
        }
    }
}
