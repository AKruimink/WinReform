using Resizer.Gui.Common.Messenger;
using Resizer.Gui.Tests.Common.Messenger.Mocks;
using Xunit;

namespace Resizer.Gui.Tests.Common.Messenger
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
            var eventBase = new EventBaseMock();
            var eventSubscription = new EventSubscriptionMock();

            // Act
            eventBase.Subscribe(eventSubscription);

            // Assert
            Assert.NotNull(eventSubscription.SubscriptionToken);
            Assert.True(eventBase.Contains(eventSubscription.SubscriptionToken!));
        }

        #endregion

        #region InternalUnsubscribe Tests

        [Fact]
        public void InternalUnsubscribe_UnsubscribeEvent_ShouldUnubscribeFromEvent()
        {
            // Prepare
            var eventBase = new EventBaseMock();
            var eventSubscription = new EventSubscriptionMock();
            eventBase.Subscribe(eventSubscription);

            // Assert
            Assert.NotNull(eventSubscription.SubscriptionToken);
            Assert.True(eventBase.Contains(eventSubscription.SubscriptionToken!));

            // Act
            eventBase.Unsubscribe(eventSubscription.SubscriptionToken!);

            // Assert
            Assert.False(eventBase.Contains(eventSubscription.SubscriptionToken!));
        }

        #endregion

        #region InternalPublish Tests

        [Fact]
        public void InternalPublish_PublishEvent_ShouldNotifySubscription()
        {
            // Prepare
            var eventPublished = false;
            var eventBase = new EventBaseMock();
            var eventSubscription = new EventSubscriptionMock
            {
                PublishActionReturnValue = delegate { eventPublished = true; }
            };


            // Act
            eventBase.Subscribe(eventSubscription);
            eventBase.Publish();

            // Assert
            Assert.True(eventSubscription.PublishActionCalled);
            Assert.True(eventPublished);
        }

        [Fact]
        public void InternalPublish_PublishEvent_ShouldNotifyMultipleSubscription()
        {
            // Prepare
            var firstEventPublished = false;
            var secondEventPublished = false;
            var eventBase = new EventBaseMock();
            var firstEventSubscription = new EventSubscriptionMock
            {
                PublishActionReturnValue = delegate { firstEventPublished = true; }
            };

            var secondEventSubscription = new EventSubscriptionMock
            {
                PublishActionReturnValue = delegate { secondEventPublished = true; }
            };


            // Act
            eventBase.Subscribe(firstEventSubscription);
            eventBase.Subscribe(secondEventSubscription);
            eventBase.Publish();

            // Assert
            Assert.True(firstEventSubscription.PublishActionCalled);
            Assert.True(firstEventPublished);

            Assert.True(secondEventSubscription.PublishActionCalled);
            Assert.True(secondEventPublished);
        }

        [Fact]
        public void InternalPublish_NullReference_ShouldPruneStratagies()
        {
            // Prepare
            var eventBase = new EventBaseMock();
            var eventSubscription = new EventSubscriptionMock();
            eventSubscription.PublishActionReturnValue = null;
            var token = eventBase.Subscribe(eventSubscription);

            // Act
            eventBase.Publish();

            // Assert
            Assert.False(eventBase.Contains(token));
        }

        #endregion


        #region Contains Tests

        [Fact]
        public void Contains_SearchByToken_ShouldFindSubscriber()
        {
            // Prepare
            var eventbase = new EventBaseMock();
            var eventSubscription = new EventSubscriptionMock();
            var token = eventbase.Subscribe(eventSubscription);

            // Assert
            Assert.True(eventbase.Contains(token));
        }

        #endregion
    }
}
