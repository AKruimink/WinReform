using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Resizer.Gui.Common.Messenger;
using Resizer.Gui.Tests.Common.Messenger.Mocks;
using Xunit;

namespace Resizer.Gui.Tests.Common.Messenger
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
        private class PubSubEventMock : PubSubEvent
        {
            /// <summary>
            /// Gets all the current subscriptions
            /// </summary>
            public ICollection<IEventSubscription> CurrentSubscriptions => Subscriptions;
        }

        /// <summary>
        /// Defines a mock implementation of a generic pub sub event
        /// </summary>
        /// <typeparam name="TPayLoad">The type of argument that will be passed to the subscriber</typeparam>
        private class PubSubEventMock<TPayLoad> : PubSubEvent<TPayLoad>
        {
            /// <summary>
            /// Gets all the current subscriptions
            /// </summary>
            public ICollection<IEventSubscription> CurrentSubscriptions => Subscriptions;
        }

        #endregion

        #region Subscribe Tests

        [Fact]
        public void SubscribeNonGeneric_DefaultThread_ShouldPublishOnSubscriberThread()
        {
            // Prepare
            static void Action() { }
            var pubsubEvent = new PubSubEventMock();

            // Act
            var token = pubsubEvent.Subscribe(Action);

            // Assert
            Assert.Equal(1, pubsubEvent.CurrentSubscriptions.Count);
            Assert.Equal(typeof(EventSubscription), pubsubEvent.CurrentSubscriptions.ElementAt(0).GetType());
        }

        [Fact]
        public void SubscribeGeneric_DefaultThread_ShouldPublishOnSubscriberThread()
        {
            // Prepare
            static void Action(string args) { }
            var pubsubEvent = new PubSubEventMock<string>();

            // Act
            var token = pubsubEvent.Subscribe(Action);

            // Assert
            Assert.Equal(1, pubsubEvent.CurrentSubscriptions.Count);
            Assert.Equal(typeof(EventSubscription<string>), pubsubEvent.CurrentSubscriptions.ElementAt(0).GetType());
        }

        [Fact]
        public void Subscribe_DefaultFilter_ShouldReturnTrue()
        {
            // Prepare
            var pubSubEvent = new PubSubEventMock<string>();
            var action = new ActionMock();

            // Act
            pubSubEvent.Subscribe(action.Action);
            var eventSubscription = pubSubEvent.CurrentSubscriptions.FirstOrDefault() as EventSubscription<string>;
            var filter = eventSubscription?.GetFilter;

            // Assert
            Assert.NotNull(filter);
            Assert.True(filter!(""));

        }

        #endregion

        #region Unsubscribe Tests

        [Fact]
        public void UnsubscribeNonGeneric_UnsubscribeByAction_ShouldUnsubscribe()
        {
            // Prepare
            var pubSubEvent = new PubSubEventMock();
            var action = new ActionMock();
            var token = pubSubEvent.Subscribe(action.Action);

            // Assert
            Assert.True(pubSubEvent.Contains(action.Action));

            // Act
            pubSubEvent.Unsubscribe(action.Action);

            // Assert
            Assert.False(pubSubEvent.Contains(action.Action));
        }

        [Fact]
        public void UnsubscribeGeneric_UnsubscribeByAction_ShouldUnsubscribe()
        {
            // Prepare
            var pubSubEvent = new PubSubEventMock<string>();
            var action = new ActionMock();
            var token = pubSubEvent.Subscribe(action.Action);

            // Assert
            Assert.True(pubSubEvent.Contains(action.Action));

            // Act
            pubSubEvent.Unsubscribe(action.Action);

            // Assert
            Assert.False(pubSubEvent.Contains(action.Action));
        }

        #endregion

        #region Publish Tests

        [Fact]
        public void PublishGeneric_NullArgument_ShouldThrowNullArgumentException()
        {
            // Prepare
        }

        #endregion

        #region Contains Tests

        [Fact]
        public void ContainsNonGeneric_SearchByAction_ShouldFindSubscriber()
        {
            // Prepare
            var pubSubEvent = new PubSubEventMock();
            var action = new ActionMock();
            var token = pubSubEvent.Subscribe(action.Action);

            // Assert
            Assert.True(pubSubEvent.Contains(action.Action));
        }

        [Fact]
        public void ContainsGeneric_SearchByAction_ShouldFindSubscriber()
        {
            // Prepare
            var pubSubEvent = new PubSubEventMock<string>();
            var action = new ActionMock();
            var token = pubSubEvent.Subscribe(action.Action);

            // Assert
            Assert.True(pubSubEvent.Contains(action.Action));
        }

        #endregion
    }
}
