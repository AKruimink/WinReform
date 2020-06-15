using Resizer.Domain.Infrastructure.Messenger;
using Resizer.Domain.Infrastructure.Messenger.Strategies;
using System;

namespace Resizer.Domain.Tests.Infrastructure.Messenger.Mocks
{
    public class EventSubscriptionMock : IEventSubscription
    {
        /// <summary>
        /// Gets or Sets the value that is returned when a <see cref="Action"/> is published
        /// </summary>
        public Action<object[]>? PublishActionReturnValue { get; set; }

        /// <summary>
        /// Gets or Sets an indicater that defines if publish has been called
        /// </summary>
        public bool PublishActionCalled { get; set; }


        /// <summary>
        /// <see cref="SubscriptionToken"/> assigned to this subscription by <see cref="EventBase"/>
        /// </summary>
        public SubscriptionToken? SubscriptionToken { get; set; }

        /// <summary>
        /// The stratagy used to notify this subscriber
        /// </summary>
        /// <returns>Returns the execution stratagy used</returns>
        public Action<object[]>? GetExecutionStrategy()
        {
            PublishActionCalled = true;
            return PublishActionReturnValue;
        }
    }
}
