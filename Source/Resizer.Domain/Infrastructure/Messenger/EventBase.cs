using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Resizer.Domain.Infrastructure.Messenger.Strategies;

namespace Resizer.Domain.Infrastructure.Messenger
{
    /// <summary>
    /// Defines base class that allows publication and subscriptioon to events
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// All the current subscriptions to this event
        /// </summary>
        private readonly List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        /// <summary>
        /// Gets or Sets the <see cref="SynchronizationContext"/> used by <see cref="DispatcherEventSubscription/>
        /// </summary>
        public SynchronizationContext? SynchronizationContext { get; set; }

        /// <summary>
        /// Gets a list of all the current subscriptions
        /// </summary>
        protected ICollection<IEventSubscription> Subscriptions => _subscriptions;

        /// <summary>
        /// Check if a given <see cref="SubscriptionToken"/> is subscribed to the event
        /// </summary>
        /// <param name="token">The token to match against the subscriptions</param>
        /// <returns>Returns <see langword="true"/> if the subscription exists, otherwise returns <see langword="false"/></returns>
        public virtual bool Contains(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                var subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                return subscription != null;
            }
        }

        /// <summary>
        /// Prunes the subscription that no longer have a valid execution stratagy
        /// </summary>
        public void PruneSubscriptions()
        {
            lock (Subscriptions)
            {
                for (var i = Subscriptions.Count - 1; i >= 0; i--)
                {
                    if (_subscriptions[i].GetExecutionStrategy() == null)
                    {
                        _subscriptions.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a new <see cref="IEventSubscription"/> to the subscribers collection of this event
        /// </summary>
        /// <param name="eventSubscription">The <see cref="IEventSubscription"/> to add</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that idenfies the subscription</returns>
        protected virtual SubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
        {
            if (eventSubscription == null)
            {
                throw new ArgumentNullException(nameof(eventSubscription));
            }

            eventSubscription.SubscriptionToken = new SubscriptionToken();

            lock (Subscriptions)
            {
                Subscriptions.Add(eventSubscription);
            }
            return eventSubscription.SubscriptionToken;
        }

        /// <summary>
        /// Removes a subscription matching a <see cref="SubscriptionToken"/>
        /// </summary>
        /// <param name="token"><see cref="SubscriptionToken"/> identifying the subscription to remove</param>
        protected virtual void InternalUnsubscribe(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                var subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                if (subscription != null)
                {
                    Subscriptions.Remove(subscription);
                }
            }
        }

        /// <summary>
        /// Calls all the execution stratagies exposed by <see cref="IEventSubscription"/>
        /// </summary>
        /// <param name="arguments">The arguments to be passed to all subscriptions on execution</param>
        protected virtual void InternalPublish(params object[] arguments)
        {
            var executionStrategies = GetExecutionStratagies();
            foreach (var executionStrategy in executionStrategies)
            {
                executionStrategy(arguments);
            }
        }

        /// <summary>
        /// Gets a list of all execution stratagies
        /// </summary>
        /// <returns><Returns <see cref="List{T}"/> containing all execution stratagies</returns>
        private List<Action<object[]>> GetExecutionStratagies()
        {
            var returnList = new List<Action<object[]>>();

            lock (Subscriptions)
            {
                for (var i = Subscriptions.Count - 1; i >= 0; i--)
                {
                    var listItem = _subscriptions[i].GetExecutionStrategy();

                    if (listItem == null)
                    {
                        _subscriptions.RemoveAt(i);
                    }
                    else
                    {
                        returnList.Add(listItem);
                    }
                }
            }
            return returnList;
        }
    }
}
