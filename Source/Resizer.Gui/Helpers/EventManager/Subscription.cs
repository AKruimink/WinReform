using System;
using System.Reflection;

namespace Resizer.Gui.Helpers.EventManager
{
    /// <summary>
    /// Defines a event subscription
    /// </summary>
    internal struct Subscription
    {
        /// <summary>
        /// The target instance to invoke the handler on
        /// </summary>
        public WeakReference? Subscriber { get; }

        /// <summary>
        /// The event handler
        /// </summary>
        public MethodInfo Handler { get; }

        /// <summary>
        /// Creates a new <see cref="Subscription"/>
        /// </summary>
        /// <param name="subscriber">The <see cref="WeakReference"/> of the target instance to invoke on</param>
        /// <param name="handler">The <see cref="MethodInfo"/> of the event handler</param>
        public Subscription(in WeakReference? subscriber, in MethodInfo? handler)
        {
            Subscriber = subscriber;
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }
    }
}
