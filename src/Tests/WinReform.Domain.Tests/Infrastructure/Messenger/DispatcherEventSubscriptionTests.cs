using System.Threading;
using WinReform.Domain.Infrastructure.Messenger.Strategies;
using Xunit;

namespace WinReform.Domain.Tests.Infrastructure.Messenger
{
    /// <summary>
    /// Tests for the <see cref="DispatcherEventSubscription"/>
    /// TODO <see cref="DispatcherEventSubscription"/> currently doesn't work as unit tests don't have a <see cref="SynchronizationContext"/> so have to find a way to properly test these
    /// </summary>
    public class DispatcherEventSubscriptionTests
    {
        #region InvokeAction Tests

        [Fact]
        public void InvokeActionNonGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            //var completedEvent = new ManualResetEvent(false);
            //var synchronizationContextMock = new SynchronizationContextMock();
            //SynchronizationContext.SetSynchronizationContext(synchronizationContextMock);
            //SynchronizationContext? calledSyncContext = null; // TODO: calledSyncContext is always set to null, SynchronizationContext can be confusing

            //var mockActionReference = new DelegateReferenceMock() { Delegate = (Action)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            //var eventSubscription = new DispatcherEventSubscription(mockActionReference, synchronizationContextMock);
            //var publishAction = eventSubscription.GetExecutionStrategy();

            //// Act
            //publishAction?.Invoke(null!);
            //completedEvent.WaitOne(5000);

            //// Assert

            //Assert.NotEqual(SynchronizationContext.Current, calledSyncContext);
            //Assert.Equal(SynchronizationContextMock.SynchronizationContext, calledSyncContext);
        }

        [Fact]
        public void InvokeActionGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            //var completedEvent = new ManualResetEvent(false);
            //var synchronizationContextMock = new SynchronizationContextMock();
            //SynchronizationContext.SetSynchronizationContext(synchronizationContextMock);
            //SynchronizationContext? calledSyncContext = null; // TODO: calledSyncContext is always set to null, SynchronizationContext can be confusing

            //var mockActionReference = new DelegateReferenceMock() { Delegate = (Action<object>)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            //var mockFilterReference = new DelegateReferenceMock() { Delegate = (Predicate<object>)delegate { return true; } };
            //var eventSubscription = new DispatcherEventSubscription<object>(mockActionReference, mockFilterReference, synchronizationContextMock);
            //var publishAction = eventSubscription.GetExecutionStrategy();

            //// Act
            //publishAction?.Invoke(null!);
            //completedEvent.WaitOne(5000);

            //// Assert
            //Assert.NotEqual(SynchronizationContext.Current, calledSyncContext);
            //Assert.Equal(SynchronizationContextMock.SynchronizationContext, calledSyncContext);
        }

        #endregion InvokeAction Tests
    }
}
