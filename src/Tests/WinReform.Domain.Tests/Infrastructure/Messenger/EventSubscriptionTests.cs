using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Infrastructure.Messenger.Strategies;
using System;
using Xunit;
using Moq;

namespace WinReform.Domain.Tests.Infrastructure.Messenger
{
    /// <summary>
    /// Tests for the <see cref="EventSubscription"/>
    /// </summary>
    public class EventSubscriptionTests
    {
        #region Constructor Tests

        [Fact]
        public void ConstructNonGeneric_NullActionReference_ShouldThrowArgumentNullException()
        {
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
            var delegateReferenceMock = new Mock<IDelegateReference>();
            delegateReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return true; });

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(null!, delegateReferenceMock.Object);
            });
        }

        [Fact]
        public void ConstructGeneric_NullFilterReference_ShouldThrowArgumentNullException()
        {
            // Prepare
            var delegateReferenceMock = new Mock<IDelegateReference>();
            delegateReferenceMock.Setup(x => x.Delegate).Returns((Action<object>)delegate { });

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(delegateReferenceMock.Object, null!);
            });
        }

        [Fact]
        public void ConstructNonGeneric_NullAction_ShouldThrowArgumentException()
        {
            // Prepare
            var delegateReferenceMock = new Mock<IDelegateReference>();
            delegateReferenceMock.Setup(x => x.Delegate).Returns(null);

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription(delegateReferenceMock.Object);
            });
        }

        [Fact]
        public void ConstructGeneric_NullAction_ShouldThrowArgumentException()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns(null);
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return true; });

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            });
        }

        [Fact]
        public void ConstructGeneric_NullFilter_ShouldThrowArgumentException()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<object>)delegate { });
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns(null);

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            });
        }

        [Fact]
        public void ConstructNonGeneric_DifferentTargetTypeActionReference_ShouldThrowArgumentException()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<int>)delegate { });

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription(delegateActionReferenceMock.Object);
            });
        }

        [Fact]
        public void ConstructGeneric_DifferentTargetTypeActionReference_ShouldThrowArgumentException()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<int>)delegate { });
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return true; });

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            });
        }

        [Fact]
        public void ConstructGeneric_DifferentTargetTypeFilterReference_ShouldThrowArgumentException()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<object>)delegate { });
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<int>)delegate { return true; });

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            });
        }

        #endregion Constructor Tests

        #region SubscriptionToken Tests

        [Fact]
        public void SubscriptionTokenNonGeneric_Intialize_ShouldInitialize()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action)delegate { });

            var eventSubscription = new EventSubscription(delegateActionReferenceMock.Object);
            var subscriptionToken = new SubscriptionToken();

            // Act
            eventSubscription.SubscriptionToken = subscriptionToken;

            // Assert
            Assert.Same(delegateActionReferenceMock.Object, eventSubscription.GetAction);
            Assert.Same(subscriptionToken, eventSubscription.SubscriptionToken);
        }

        [Fact]
        public void SubscriptionTokenGeneric_Intialize_ShouldInitialize()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<object>)delegate { });
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return true; });

            var eventSubscription = new EventSubscription<object>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            var subscriptionToken = new SubscriptionToken();

            // Act
            eventSubscription.SubscriptionToken = subscriptionToken;

            // Assert
            Assert.Same(delegateActionReferenceMock.Object, eventSubscription.GetAction);
            Assert.Same(delegateFilterReferenceMock.Object, eventSubscription.GetFilter);
            Assert.Same(subscriptionToken, eventSubscription.SubscriptionToken);
        }

        #endregion SubscriptionToken Tests

        #region GetAction Tests

        [Fact]
        public void GetActionNonGeneric_NullAction_ShouldReturnNull()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action)delegate { });
            var eventSubscription = new EventSubscription(delegateActionReferenceMock.Object);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns(null);
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        [Fact]
        public void GetActionGeneric_NullAction_ShouldReturnNull()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<object>)delegate { });
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return true; });

            var eventSubscription = new EventSubscription<object>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns(null);
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        #endregion GetAction Tests

        #region GetExecutionStrategy Tests

        [Fact]
        public void GetExecutionStrategyGeneric_PassArgument_ShouldPassArgumentToDelegates()
        {
            // Prepare
            string? passedArgumentToAction = null;
            string? passedArgumentToFilter = null;

            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<string>)(obj => { passedArgumentToAction = obj; }));
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<string>)(obj => { passedArgumentToFilter = obj; return true; }));

            var eventSubscription = new EventSubscription<string>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(new[] { "TestString" });

            // Assert
            Assert.Equal("TestString", passedArgumentToAction);
            Assert.Equal("TestString", passedArgumentToFilter);
        }

        [Fact]
        public void GetExecutionStrategyNonGeneric_NullAction_ShouldReturnNull()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action)delegate { });

            var eventSubscription = new EventSubscription(delegateActionReferenceMock.Object);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns(null);
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        [Fact]
        public void GetExecutionStrategyGeneric_NullAction_ShouldReturnNull()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<object>)delegate { });
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return false; });

            var eventSubscription = new EventSubscription<int>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns(null);
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        [Fact]
        public void GetExecutionStrategyGeneric_NullFilter_ShouldReturnNull()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<object>)delegate { });
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return false; });

            var eventSubscription = new EventSubscription<int>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns(null);
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        [Fact]
        public void GetExecutionStrategyGeneric_FilterReturnsFalse_ShouldNotExecute()
        {
            // Prepare
            var actionExecuted = false;

            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<int>)(obj => actionExecuted = true)) ;
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<int>)delegate { return false; });

            var eventSubscription = new EventSubscription<int>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(new object[] { null! });

            // Assert
            Assert.False(actionExecuted);
        }

        #endregion GetExecutionStrategy Tests

        #region InvokeAction Tests

        [Fact]
        public void InvokeActionNonGeneric_ValidAction_ShouldExecuteAction()
        {
            // Prepare
            var actionExecuted = false;

            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action)delegate { actionExecuted = true; });

            var eventSubscription = new EventSubscription(delegateActionReferenceMock.Object);

            // Act
            eventSubscription.InvokeAction((Action)delegateActionReferenceMock.Object.Delegate!);

            // Assert
            Assert.True(actionExecuted);
        }

        [Fact]
        public void InvokeActionGeneric_ValidAction_ShouldExecuteAction()
        {
            // Prepare
            var actionExecuted = false;

            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<object>)(obj => actionExecuted = true));
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return false; });

            var eventSubscription = new EventSubscription<object>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);

            // Act
            eventSubscription.InvokeAction((Action<object>)delegateActionReferenceMock.Object.Delegate!, "testString");

            // Assert
            Assert.True(actionExecuted);
        }

        [Fact]
        public void InvokeActionNonGeneric_NullAction_ShouldThrowArgumentNullException()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action)delegate { });
            var eventSubscription = new EventSubscription(delegateActionReferenceMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                eventSubscription.InvokeAction(null!);
            });
        }

        [Fact]
        public void InvokeActionGeneric_NullAction_ShouldThrowArgumentNullException()
        {
            // Prepare
            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action)delegate { });
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<object>)delegate { return true; });

            var eventSubscription = new EventSubscription<object>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                eventSubscription.InvokeAction(null!, "testString");
            });
        }

        [Fact]
        public void InvokeActionGeneric_PassArgument_ShouldPassArgument()
        {
            // Prepare
            string? passedArgument = null;

            var delegateActionReferenceMock = new Mock<IDelegateReference>();
            delegateActionReferenceMock.Setup(x => x.Delegate).Returns((Action<string>)(obj => passedArgument = obj));
            var delegateFilterReferenceMock = new Mock<IDelegateReference>();
            delegateFilterReferenceMock.Setup(x => x.Delegate).Returns((Predicate<string>)delegate { return true; });

            var eventSubscription = new EventSubscription<string>(delegateActionReferenceMock.Object, delegateFilterReferenceMock.Object);

            // Act
            eventSubscription.InvokeAction((Action<string>)delegateActionReferenceMock.Object.Delegate!, "someString");

            // Assert
            Assert.Equal("someString", passedArgument);
        }

        #endregion InvokeAction Tests
    }
}
