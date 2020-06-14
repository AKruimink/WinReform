using System;
using System.Globalization;

namespace Resizer.Gui.Infrastructure.Common.Messenger.Strategies
{
    /// <summary>
    /// Defines a subscription to a event that executes on the publisher thread
    /// </summary>
    public class EventSubscription : IEventSubscription
    {
        /// <summary>
        /// Action to be executed when the subscribed event is published
        /// </summary>
        private readonly IDelegateReference _actionReference;

        ///<inheritdoc/>
        public SubscriptionToken? SubscriptionToken { get; set; }

        /// <summary>
        /// Create a new <see cref="EventSubscription"/> that represents a subscription to an event that get executed on the publisher thread
        /// </summary>
        /// <param name="actionReference"><see cref="DelegateReference"/> of the <see cref="Action"/> to execute when the subscribed event is published</param>
        public EventSubscription(IDelegateReference actionReference)
        {
            if (actionReference == null)
            {
                throw new ArgumentNullException(nameof(actionReference));
            }

            if (!(actionReference.Delegate is Action))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid Delegate Reference Type Exception", typeof(Action).FullName, nameof(actionReference)));
            }

            _actionReference = actionReference;
        }

        /// <summary>
        /// Gets the <see cref="Action"/> that is referenced by the <see cref="DelegateReference"/>
        /// </summary>
        public Action? GetAction
        {
            get => (Action?)_actionReference.Delegate;
        }

        ///<inheritdoc/>
        public virtual Action<object[]>? GetExecutionStrategy()
        {
            var action = GetAction;
            if (action != null)
            {
                return arguments =>
                {
                    InvokeAction(action);
                };
            }
            return null;
        }

        /// <summary>
        /// Invokes a <see cref="Action"/> synchronously
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke</param>
        public virtual void InvokeAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action();
        }
    }

    /// <summary>
    /// Defines a subscription to a event that executes on the publisher thread
    /// </summary>
    /// <typeparam name="TPayload">The type of the generic arguments used for <see cref="Action{TPayload}"/> and <see cref="Predicate{TPayload}"/></typeparam>
    public class EventSubscription<TPayload> : IEventSubscription
    {
        /// <summary>
        /// <see cref="Action{TPayload}"/> to be executed when the subscribed event is published
        /// </summary>
        private readonly IDelegateReference _actionReference;

        /// <summary>
        /// <see cref="Predicate{TPayLoad}"/> with criteria that need to match for the subscriber callback to invoke
        /// </summary>
        private readonly IDelegateReference _filterReference;

        ///<inheritdoc/>
        public SubscriptionToken? SubscriptionToken { get; set; }

        /// <summary>
        /// Create a new <see cref="EventSubscription"/> that represents a subscription to an event that get executed on the publisher thread
        /// </summary>
        /// <param name="actionReference"><see cref="DelegateReference"/> of the <see cref="Action{T}"/> to execute when the subscribed event is published</param>
        /// <param name="filterReference"><see cref="DelegateReference"/> of a <see cref="Predicate{T}"/> with criteria that need to match for the subscriber callback to invoke</param>
        public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            if (actionReference == null)
            {
                throw new ArgumentNullException(nameof(actionReference));
            }

            if (!(actionReference.Delegate is Action<TPayload>))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid Delegate Reference Type Exception", typeof(Action<TPayload>).FullName, nameof(actionReference)));
            }

            if (filterReference == null)
            {
                throw new ArgumentNullException(nameof(filterReference));
            }

            if (!(filterReference.Delegate is Predicate<TPayload>))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid Delegate Reference Type Exception", typeof(Predicate<TPayload>).FullName, nameof(filterReference)));
            }

            _actionReference = actionReference;
            _filterReference = filterReference;
        }

        /// <summary>
        /// Gets the <see cref="Action{TPayload}"/> that is referenced by the <see cref="DelegateReference"/>
        /// </summary>
        public Action<TPayload>? GetAction
        {
            get => (Action<TPayload>?)_actionReference.Delegate;
        }

        /// <summary>
        /// Gets the <see cref="Predicate{TPayload}"/> that is referenced by the <see cref="DelegateReference"/>
        /// </summary>
        public Predicate<TPayload>? GetFilter
        {
            get => (Predicate<TPayload>?)_filterReference.Delegate;
        }

        ///<inheritdoc/>
        public virtual Action<object[]>? GetExecutionStrategy()
        {
            var action = GetAction;
            var filter = GetFilter;
            if (action != null && filter != null)
            {
                return arguments =>
                {
                    var argument = default(TPayload);
                    if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                    {
                        argument = (TPayload)arguments[0];
                    }
                    //TODO argument possible null reference is suppressed, null should be allowed to be passed, but was unable to figure nullable out for it at this time.
                    // or we should just throw during a null argument, when no arguments are passed the non generic type should probably just be used
                    if (filter(argument!))
                    {
                        InvokeAction(action, argument!);
                    }
                };
            }
            return null;
        }

        /// <summary>
        /// Invokes a <see cref="Action"/> synchronously
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke</param>
        /// <param name="argument">The arguments to be passed to the invoked <see cref="Action"/></param>
        public virtual void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(argument);
        }
    }
}
