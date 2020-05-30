using System;
using Resizer.Gui.Helpers.Messenger;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.Messenger
{
    /// <summary>
    /// Tests for the <see cref="EventSubscription"/>
    /// </summary>
    public class EventSubscriptionTests
    {
        #region Test Fixtures

        private class MockDelegateReference : IDelegateReference
        {
            public Delegate? GetDelegate { get; set; }

            public MockDelegateReference()
            {

            }

            public MockDelegateReference(Delegate @delegate)
            {
                GetDelegate = @delegate;
            }
        }

        #endregion

        #region Constructor Tests

        [Fact]
        public void ConstructNonGeneric_NullActionReference_ShouldThrowArgumentNullException()
        {
            // Prepare

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var eventSubscription = new EventSubscription(null!);
            });
        }

        [Fact]
        public void ConstructGeneric_NullActionReference_ShouldThrowArgumentNullException()
        {
            // Prepare
            var mockFilterReference = new MockDelegateReference() { GetDelegate = (Predicate<object>)(arg => { return true; }) };

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(null!, mockFilterReference);
            });
        }

        [Fact]
        public void ConstructGeneric_NullFilterReference_ShouldThrowArgumentNullException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { GetDelegate = (Action<object>)delegate { } };

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, null!);
            });
        }

        [Fact]
        public void ConstructNonGeneric_NullAction_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { GetDelegate = null };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription(mockActionReference);
            });
        }

        [Fact]
        public void ConstructGeneric_NullAction_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { GetDelegate = null };
            var mockFilterReference = new MockDelegateReference() { GetDelegate = (Predicate<object>)(arg => { return true; }) };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            });
        }

        [Fact]
        public void ConstructGeneric_NullFilter_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { GetDelegate = (Action<object>)delegate { }};
            var mockFilterReference = new MockDelegateReference() { GetDelegate = null };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            });
        }

        [Fact]
        public void ConstructNonGeneric_DifferentTargetTypeActionReference_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { GetDelegate = (Action<int>)delegate { } };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription(mockActionReference);
            });
        }

        [Fact]
        public void ConstructGeneric_DifferentTargetTypeActionReference_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { GetDelegate = (Action<int>)delegate { } };
            var mockFilterReference = new MockDelegateReference() { GetDelegate = (Predicate<object>)(arg => { return true; }) };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            });
        }

        [Fact]
        public void ConstructGeneric_DifferentTargetTypeFilterReference_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { GetDelegate = (Action<object>)delegate { } };
            var mockFilterReference = new MockDelegateReference() { GetDelegate = (Predicate<int>)(arg => { return true; }) };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            });
        }

        #endregion
    }
}
