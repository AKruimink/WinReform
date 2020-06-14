using System;
using System.Threading;

namespace Resizer.Gui.Infrastructure.Common.Messenger.Strategies
{
    /// <summary>
    /// Defines a subscription to a event that executes on a given <see cref="SynchronizationContext"/>
    /// </summary>
    public class DispatcherEventSubscription : EventSubscription
    {
        /// <summary>
        /// <see cref="SynchronizationContext"/> on which to invoke the subscription event
        /// </summary>
        private readonly SynchronizationContext _synchronizationContext;

        /// <summary>
        /// Create a new <see cref="DispatcherEventSubscription"/> that represents a subscription to an event that get executed on a given <see cref="SynchronizationContext"/>
        /// </summary>
        /// <param name="actionReference"><see cref="DelegateReference"/> of the <see cref="Action"/> to execute when the subscribed event is published</param>
        /// <param name="context"><see cref="SynchronizationContext"/> on which to execute the event <see cref="Action"/></param>
        public DispatcherEventSubscription(IDelegateReference actionReference, SynchronizationContext context) : base(actionReference)
        {
            _synchronizationContext = context;
        }

        /// <summary>
        /// Invokes a <see cref="Action"/> asynchronous thread by using <see cref="Task"/>
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke</param>
        public override void InvokeAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            _synchronizationContext.Post((o) => action(), null);
        }
    }

    /// <summary>
    /// Defines a subscription to a event that executes on a given <see cref="SynchronizationContext"/>
    /// </summary>
    /// <typeparam name="TPayload">The type of the generic arguments used for <see cref="Action{TPayload}"/> and <see cref="Predicate{TPayload}"/></typeparam>
    public class DispatcherEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        /// <summary>
        /// <see cref="SynchronizationContext"/> on which to invoke the subscription event
        /// </summary>
        private readonly SynchronizationContext _synchronizationContext;

        /// <summary>
        /// Create a new <see cref="DispatcherEventSubscription"/> that represents a subscription to an event that get executed on a given <see cref="SynchronizationContext"/>
        /// </summary>
        /// <param name="actionReference"><see cref="DelegateReference"/> of the <see cref="Action{T}"/> to execute when the subscribed event is published</param>
        /// <param name="filterReference"><see cref="DelegateReference"/> of a <see cref="Predicate{T}"/> with criteria that need to match for the subscriber callback to invoke</param>
        /// <param name="context"><see cref="SynchronizationContext"/> on which to execute the event <see cref="Action{T}"/></param>
        public DispatcherEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference, SynchronizationContext context) : base(actionReference, filterReference)
        {
            _synchronizationContext = context;
        }

        /// <summary>
        /// Invokes a <see cref="Action{T}"/> asynchronous thread by using <see cref="Task"/>
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to invoke</param>
        /// <param name="argument">The arguments to be passed to the invoked <see cref="Action"/></param>
        public override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            //TODO argument possible null reference is suppressed, null should be allowed to be passed, but was unable to figure nullable out for it at this time.
            // or we should just throw during a null argument, when no arguments are passed the non generic type should probably just be used
            _synchronizationContext.Post((o) => action((TPayload)o!), argument);
        }
    }
}
