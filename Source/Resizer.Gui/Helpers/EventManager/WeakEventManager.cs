using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Resizer.Gui.Helpers.EventManager
{
    #region IWeakEventManager

    /// <summary>
    /// Represents a weak event manager that allows garbage collection on subscribed event handlers
    /// </summary>
    /// <typeparam name="T">The event args of the event</typeparam>
    public interface IWeakEventManager
    {
        /// <summary>
        /// Add a new event handler
        /// </summary>
        /// <param name="handler">The handler to add</param>
        /// <param name="eventName">The name of the event to subscribe to</param>
        void AddEventHandler(Delegate handler, [CallerMemberName] string eventName = "");

        /// <summary>
        /// Remove a existing event handler
        /// </summary>
        /// <param name="handler">The handler to remove</param>
        /// <param name="eventName">The name of the event to remove the subscription from</param>
        void RemoveEventHandler(Delegate handler, [CallerMemberName] string eventName = "");

        /// <summary>
        /// Invoke all event handlers subscribed to a fired event
        /// </summary>
        /// <param name="sender">The event that executed the event</param>
        /// <param name="eventArgs">The arguments to pass to the event handler</param>
        /// <param name="eventName">The name of the event that fired</param>
        void HandleEvent(object? sender, object eventArgs, string eventName);

        /// <summary>
        /// Invoke all event handlers subscribed to a fired event
        /// </summary>
        /// <param name="eventName">The name of the event that fired</param>
        void HandleEvent(string eventName);
    }

    /// <summary>
    /// Represents a weak event manager that allows garbage collection on subscribed event handlers
    /// </summary>
    /// <typeparam name="T">The type of event argument</typeparam>
    public interface IWeakEventManager<TEventArgs>
    {
        /// <summary>
        /// Add a new event handler
        /// </summary>
        /// <param name="handler">The handler to add</param>
        /// <param name="eventName">The name of the event to subscribe to</param>
        void AddEventHandler(EventHandler<TEventArgs> handler, [CallerMemberName] string eventName = "");

        /// <summary>
        /// Add a new event handler
        /// </summary>
        /// <param name="action">The handler to add</param>
        /// <param name="eventName">The name of the event to subscribe to</param>
        void AddEventHandler(Action<TEventArgs> action, [CallerMemberName] string eventName = "");

        /// <summary>
        /// Remove a existing event handler
        /// </summary>
        /// <param name="handler">The handler to remove</param>
        /// <param name="eventName">The name of the event to remove the subscription from</param>
        void RemoveEventHandler(EventHandler<TEventArgs> handler, [CallerMemberName] string eventName = "");

        /// <summary>
        /// Remove a existing event handler
        /// </summary>
        /// <param name="action">The handler to remove</param>
        /// <param name="eventName">The name of the event to remove the subscription from</param>
        void RemoveEventHandler(Action<TEventArgs> action, [CallerMemberName] string eventName = "");

        /// <summary>
        /// Invoke all event handlers subscribed to a fired event
        /// </summary>
        /// <param name="sender">The event that executed the event</param>
        /// <param name="eventArgs">The arguments to pass to the event handler</param>
        /// <param name="eventName">The name of the event that fired</param>
        void HandleEvent(object? sender, TEventArgs eventArgs, string eventName);

        /// <summary>
        /// Invoke all event handlers subscribed to a fired event
        /// </summary>
        /// <param name="eventArgs">The arguments to pass to the event handler</param>
        /// <param name="eventName">The name of the event that fired</param>
        void HandleEvent(TEventArgs eventArgs, string eventName);
    }

    #endregion

    /// <summary>
    /// Defines a weak event manager that allows garbage collection on subscribed event handlers
    /// </summary>
    public class WeakEventManager : IWeakEventManager
    {
        /// <summary>
        /// The events and their subscriptions
        /// </summary>
        private readonly Dictionary<string, List<Subscription>> _eventHandlers = new Dictionary<string, List<Subscription>>();

        /// <inheritdoc/>
        public void AddEventHandler(Delegate handler, [CallerMemberName] string eventName = "")
        {
            if (handler is null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentNullException(nameof(eventName));

            EventManagerService.AddEventHandler(eventName, handler.Target, handler.GetMethodInfo(), _eventHandlers);
        }

        /// <inheritdoc/>
        public void RemoveEventHandler(Delegate handler, [CallerMemberName] string eventName = "")
        {
            if (handler is null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentNullException(nameof(eventName));

            EventManagerService.RemoveEventHandler(eventName, handler.Target, handler.GetMethodInfo(), _eventHandlers);
        }

        /// <inheritdoc/>
        public void HandleEvent(object? sender, object eventArgs, string eventName)
        {
            EventManagerService.HandleEvent(eventName, sender, eventArgs, _eventHandlers);
        }

        /// <inheritdoc/>
        public void HandleEvent(string eventName)
        {
            EventManagerService.HandleEvent(eventName, _eventHandlers);
        }
    }

    /// <summary>
    /// Defines a weak event manager that allows garbage collection on subscribed event handlers
    /// </summary>
    /// <typeparam name="TEventArgs">The type of event argument</typeparam>
    public class WeakEventManager<TEventArgs> : IWeakEventManager<TEventArgs>
    {
        /// <summary>
        /// The events and their subscriptions
        /// </summary>
        private readonly Dictionary<string, List<Subscription>> _eventHandlers = new Dictionary<string, List<Subscription>>();

        /// <inheritdoc/>
        public void AddEventHandler(EventHandler<TEventArgs> handler, [CallerMemberName] string eventName = "")
        {
            if (handler is null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentNullException(nameof(eventName));

            EventManagerService.AddEventHandler(eventName, handler.Target, handler.GetMethodInfo(), _eventHandlers);
        }

        /// <inheritdoc/>
        public void AddEventHandler(Action<TEventArgs> action, [CallerMemberName] string eventName = "")
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentNullException(nameof(eventName));

            EventManagerService.AddEventHandler(eventName, action.Target, action.GetMethodInfo(), _eventHandlers);
        }

        /// <inheritdoc/>
        public void RemoveEventHandler(EventHandler<TEventArgs> handler, [CallerMemberName] string eventName = "")
        {
            if (handler is null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentNullException(nameof(eventName));

            EventManagerService.RemoveEventHandler(eventName, handler.Target, handler.GetMethodInfo(), _eventHandlers);
        }

        /// <inheritdoc/>
        public void RemoveEventHandler(Action<TEventArgs> action, [CallerMemberName] string eventName = "")
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentNullException(nameof(eventName));

            EventManagerService.RemoveEventHandler(eventName, action.Target, action.GetMethodInfo(), _eventHandlers);
        }

        /// <inheritdoc/>
        public void HandleEvent(object? sender, TEventArgs eventArgs, string eventName)
        {
            EventManagerService.HandleEvent(eventName, sender, eventArgs, _eventHandlers);
        }

        /// <inheritdoc/>
        public void HandleEvent(TEventArgs eventArgs, string eventName)
        {
            EventManagerService.HandleEvent(eventName, eventArgs, _eventHandlers);
        }
    }
}
