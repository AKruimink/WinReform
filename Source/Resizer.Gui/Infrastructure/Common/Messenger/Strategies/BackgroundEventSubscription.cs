using System;
using System.Threading.Tasks;

namespace Resizer.Gui.Infrastructure.Common.Messenger.Strategies
{
    /// <summary>
    /// Defines a subscription to a event that executes on a background thread
    /// </summary>
    public class BackgroundEventSubscription : EventSubscription
    {
        /// <summary>
        /// Create a new <see cref="BackgroundEventSubscription"/> that represents a subscription to an event that get executed on a background thread
        /// </summary>
        /// <param name="actionReference"><see cref="DelegateReference"/> of the <see cref="Action"/> to execute when the subscribed event is published</param>
        public BackgroundEventSubscription(IDelegateReference actionReference) : base(actionReference)
        {
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

            Task.Run(action);
        }
    }

    /// <summary>
    /// Defines a subscription to a event that executes on a background thread
    /// </summary>
    /// <typeparam name="TPayload">The type of the generic arguments used for <see cref="Action{TPayload}"/> and <see cref="Predicate{TPayload}"/></typeparam>
    public class BackgroundEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        /// <summary>
        /// Create a new <see cref="BackgroundEventSubscription"/> that represents a subscription to an event that get executed on a background thread
        /// </summary>
        /// <param name="actionReference"><see cref="DelegateReference"/> of the <see cref="Action{T}"/> to execute when the subscribed event is published</param>
        /// <param name="filterReference"><see cref="DelegateReference"/> of a <see cref="Predicate{T}"/> with criteria that need to match for the subscriber callback to invoke</param>
        public BackgroundEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference) : base(actionReference, filterReference)
        {
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

            Task.Run(() => action(argument));
        }
    }
}
