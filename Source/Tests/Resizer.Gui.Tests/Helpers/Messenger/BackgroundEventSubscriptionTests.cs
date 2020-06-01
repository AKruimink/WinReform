using System;
using System.Threading;
using Resizer.Gui.Helpers.Messenger;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.Messenger
{
    /// <summary>
    /// Tests for the <see cref="BackgroundEventSubscription"/>
    /// </summary>
    public class BackgroundEventSubscriptionTests
    {
        #region Test Fixtures

        private class MockDelegateReference : IDelegateReference
        {
            public Delegate? Delegate { get; set; }

            public MockDelegateReference()
            {

            }

            public MockDelegateReference(Delegate @delegate)
            {
                Delegate = @delegate;
            }
        }

        #endregion

        #region InvokeAction Tests

        [Fact]
        public void InvokeActionNonGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            var completedEvent = new ManualResetEvent(false);
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext? calledSyncContext = null;

            var mockActionReference = new MockDelegateReference() { Delegate = (Action)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            var eventSubscription = new BackgroundEventSubscription(mockActionReference);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(null!);
            completedEvent.WaitOne(5000);

            // Assert
            Assert.NotEqual(SynchronizationContext.Current, calledSyncContext);
        }

        [Fact]
        public void InvokeActionGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            var completedEvent = new ManualResetEvent(false);
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext? calledSyncContext = null;

            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)delegate { return true; } };
            var eventSubscription = new BackgroundEventSubscription<object>(mockActionReference, mockFilterReference);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(null!);
            completedEvent.WaitOne(5000);

            // Assert
            Assert.NotEqual(SynchronizationContext.Current, calledSyncContext);
        }

        #endregion
    }
}
