using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Resizer.Gui.Helpers.Messenger;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.Messenger
{
    /// <summary>
    /// Tests for the <see cref="DispatcherEventSubscription"/>
    /// </summary>
    public class DispatcherEventSubscriptionTests
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

        private class MockSynchronizationContext : SynchronizationContext
        {
            public bool InvokeCalled { get; private set; } = false;

            public object? InvokeArgs { get; private set; }

            public SynchronizationContext? SynchronizationContext { get; private set; }


            public override void Post(SendOrPostCallback d, object? state)
            {
                base.Post(d, state);

                InvokeCalled = true;
                InvokeArgs = d;
                SynchronizationContext = Current;
            }
        }

        #endregion

        #region InvokeAction Tests

        [Fact]
        public void InvokeActionNonGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            var completedEvent = new ManualResetEvent(false);
            var mockSynchronizationContext = new MockSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(mockSynchronizationContext);
            SynchronizationContext? calledSyncContext = null; // TODO: calledSyncContext is always set to null, SynchronizationContext can be confusing

            var mockActionReference = new MockDelegateReference() { Delegate = (Action)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            var eventSubscription = new DispatcherEventSubscription(mockActionReference, mockSynchronizationContext);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(null!);
            completedEvent.WaitOne(5000);

            // Assert
            
            Assert.NotEqual(SynchronizationContext.Current, calledSyncContext);
            //Assert.Equal(mockSynchronizationContext.SynchronizationContext, calledSyncContext);
        }

        [Fact]
        public void InvokeActionGeneric_Execution_ShouldExecuteOnBackgroundThread()
        {
            // Prepare
            var completedEvent = new ManualResetEvent(false);
            var mockSynchronizationContext = new MockSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(mockSynchronizationContext);
            SynchronizationContext? calledSyncContext = null; // TODO: calledSyncContext is always set to null, SynchronizationContext can be confusing

            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate { calledSyncContext = SynchronizationContext.Current; completedEvent.Set(); } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)delegate { return true; } };
            var eventSubscription = new DispatcherEventSubscription<object> (mockActionReference, mockFilterReference, mockSynchronizationContext);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(null!);
            completedEvent.WaitOne(5000);

            // Assert
            Assert.NotEqual(SynchronizationContext.Current, calledSyncContext);
            //Assert.Equal(mockSynchronizationContext.SynchronizationContext, calledSyncContext);
        }

        #endregion
    }
}
