using System;
using System.Linq;
using WinReform.Domain.Infrastructure.Messenger.Strategies;

namespace WinReform.Domain.Infrastructure.Messenger
{
    /// <summary>
    /// Defines a class that mnages publication of and subscription to events
    /// </summary>
    public class PubSubEvent : EventBase
    {
        /// <summary>
        /// Subscribe a <see cref="Action"/> to the <see cref="PubSubEvent"/> that will be published on <see cref="ThreadOption.PublisherThread"/>
        /// </summary>
        /// <param name="action">The <see cref="Action"/> that gets executed when the event is published</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public SubscriptionToken Subscribe(Action action)
        {
            return Subscribe(action, ThreadOption.PublisherThread);
        }

        /// <summary>
        /// Subscribe a delegate to an event that will be published on <see cref="ThreadOption.PublisherThread"/>
        /// </summary>
        /// <param name="action">The <see cref="Action"/> that gets executed when the event is published</param>
        /// <param name="keepSubscriberReferenceAlive">If set to <see langword="true"/> the <see cref="PubSubEvent"/> will hold a strong reference to the action, otherwise a weak reference will be kept</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public SubscriptionToken Subscribe(Action action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);
        }

        /// <summary>
        /// Subscribe a <see cref="Action"/> to the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="action">The <see cref="Action"/> that gets executed when the event is published</param>
        /// <param name="threadOption">Specifies on which thread the received delegate is executed</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public SubscriptionToken Subscribe(Action action, ThreadOption threadOption)
        {
            return Subscribe(action, threadOption, false);
        }

        /// <summary>
        /// Subscribe a <see cref="Action"/> to the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="action">The <see cref="Action"/> that gets executed when the event is published</param>
        /// <param name="threadOption">Specifies on which thread the received delegate is executed</param>
        /// <param name="keepSubscriberReferenceAlive">If set to <see langword="true"/> the <see cref="PubSubEvent"/> will hold a strong reference to the action, otherwise a weak reference will be kept</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public virtual SubscriptionToken Subscribe(Action action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
        {
            var actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);

            EventSubscription subscription;
            switch (threadOption)
            {
                case ThreadOption.BackgroundThread:
                    subscription = new BackgroundEventSubscription(actionReference);
                    break;

                case ThreadOption.PublisherThread:
                    subscription = new EventSubscription(actionReference);
                    break;

                case ThreadOption.UIThread:
                    if (SynchronizationContext == null)
                    {
                        throw new ArgumentNullException(nameof(SynchronizationContext));
                    }
                    subscription = new DispatcherEventSubscription(actionReference, SynchronizationContext);
                    break;

                default:
                    subscription = new EventSubscription(actionReference);
                    break;
            }

            return InternalSubscribe(subscription);
        }

        /// <summary>
        /// Unsubscribe a <see cref="Action"/> from the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="subscriber">The delegate to unsubscribe</param>
        public virtual void Unsubscribe(Action subscriber)
        {
            lock (Subscriptions)
            {
                var eventSubscription = Subscriptions.Cast<EventSubscription>().FirstOrDefault(evt => evt.GetAction == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        /// <summary>
        /// Publishe the <see cref="PubSubEvent"/>
        /// </summary>
        public virtual void Publish()
        {
            InternalPublish();
        }

        /// <summary>
        /// Check if a given <see cref="Action"/> is subscribed to the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="subscriber">The <see cref="Action"/> to match against the subscriptions</param>
        /// <returns>Returns <see langword="true"/> if the subscription exists, otherwise returns <see langword="false"/></returns>
        public virtual bool Contains(Action subscriber)
        {
            lock (Subscriptions)
            {
                var subscription = Subscriptions.Cast<EventSubscription>().FirstOrDefault(evt => evt.GetAction == subscriber);
                return subscription != null;
            }
        }
    }

    /// <summary>
    /// Defines a class that mnages publication of and subscription to events
    /// </summary>
    /// <typeparam name="TPayLoad">The type of argument that will be passed to the subscriber</typeparam>
    public class PubSubEvent<TPayLoad> : EventBase
    {
        /// <summary>
        /// Subscribe a <see cref="Action{T}"/> to the <see cref="PubSubEvent"/> that will be published on <see cref="ThreadOption.PublisherThread"/>
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> that gets executed when the event is published</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public SubscriptionToken Subscribe(Action<TPayLoad> action)
        {
            return Subscribe(action, ThreadOption.PublisherThread);
        }

        /// <summary>
        /// Subscribe a delegate to an event that will be published on <see cref="ThreadOption.PublisherThread"/>
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> that gets executed when the event is published</param>
        /// <param name="keepSubscriberReferenceAlive">If set to <see langword="true"/> the <see cref="PubSubEvent"/> will hold a strong reference to the action, otherwise a weak reference will be kept</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public SubscriptionToken Subscribe(Action<TPayLoad> action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);
        }

        /// <summary>
        /// Subscribe a <see cref="Action"/> to the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> that gets executed when the event is published</param>
        /// <param name="threadOption">Specifies on which thread the received delegate is executed</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public SubscriptionToken Subscribe(Action<TPayLoad> action, ThreadOption threadOption)
        {
            return Subscribe(action, threadOption, false);
        }

        /// <summary>
        /// Subscribe a <see cref="Action{T}"/> to the <see cref="PubSubEvent"/> that will be published on <see cref="ThreadOption.PublisherThread"/>
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> that gets executed when the event is published</param>
        /// <param name="filter">Filter that evaluates if the subscriber should receive the event</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public SubscriptionToken Subscribe(Action<TPayLoad> action, Predicate<TPayLoad> filter)
        {
            return Subscribe(action, ThreadOption.PublisherThread, false, filter);
        }

        /// <summary>
        /// Subscribe a <see cref="Action{T}"/> to the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> that gets executed when the event is published</param>
        /// <param name="threadOption">Specifies on which thread the received delegate is executed</param>
        /// <param name="keepSubscriberReferenceAlive">If set to <see langword="true"/> the <see cref="PubSubEvent"/> will hold a strong reference to the action, otherwise a weak reference will be kept</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public virtual SubscriptionToken Subscribe(Action<TPayLoad> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, threadOption, keepSubscriberReferenceAlive, null);
        }

        /// <summary>
        /// Subscribe a <see cref="Action{T}"/> to the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> that gets executed when the event is published</param>
        /// <param name="threadOption">Specifies on which thread the received delegate is executed</param>
        /// <param name="keepSubscriberReferenceAlive">If set to <see langword="true"/> the <see cref="PubSubEvent"/> will hold a strong reference to the action, otherwise a weak reference will be kept</param>
        /// <param name="filter">Filter that evaluates if the subscriber should receive the event</param>
        /// <returns>Returns <see cref="SubscriptionToken"/> that uniquely identifies the subscription</returns>
        public virtual SubscriptionToken Subscribe(Action<TPayLoad> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<TPayLoad>? filter)
        {
            var actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
            DelegateReference filterReference;
            if (filter != null)
            {
                filterReference = new DelegateReference(filter, keepSubscriberReferenceAlive);
            }
            else
            {
                filterReference = new DelegateReference(new Predicate<TPayLoad>(delegate { return true; }), true);
            }

            EventSubscription<TPayLoad> subscription;
            switch (threadOption)
            {
                case ThreadOption.BackgroundThread:
                    subscription = new BackgroundEventSubscription<TPayLoad>(actionReference, filterReference);
                    break;

                case ThreadOption.PublisherThread:
                    subscription = new EventSubscription<TPayLoad>(actionReference, filterReference);
                    break;

                case ThreadOption.UIThread:
                    if (SynchronizationContext == null)
                    {
                        throw new ArgumentNullException(nameof(SynchronizationContext));
                    }
                    subscription = new DispatcherEventSubscription<TPayLoad>(actionReference, filterReference, SynchronizationContext);
                    break;

                default:
                    subscription = new EventSubscription<TPayLoad>(actionReference, filterReference);
                    break;
            }

            return InternalSubscribe(subscription);
        }

        /// <summary>
        /// Unsubscribe a <see cref="Action"/> from the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="subscriber">The delegate to unsubscribe</param>
        public virtual void Unsubscribe(Action<TPayLoad> subscriber)
        {
            lock (Subscriptions)
            {
                var eventSubscription = Subscriptions.Cast<EventSubscription<TPayLoad>>().FirstOrDefault(evt => evt.GetAction == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        /// <summary>
        /// Publishe the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="arguments">The argument to pass to the subscribers</param>
        public virtual void Publish(TPayLoad argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(nameof(argument), "Use the Non Generic PubSubEvent for events without arguments");
            }
            InternalPublish(argument);
        }

        /// <summary>
        /// Check if a given <see cref="Action"/> is subscribed to the <see cref="PubSubEvent"/>
        /// </summary>
        /// <param name="subscriber">The <see cref="Action"/> to match against the subscriptions</param>
        /// <returns>Returns <see langword="true"/> if the subscription exists, otherwise returns <see langword="false"/></returns>
        public virtual bool Contains(Action<TPayLoad> subscriber)
        {
            lock (Subscriptions)
            {
                var subscription = Subscriptions.Cast<EventSubscription<TPayLoad>>().FirstOrDefault(evt => evt.GetAction == subscriber);
                return subscription != null;
            }
        }
    }
}
