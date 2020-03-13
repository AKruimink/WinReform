using System;
using Resizer.Gui.Helpers.EventManager;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.EventManager
{
    /// <summary>
    /// Defines all the <see cref="EventHandler"/> tests for the <see cref="WeakEventManager"/>
    /// </summary>
    public class WeakEventManagerEventHandlerTests : WeakEventManagerTestsBase
    {
        public event EventHandler Event
        {
            add => WeakEventManager.AddEventHandler(value);
            remove => WeakEventManager.RemoveEventHandler(value);
        }

        public event EventHandler<string> StringEvent
        {
            add => WeakEventManagerType.AddEventHandler(value);
            remove => WeakEventManagerType.RemoveEventHandler(value);
        }

#pragma warning disable CS8604 // Possible null reference argument.

        #region AddEventHandler Tests

        [Fact]
        public void EventHandler_AddEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            EventHandler? nullevent = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(nullevent, nameof(Event)));
        }

        [Fact]
        public void EventHandlerT_AddEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            EventHandler<string>? nullevent = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.AddEventHandler(nullevent, nameof(StringEvent)));
        }

        [Fact]
        public void EventHandler_AddEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler((sender, e) => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(handler, nullName));
        }

        [Fact]
        public void EventHandlerT_AddEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler<string>((sender, e) => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.AddEventHandler(handler, nullName));
        }

        [Fact]
        public void EventHandler_AddEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler((sender, e) => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(handler, emptyName));
        }

        [Fact]
        public void EventHandlerT_AddEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler<string>((sender, e) => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.AddEventHandler(handler, emptyName));
        }

        [Fact]
        public void EventHandler_AddEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler((sender, e) => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(handler, whitespaceName));
        }

        [Fact]
        public void EventHandlerT_AddEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler<string>((sender, e) => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.AddEventHandler(handler, whitespaceName));
        }

        #endregion

        #region RemoveEventHandler Tests

        [Fact]
        public void EventHandler_RemoveEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            EventHandler? nullEvent = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(nullEvent, nameof(Event)));
        }

        [Fact]
        public void EventHandlerT_RemoveEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            EventHandler<string>? nullEvent = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.RemoveEventHandler(nullEvent, nameof(StringEvent)));
        }

        [Fact]
        public void EventHandler_RemoveEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler((sender, e) => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(handler, nullName));
        }

        [Fact]
        public void EventHandlerT_RemoveEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler<string>((sender, e) => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.RemoveEventHandler(handler, nullName));
        }

        [Fact]
        public void EventHandler_RemoveEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler((sender, e) => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(handler, emptyName));
        }

        [Fact]
        public void EventHandlerT_RemoveEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler<string>((sender, e) => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.RemoveEventHandler(handler, emptyName));
        }

        [Fact]
        public void EventHandler_RemoveEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler((sender, e) => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(handler, whitespaceName));
        }

        [Fact]
        public void EventHandlerT_RemoveEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new EventHandler<string>((sender, e) => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.RemoveEventHandler(handler, whitespaceName));
        }

        #endregion

        #region HandleEvent Tests

        [Fact]
        public void EventHandler_HandleEvent_ValidImplementation_ShouldExecute()
        {
            //Arrange
            var eventFired = false;
            Event += HandleEvent;

            void HandleEvent(object? sender, EventArgs e)
            {
                //Assert
                Assert.NotNull(sender);
                Assert.Equal(GetType(), sender?.GetType());
                Assert.NotNull(e);

                //Act
                eventFired = true;

                //Arrange
                Event -= HandleEvent;
            }

            //Act
            WeakEventManager.HandleEvent(this, new EventArgs(), nameof(Event));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void EventHandlerT_HandleEvent_ValidImplementation_ShouldExecute()
        {
            //Arrange
            var eventFired = false;
            const string EventArgs = "Test";
            StringEvent += HandleEvent;

            void HandleEvent(object? sender, string? e)
            {
                //Assert
                Assert.NotNull(sender);
                Assert.Equal(GetType(), sender?.GetType());
                Assert.NotNull(e);
                Assert.Equal(EventArgs, e);

                //Act
                eventFired = true;

                //Arrange
                StringEvent -= HandleEvent;
            }

            //Act
            WeakEventManagerType.HandleEvent(this, EventArgs, nameof(StringEvent));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void EventHandler_HandleEvent_InvalidEventName_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            Event += HandleEvent;

            void HandleEvent(object? sender, EventArgs e) => eventFired = true;

            //Act
            WeakEventManager.HandleEvent(this, new EventArgs(), nameof(InvalidEvent));

            //Assert
            Assert.False(eventFired);

            //Arrange
            Event -= HandleEvent;
        }

        [Fact]
        public void EventHandlerT_HandleEvent_InvalidEventName_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            const string EventArgs = "Test";
            StringEvent += HandleEvent;

            void HandleEvent(object? sender, string? e) => eventFired = true;

            //Act
            WeakEventManagerType.HandleEvent(this, EventArgs, nameof(InvalidEvent));

            //Assert
            Assert.False(eventFired);

            //Arrange
            StringEvent -= HandleEvent;
        }

        [Fact]
        public void EventHandler_HandleEvent_UnAssignedEvent_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            Event += HandleEvent;
            Event -= HandleEvent;

            void HandleEvent(object? sender, EventArgs e) => eventFired = true;

            //Act
            WeakEventManager.HandleEvent(nameof(Event));

            //Assert
            Assert.False(eventFired);
        }

        [Fact]
        public void EventHandlerT_HandleEvent_UnAssignedEvent_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            const string EventArgs = "Test";
            StringEvent += HandleEvent;
            StringEvent -= HandleEvent;

            void HandleEvent(object? sender, string? e) => eventFired = true;

            //Act
            WeakEventManagerType.HandleEvent(EventArgs, nameof(StringEvent));

            //Assert
            Assert.False(eventFired);
        }

        [Fact]
        public void EventHandler_HandleEvent_UnAssignedWeakEventManager_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            var unassignedWeakEventManager = new WeakEventManager();
            Event += HandleEvent;

            void HandleEvent(object? sender, EventArgs e) => eventFired = true;

            //Act
            unassignedWeakEventManager.HandleEvent(nameof(Event));

            //Assert
            Assert.False(eventFired);

            //Arrange
            Event -= HandleEvent;
        }

        [Fact]
        public void EventHandlerT_HandleEvent_UnAssignedWeakEventManager_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            const string EventArgs = "Test";
            var unassignedWeakEventManager = new WeakEventManager<string>();
            StringEvent += HandleEvent;

            void HandleEvent(object? sender, string? e) => eventFired = true;

            //Act
            unassignedWeakEventManager.HandleEvent(EventArgs, nameof(StringEvent));

            //Assert
            Assert.False(eventFired);

            //Arrange
            StringEvent -= HandleEvent;
        }

        [Fact]
        public void EventHandler_HandleEvent_NullSender_ShouldFire()
        {
            //Arrange
            var eventFired = false;
            Event += HandleEvent;

            void HandleEvent(object? sender, EventArgs e)
            {
                //Assert
                Assert.Null(sender);
                Assert.NotNull(e);

                //Act
                eventFired = true;

                //Arrange
                Event -= HandleEvent;
            }

            //Act
            WeakEventManager.HandleEvent(null, new EventArgs(), nameof(Event));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void EventHandlerT_HandleEvent_NullSender_ShouldFire()
        {
            //Arrange
            var eventFired = false;
            const string EventArgs = "Test";
            StringEvent += HandleEvent;

            void HandleEvent(object? sender, string? e)
            {
                //Assert
                Assert.Null(sender);
                Assert.NotNull(e);
                Assert.Equal(EventArgs, e);

                //Act
                eventFired = true;

                //Arrange
                StringEvent -= HandleEvent;
            }

            //Act
            WeakEventManagerType.HandleEvent(null, EventArgs, nameof(StringEvent));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void EventHandler_HandleEvent_NullEventArgs_ShouldFire()
        {
            //Arrange
            var eventFired = false;
            EventArgs? nullEventArgs = null;
            Event += HandleEvent;

            void HandleEvent(object? sender, EventArgs e)
            {
                //Assert
                Assert.NotNull(sender);
                Assert.Equal(GetType(), sender?.GetType());
                Assert.Null(e);

                //Act
                eventFired = true;

                //Arrange
                Event -= HandleEvent;
            }

            //Act
            WeakEventManager.HandleEvent(this, nullEventArgs, nameof(Event));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void EventHandlerT_HandleEvent_NullEventArgs_ShouldFire()
        {
            //Arrange
            var eventFired = false;
            string? nullEventArgs = null;
            StringEvent += HandleEvent;

            void HandleEvent(object? sender, string? e)
            {
                //Assert
                Assert.NotNull(sender);
                Assert.Equal(GetType(), sender?.GetType());
                Assert.Null(e);

                //Act
                eventFired = true;

                //Arrange
                StringEvent -= HandleEvent;
            }

            //Act
            WeakEventManagerType.HandleEvent(this, nullEventArgs, nameof(StringEvent));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void EventHandler_HandleEvent_InvalidHandleEvent_ThrowsInvalidHandleEventException()
        {
            //Arrange
            var eventFired = false;
            Event += HandleEvent;

            void HandleEvent(object? sender, EventArgs e) => eventFired = true;

            //Act

            //Assert
            Assert.Throws<InvalidHandleEventException>(() => WeakEventManager.HandleEvent(nameof(Event)));
            Assert.False(eventFired);

            //Arrange
            Event -= HandleEvent;
        }

        [Fact]
        public void EventHandlerT_HandleEvent_InvalidHandleEvent_ThrowsInvalidHandleEventException()
        {
            //Arrange
            var eventFired = false;
            const string EventArgs = "Test";
            StringEvent += HandleEvent;

            void HandleEvent(object? sender, string? e) => eventFired = true;

            //Act

            //Assert
            Assert.Throws<InvalidHandleEventException>(() => WeakEventManagerType.HandleEvent(EventArgs, nameof(StringEvent)));
            Assert.False(eventFired);

            //Arrange
            StringEvent -= HandleEvent;
        }

        // Line 68

        #endregion

#pragma warning restore CS8604 // Possible null reference argument.
    }
}
