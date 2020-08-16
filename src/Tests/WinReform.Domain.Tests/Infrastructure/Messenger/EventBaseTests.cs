using Moq;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Infrastructure.Messenger.Strategies;
using WinReform.Tests.Fixtures;
using Xunit;

namespace WinReform.Domain.Tests.Infrastructure.Messenger
{
    /// <summary>
    /// Tests for the <see cref="EventBase"/>
    /// </summary>
    public class EventBaseTests
    {
        #region InternalSubscribe Tests

        [Fact]
        public void InternalSubscribe_SubscribeEvent_ShouldSubscribeToEvent()
        {
            // Prepare
            var eventBase = new EventFixture();
            var eventSubscriptionMock = new Mock<IEventSubscription>();

            // Act
            var token = eventBase.Subscribe(eventSubscriptionMock.Object);

            // Assert
            Assert.NotNull(token);
            Assert.True(eventBase.Contains(token));
        }

        #endregion InternalSubscribe Tests

        #region InternalUnsubscribe Tests

        [Fact]
        public void InternalUnsubscribe_UnsubscribeEvent_ShouldUnubscribeFromEvent()
        {
            // Prepare
            var eventBase = new EventFixture();
            var eventSubscriptionMock = new Mock<IEventSubscription>();
            var token = eventBase.Subscribe(eventSubscriptionMock.Object);

            // Assert
            Assert.NotNull(token);
            Assert.True(eventBase.Contains(token));

            // Act
            eventBase.Unsubscribe(token);

            // Assert
            Assert.False(eventBase.Contains(token));
        }

        #endregion InternalUnsubscribe Tests

        #region InternalPublish Tests

        [Fact]
        public void InternalPublish_PublishEvent_ShouldNotifySubscription()
        {
            // Prepare
            var eventPublished = false;
            var eventBase = new EventFixture();
            var eventSubscriptionMock = new Mock<IEventSubscription>();
            eventSubscriptionMock.Setup(x => x.GetExecutionStrategy()).Returns(delegate { eventPublished = true; });

            // Act
            eventBase.Subscribe(eventSubscriptionMock.Object);
            eventBase.Publish();

            // Assert
            eventSubscriptionMock.Verify(x => x, Times.Once());
            Assert.True(eventPublished);
        }

        [Fact]
        public void InternalPublish_PublishEvent_ShouldNotifyMultipleSubscription()
        {
            // Prepare
            var eventPublished1 = false;
            var eventPublished2 = false;
            var eventBase = new EventFixture();
            var eventSubscriptionMock1 = new Mock<IEventSubscription>();
            var eventSubscriptionMock2 = new Mock<IEventSubscription>();
            eventSubscriptionMock1.Setup(x => x.GetExecutionStrategy()).Returns(delegate { eventPublished1 = true; });
            eventSubscriptionMock2.Setup(x => x.GetExecutionStrategy()).Returns(delegate { eventPublished2 = true; });

            eventSubscriptionMock1.Setup(x => x.GetExecutionStrategy()).Returns(null);

            // Act
            eventBase.Subscribe(eventSubscriptionMock1.Object);
            eventBase.Subscribe(eventSubscriptionMock2.Object);
            eventBase.Publish();

            // Assert
            eventSubscriptionMock1.Verify(x => x, Times.Once());
            Assert.True(eventPublished1);

            eventSubscriptionMock2.Verify(x => x, Times.Once());
            Assert.True(eventPublished2);
        }

        [Fact]
        public void InternalPublish_NullReference_ShouldPruneStratagies()
        {
            // Prepare
            var eventBase = new EventFixture();
            var eventSubscriptionMock = new Mock<IEventSubscription>();
            eventSubscriptionMock.Setup(x => x.GetExecutionStrategy()).Returns(null);
            var token = eventBase.Subscribe(eventSubscriptionMock.Object);

            // Act
            eventBase.Publish();

            // Assert
            Assert.NotNull(token);
        }

        #endregion InternalPublish Tests

        #region Contains Tests

        [Fact]
        public void Contains_SearchByToken_ShouldFindSubscriber()
        {
            // Prepare
            var eventBase = new EventFixture();
            var eventSubscriptionMock = new Mock<IEventSubscription>();
            var token = eventBase.Subscribe(eventSubscriptionMock.Object);

            // Assert
            Assert.True(eventBase.Contains(token));
        }

        #endregion Contains Tests
    }
}
