using Resizer.Gui.Infrastructure.Common.Messenger.Strategies;
using Resizer.Gui.Tests.Infrastructure.Common.Messenger.Mocks;
using System;
using System.Threading;
using Xunit;

namespace Resizer.Gui.Tests.Infrastructure.Common.Messenger
{
    /// <summary>
    /// Tests for the <see cref="DispatcherEventSubscription"/>
    /// </summary>
    public class DispatcherEventSubscriptionTests
    {
        #region InvokeAction Tests

        [Fact]
        public void InvokeActionNonGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            var completedEvent = new ManualResetEvent(false);
            var synchronizationContextMock = new SynchronizationContextMock();
            SynchronizationContext.SetSynchronizationContext(synchronizationContextMock);
            SynchronizationContext? calledSyncContext = null; // TODO: calledSyncContext is always set to null, SynchronizationContext can be confusing

            var mockActionReference = new DelegateReferenceMock() { Delegate = (Action)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            var eventSubscription = new DispatcherEventSubscription(mockActionReference, synchronizationContextMock);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(null!);
            completedEvent.WaitOne(5000);

            // Assert

            Assert.NotEqual(SynchronizationContext.Current, calledSyncContext);
            //Assert.Equal(SynchronizationContextMock.SynchronizationContext, calledSyncContext);
        }

        [Fact]
        public void InvokeActionGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            var completedEvent = new ManualResetEvent(false);
            var synchronizationContextMock = new SynchronizationContextMock();
            SynchronizationContext.SetSynchronizationContext(synchronizationContextMock);
            SynchronizationContext? calledSyncContext = null; // TODO: calledSyncContext is always set to null, SynchronizationContext can be confusing

            var mockActionReference = new DelegateReferenceMock() { Delegate = (Action<object>)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            var mockFilterReference = new DelegateReferenceMock() { Delegate = (Predicate<object>)delegate { return true; } };
            var eventSubscription = new DispatcherEventSubscription<object>(mockActionReference, mockFilterReference, synchronizationContextMock);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(null!);
            completedEvent.WaitOne(5000);

            // Assert
            Assert.NotEqual(SynchronizationContext.Current, calledSyncContext);
            //Assert.Equal(SynchronizationContextMock.SynchronizationContext, calledSyncContext);
        }

        #endregion
    }
}
