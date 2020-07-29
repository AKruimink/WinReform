using System;
using System.Collections.Generic;
using System.Threading;

namespace Resizer.Domain.Infrastructure.Messenger
{
    /// <summary>
    /// Defines a class that gets the instance of a <see cref="PubSubEvent"/> event
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        /// <summary>
        /// The events that currently exist
        /// </summary>
        private readonly Dictionary<Type, EventBase> _events = new Dictionary<Type, EventBase>();

        /// <summary>
        /// The <see cref="SynchronizationContext"/> set on creation used for UI thread dispatching
        /// </summary>
        private readonly SynchronizationContext? _synchronizationContext = SynchronizationContext.Current;

        ///<inheritdoc/>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            lock (_events)
            {
                if (!_events.TryGetValue(typeof(TEventType), out var existingEvent))
                {
                    var newEvent = new TEventType
                    {
                        SynchronizationContext = _synchronizationContext
                    };
                    _events[typeof(TEventType)] = newEvent;

                    return newEvent;
                }
                else
                {
                    return (TEventType)existingEvent;
                }
            }
        }
    }
}
