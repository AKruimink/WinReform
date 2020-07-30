using System;

namespace WinReform.Domain.Infrastructure.Messenger.Strategies
{
    /// <summary>
    /// Represents a subscription to a event used by <see cref="EventBase"/>
    /// </summary>
    public interface IEventSubscription
    {
        /// <summary>
        /// Gets or Sets the <see cref="SubscriptionToken"/> that identifies this <see cref="IEventSubscription"/>
        /// </summary>
        SubscriptionToken? SubscriptionToken { get; set; }

        /// <summary>
        /// Get the execution stratagy to publish the event
        /// </summary>
        /// <returns>Returns <see cref="Action"/> of the execution stragaty or <see langword="null"/> if the <see cref="EventSubscription"/> is no longer valid</returns>
        Action<object[]>? GetExecutionStrategy();
    }
}
