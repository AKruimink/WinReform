using System.Collections.Generic;
using System.Linq;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Infrastructure.Messenger.Strategies;
using Xunit;

namespace WinReform.Domain.Tests.Infrastructure.Messenger
{
    /// <summary>
    /// Tests for the <see cref="PubSubEvent"/>
    /// </summary>
    public class PubSubEventTests
    {
        #region Test Fixtures

        /// <summary>
        /// Defines a mock implementation of a non generic pub sub event
        /// </summary>
        private class PubSubEventFixture : PubSubEvent
        {
            public ICollection<IEventSubscription> CurrentSubscriptions => Subscriptions;
        }

        /// <summary>
        /// Defines a mock implementation of a generic pub sub event
        /// </summary>
        /// <typeparam name="TPayLoad">The type of argument that will be passed to the subscriber</typeparam>
        private class PubSubEventFixture<TPayLoad> : PubSubEvent<TPayLoad>
        {
            public ICollection<IEventSubscription> CurrentSubscriptions => Subscriptions;
        }

        #endregion Test Fixtures

        #region Subscribe Tests

        [Fact]
        public void SubscribeNonGeneric_DefaultThread_ShouldPublishOnSubscriberThread()
        {
            // Prepare
            static void ActionReference()
            {
                // Method intentionally left empty.
            }

            var pubsubEvent = new PubSubEventFixture();

            // Act
            _ = pubsubEvent.Subscribe(ActionReference);

            // Assert
            Assert.Single(pubsubEvent.CurrentSubscriptions);
            Assert.Equal(typeof(EventSubscription), pubsubEvent.CurrentSubscriptions.ElementAt(0).GetType());
        }

        [Fact]
        public void SubscribeGeneric_DefaultThread_ShouldPublishOnSubscriberThread()
        {
            // Prepare
            static void ActionReference(string arg)
            {
                // Method intentionally left empty.
            }

            var pubsubEvent = new PubSubEventFixture<string>();

            // Act
            _ = pubsubEvent.Subscribe(ActionReference);

            // Assert
            Assert.Single(pubsubEvent.CurrentSubscriptions);
            Assert.Equal(typeof(EventSubscription<string>), pubsubEvent.CurrentSubscriptions.ElementAt(0).GetType());
        }

        [Fact]
        public void Subscribe_DefaultFilter_ShouldReturnTrue()
        {
            // Prepare
            static void ActionReference(string arg)
            {
                // Method intentionally left empty.
            }

            var pubSubEvent = new PubSubEventFixture<string>();

            // Act
            pubSubEvent.Subscribe(ActionReference);
            var eventSubscription = pubSubEvent.CurrentSubscriptions.FirstOrDefault() as EventSubscription<string>;
            var filter = eventSubscription?.GetFilter;

            // Assert
            Assert.NotNull(filter);
            Assert.True(filter!(""));
        }

        #endregion Subscribe Tests

        #region Unsubscribe Tests

        [Fact]
        public void UnsubscribeNonGeneric_UnsubscribeByAction_ShouldUnsubscribe()
        {
            // Prepare
            static void ActionReference()
            {
                // Method intentionally left empty.
            }

            var pubSubEvent = new PubSubEventFixture();
            _ = pubSubEvent.Subscribe(ActionReference);

            // Assert
            Assert.True(pubSubEvent.Contains(ActionReference));

            // Act
            pubSubEvent.Unsubscribe(ActionReference);

            // Assert
            Assert.False(pubSubEvent.Contains(ActionReference));
        }

        [Fact]
        public void UnsubscribeGeneric_UnsubscribeByAction_ShouldUnsubscribe()
        {
            // Prepare
            static void ActionReference(string arg)
            {
                // Method intentionally left empty.
            }

            var pubSubEvent = new PubSubEventFixture<string>();
            _ = pubSubEvent.Subscribe(ActionReference);

            // Assert
            Assert.True(pubSubEvent.Contains(ActionReference));

            // Act
            pubSubEvent.Unsubscribe(ActionReference);

            // Assert
            Assert.False(pubSubEvent.Contains(ActionReference));
        }

        #endregion Unsubscribe Tests

        #region Contains Tests

        [Fact]
        public void ContainsNonGeneric_SearchByAction_ShouldFindSubscriber()
        {
            // Prepare
            static void ActionReference()
            {
                // Method intentionally left empty.
            }

            var pubSubEvent = new PubSubEventFixture();
            _ = pubSubEvent.Subscribe(ActionReference);

            // Assert
            Assert.True(pubSubEvent.Contains(ActionReference));
        }

        [Fact]
        public void ContainsGeneric_SearchByAction_ShouldFindSubscriber()
        {
            // Prepare
            static void ActionReference(string arg)
            {
                // Method intentionally left empty.
            }

            var pubSubEvent = new PubSubEventFixture<string>();
            _ = pubSubEvent.Subscribe(ActionReference);

            // Assert
            Assert.True(pubSubEvent.Contains(ActionReference));
        }

        #endregion Contains Tests
    }
}
