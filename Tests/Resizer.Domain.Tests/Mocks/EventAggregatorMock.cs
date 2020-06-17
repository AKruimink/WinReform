using Resizer.Domain.Infrastructure.Messenger;
using System;

namespace Resizer.Domain.Tests.Mocks
{
    public class EventAggregatorMock : IEventAggregator
    {
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            return new TEventType();
        }
    }
}
