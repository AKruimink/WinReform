using System.Threading;

namespace Resizer.Gui.Tests.Common.Messenger.Mocks
{
    /// <summary>
    /// Defines a mock implementation of <see cref="SynchronizationContext"/>
    /// </summary>
    public class SynchronizationContextMock : SynchronizationContext
    {
        /// <summary>
        /// Indicates if the invoke has been called
        /// </summary>
        public bool InvokeCalled { get; set; } = false;

        /// <summary>
        /// Arguments passed to along during the invoke
        /// </summary>
        public object? InvokeArgs { get; set; }
         
        /// <summary>
        /// <see cref="SynchronizationContext"/> that the invoke is being executed on
        /// </summary>
        public SynchronizationContext? SynchronizationContext { get; set; }

        ///<inheritdoc/>
        public override void Post(SendOrPostCallback d, object? state)
        {
            InvokeCalled = true;
            InvokeArgs = d;
            SynchronizationContext = Current;

            base.Post(d, state);
        }
    }
}
