using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Infrastructure.Messenger.Strategies;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using System;

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
            Action actionReference = () => { };
            var pubsubEvent = new PubSubEventFixture();

            // Act
            var token = pubsubEvent.Subscribe(actionReference);

            // Assert
            Assert.Equal(1, pubsubEvent.CurrentSubscriptions.Count);
            Assert.Equal(typeof(EventSubscription), pubsubEvent.CurrentSubscriptions.ElementAt(0).GetType());
        }

        [Fact]
        public void SubscribeGeneric_DefaultThread_ShouldPublishOnSubscriberThread()
        {
            // Prepare
            Action<string> actionReference = (arg) => { };
            var pubsubEvent = new PubSubEventFixture<string>();

            // Act
            var token = pubsubEvent.Subscribe(actionReference);

            // Assert
            Assert.Equal(1, pubsubEvent.CurrentSubscriptions.Count);
            Assert.Equal(typeof(EventSubscription<string>), pubsubEvent.CurrentSubscriptions.ElementAt(0).GetType());
        }

        [Fact]
        public void Subscribe_DefaultFilter_ShouldReturnTrue()
        {
            // Prepare
            Action<string> actionReference = (arg) => { };
            var pubSubEvent = new PubSubEventFixture<string>();

            // Act
            pubSubEvent.Subscribe(actionReference);
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
            Action actionReference = () => { };
            var pubSubEvent = new PubSubEventFixture();
            var token = pubSubEvent.Subscribe(actionReference);

            // Assert
            Assert.True(pubSubEvent.Contains(actionReference));

            // Act
            pubSubEvent.Unsubscribe(actionReference);

            // Assert
            Assert.False(pubSubEvent.Contains(actionReference));
        }

        [Fact]
        public void UnsubscribeGeneric_UnsubscribeByAction_ShouldUnsubscribe()
        {
            // Prepare
            Action<string> actionReference = (arg) => { };
            var pubSubEvent = new PubSubEventFixture<string>();
            var token = pubSubEvent.Subscribe(actionReference);

            // Assert
            Assert.True(pubSubEvent.Contains(actionReference));

            // Act
            pubSubEvent.Unsubscribe(actionReference);

            // Assert
            Assert.False(pubSubEvent.Contains(actionReference));
        }

        #endregion Unsubscribe Tests

        #region Publish Tests

        [Fact]
        public void PublishGeneric_NullArgument_ShouldThrowNullArgumentException()
        {
            // Prepare
        }

        #endregion Publish Tests

        #region Contains Tests

        [Fact]
        public void ContainsNonGeneric_SearchByAction_ShouldFindSubscriber()
        {
            // Prepare
            Action actionReference = () => { };
            var pubSubEvent = new PubSubEventFixture();
            var token = pubSubEvent.Subscribe(actionReference);

            // Assert
            Assert.True(pubSubEvent.Contains(actionReference));
        }

        [Fact]
        public void ContainsGeneric_SearchByAction_ShouldFindSubscriber()
        {
            // Prepare
            Action<string> actionReference = (arg) => { };
            var pubSubEvent = new PubSubEventFixture<string>();
            var token = pubSubEvent.Subscribe(actionReference);

            // Assert
            Assert.True(pubSubEvent.Contains(actionReference));
        }

        #endregion Contains Tests
    }
}
