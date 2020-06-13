using Resizer.Gui.Infrastructure.Common.Messenger;
using Resizer.Gui.Infrastructure.Common.Messenger.Strategies;

namespace Resizer.Gui.Tests.Infrastructure.Common.Messenger.Mocks
{
    /// <summary>
    /// Defines a mock implementation of <see cref="EventBase"/>
    /// </summary>
    public class EventBaseMock : EventBase
    {
        /// <summary>
        /// Subscribes a new subscription to an event
        /// </summary>
        /// <param name="subscription">The subscription to add to the subscriber list</param>
        /// <returns>Returns the <see cref="SubscriptionToken"/> that was generated for the subscription</returns>
        public SubscriptionToken Subscribe(IEventSubscription subscription)
        {
            return base.InternalSubscribe(subscription);
        }

        /// <summary>
        /// Unsubscribes a subscription from an evemt
        /// </summary>
        /// <param name="token">The <see cref="SubscriptionToken"/> assigned on subscription</param>
        public void Unsubscribe(SubscriptionToken token)
        {
            base.InternalUnsubscribe(token);
        }

        /// <summary>
        /// Publishes an event
        /// </summary>
        /// <param name="arguments">The arguments to pass along</param>
        public void Publish(params object[] arguments)
        {
            base.InternalPublish(arguments);
        }
    }
}
