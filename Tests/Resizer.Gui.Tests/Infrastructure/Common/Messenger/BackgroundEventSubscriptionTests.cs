using System;
using System.Threading;
using Resizer.Gui.Common.Messenger;
using Resizer.Gui.Tests.Infrastructure.Common.Messenger.Mocks;
using Xunit;

namespace Resizer.Gui.Tests.Infrastructure.Common.Messenger
{
    /// <summary>
    /// Tests for the <see cref="BackgroundEventSubscription"/>
    /// </summary>
    public class BackgroundEventSubscriptionTests
    {
        #region InvokeAction Tests

        [Fact]
        public void InvokeActionNonGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            var completedEvent = new ManualResetEvent(false);
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext? calledSyncContext = null; // TODO: calledSyncContext is always set to null, SynchronizationContext can be confusing

            var mockActionReference = new DelegateReferenceMock() { Delegate = (Action)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
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
            SynchronizationContext? calledSyncContext = null; // TODO: calledSyncContext is always set to null, SynchronizationContext can be confusing

            var mockActionReference = new DelegateReferenceMock() { Delegate = (Action<object>)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            var mockFilterReference = new DelegateReferenceMock() { Delegate = (Predicate<object>)delegate { return true; } };
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
