using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Infrastructure.Messenger.Strategies;

namespace WinReform.Tests.Fixtures
{
    /// <summary>
    /// Defines a class that represents a event fixture that provides a fake implementation of <see cref="EventBase"/>
    /// </summary>
    public class EventFixture : EventBase
    {
        /// <summary>
        /// Subscribe to a new event
        /// </summary>
        /// <param name="subscription">Subscription to be added to the subscription list</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that identifies the newly created subscription</returns>
        public SubscriptionToken Subscribe(IEventSubscription subscription)
            => base.InternalSubscribe(subscription);

        /// <summary>
        /// Unsubsribe a subscription from a event
        /// </summary>
        /// <param name="token"><see cref="SubscriptionToken"/> that identifies the subscription to unsubscribe</param>
        public void Unsubscribe(SubscriptionToken token)
            => base.InternalUnsubscribe(token);

        /// <summary>
        /// Publish a event
        /// </summary>
        /// <param name="arguments">Arguments to be passed to the subscribers</param>
        public void Publish(params object[] arguments)
            => base.InternalPublish(arguments);
    }
}
