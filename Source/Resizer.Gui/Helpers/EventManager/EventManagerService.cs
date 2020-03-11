using System;
using System.Collections.Generic;
using System.Reflection;

namespace Resizer.Gui.Helpers.EventManager
{
    /// <summary>
    /// Defines a event manager service for handeling, removing and adding event handlers to the weak event manager
    /// </summary>
    internal static class EventManagerService
    {
        /// <summary>
        /// Add a event handler to an event
        /// </summary>
        /// <param name="eventName">The name of the event to add a handler to</param>
        /// <param name="handlerTarget">The instance on which the method info is invoked</param>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> of the event handler to be added</param>
        /// <param name="eventHandlers">The <see cref="Dictionary{TKey, TValue}"/> of event subscriptions<</param>
        internal static void AddEventHandler(string eventName, object? handlerTarget, MethodInfo? methodInfo, Dictionary<string, List<Subscription>> eventHandlers)
        {
            var containsSubscription = eventHandlers.TryGetValue(eventName, out var subscriptions);

            if (!containsSubscription || subscriptions is null)
            {
                subscriptions = new List<Subscription>();
                eventHandlers.Add(eventName, subscriptions);
            }

            if (handlerTarget is null)
            {
                subscriptions.Add(new Subscription(null, methodInfo));
            }
            else
            {
                subscriptions.Add(new Subscription(new WeakReference(handlerTarget), methodInfo));
            }
        }

        /// <summary>
        /// Remove a event handler from an event
        /// </summary>
        /// <param name="eventName">The name of the event to remove a handler from</param>
        /// <param name="handlerTarget">The instance on which the method info would be invoked</param>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> of the event handler to be removed</param>
        /// <param name="eventHandlers">The <see cref="Dictionary{TKey, TValue}"/> of event subscriptions</param>
        internal static void RemoveEventHandler(string eventName, object? handlerTarget, MethodInfo? methodInfo, Dictionary<string, List<Subscription>> eventHandlers)
        {
            var containsSubscription = eventHandlers.TryGetValue(eventName, out var subscriptions);

            if (!containsSubscription || subscriptions is null)
            {
                return;
            }

            for (var i = subscriptions.Count; i > 0; i--)
            {
                var current = subscriptions[i - 1];

                if (current.Subscriber?.Target != handlerTarget || current.Handler.Name != methodInfo?.Name)
                {
                    continue;
                }

                subscriptions.Remove(current);
                break;
            }
        }

        /// <summary>
        /// Handle a event
        /// </summary>
        /// <param name="eventName">The name of the event that fired</param>
        /// <param name="sender">The sender information to pass to the handler</param>
        /// <param name="eventArgs">The event args to pass to the handler</param>
        /// <param name="eventHandlers">The <see cref="Dictionary{TKey, TValue}"/> of event subscriptions<</param>
        internal static void HandleEvent(string eventName, object? sender, object? eventArgs, Dictionary<string, List<Subscription>> eventHandlers)
        {
            CleanSubscriptions(eventName, eventHandlers, out var toRaise);

            for (var i = 0; i < toRaise.Count; i++)
            {
                try
                {
                    var tuple = toRaise[i];
                    tuple.Item2.Invoke(tuple.Item1, new[] { sender, eventArgs });
                }
                catch (TargetParameterCountException ex)
                {
                    throw new InvalidHandleEventException("Parameter count mismatch. If invoking 'event Action` use `HandleEvent(string eventName)`, if invoking `event Action<T>` use `HandleEvent(object eventArgs, string eventName)`", ex);
                }
            }
        }

        /// <summary>
        /// Handle a event
        /// </summary>
        /// <param name="eventName">The name of the event that fired</param>
        /// <param name="eventArgs">The event args to pass to the handler</param>
        /// <param name="eventHandlers">The <see cref="Dictionary{TKey, TValue}"/> of event subscriptions<</param>
        internal static void HandleEvent(string eventName, object? eventArgs, Dictionary<string, List<Subscription>> eventHandlers)
        {
            CleanSubscriptions(eventName, eventHandlers, out var toRaise);

            for (var i = 0; i < toRaise.Count; i++)
            {
                try
                {
                    var tuple = toRaise[i];
                    tuple.Item2.Invoke(tuple.Item1, new[] { eventArgs });
                }
                catch (TargetParameterCountException ex)
                {
                    throw new InvalidHandleEventException("Parameter count mismatch. If invoking 'event Action` use `HandleEvent(string eventName)`, if invoking `event Action<T>` use `HandleEvent(object eventArgs, string eventName)`", ex);
                }
            }
        }

        /// <summary>
        /// Handle a event
        /// </summary>
        /// <param name="eventName">The name of the event that fired</param>
        /// <param name="eventHandlers">The <see cref="Dictionary{TKey, TValue}"/> of event subscriptions<</param>
        internal static void HandleEvent(string eventName, Dictionary<string, List<Subscription>> eventHandlers)
        {
            CleanSubscriptions(eventName, eventHandlers, out var toRaise);

            for (var i = 0; i < toRaise.Count; i++)
            {
                try
                {
                    var tuple = toRaise[i];
                    tuple.Item2.Invoke(tuple.Item1, null);
                }
                catch (TargetParameterCountException ex)
                {
                    throw new InvalidHandleEventException("Parameter count mismatch. If invoking 'event Action` use `HandleEvent(string eventName)`, if invoking `event Action<T>` use `HandleEvent(object eventArgs, string eventName)`", ex);
                }
            }
        }

        /// <summary>
        /// Cleanup disposed event handlers
        /// </summary>
        /// <param name="eventName">The name of the event to cleanup handlers from</param>
        /// <param name="eventHandlers">The <see cref="Dictionary{TKey, TValue}"/> of event subscriptions<</param>
        /// <param name="toRaise">The event handlers that still exist and need to be raised</param>
        private static void CleanSubscriptions(string eventName, Dictionary<string, List<Subscription>> eventHandlers, out List<Tuple<object?, MethodInfo>> toRaise)
        {
            var toRemove = new List<Subscription>();
            toRaise = new List<Tuple<object?, MethodInfo>>();

            var containsSubscription = eventHandlers.TryGetValue(eventName, out var subscriptions);

            if (!containsSubscription || subscriptions is null)
            {
                return;
            }

            for (var i = 0; i < subscriptions.Count; i++)
            {
                var subscription = subscriptions[i];
                var isStatic = subscription.Subscriber is null;

                if (isStatic)
                {
                    toRaise.Add(Tuple.Create<object?, MethodInfo>(null, subscription.Handler));
                    continue;
                }

                var subscriber = subscription.Subscriber?.Target;

                if (subscriber is null)
                {
                    toRemove.Add(subscription);
                }
                else
                {
                    toRaise.Add(Tuple.Create<object?, MethodInfo>(subscriber, subscription.Handler));
                }
            }

            for (var i = 0; i < toRemove.Count; i++)
            {
                var subscription = toRemove[i];
                subscriptions.Remove(subscription);
            }
        }
    }
}
