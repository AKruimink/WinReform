using System;
using System.ComponentModel;
using Resizer.Gui.Helpers.EventManager;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.EventManager
{
    /// <summary>
    /// Defines all the <see cref="Delegate"/> tests for the <see cref="WeakEventManager"/>
    /// </summary>
    public class WeakEventManagerDelegateTests : WeakEventManagerTestsBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => WeakEventManager.AddEventHandler(value);
            remove => WeakEventManager.RemoveEventHandler(value);
        }

#pragma warning disable CS8604 // Possible null reference argument.

        #region AddEventHandler Tests

        [Fact]
        public void Delegate_AddEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            PropertyChangedEventHandler? nullevent = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(nullevent, nameof(PropertyChanged)));
        }

        [Fact]
        public void Delegate_AddEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new PropertyChangedEventHandler((sender, e) => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(handler, nullName));
        }

        [Fact]
        public void Delegate_AddEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new PropertyChangedEventHandler((sender, e) => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(handler, emptyName));
        }

        [Fact]
        public void EventHandler_AddEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new PropertyChangedEventHandler((sender, e) => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(handler, whitespaceName));
        }

        #endregion

        #region RemoveEventHandler Tests

        [Fact]
        public void Delegate_RemoveEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            PropertyChangedEventHandler? nullEvent = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(nullEvent, nameof(PropertyChanged)));
        }

        [Fact]
        public void Delegate_RemoveEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new PropertyChangedEventHandler((sender, e) => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(handler, nullName));
        }

        [Fact]
        public void Delegate_RemoveEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new PropertyChangedEventHandler((sender, e) => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(handler, emptyName));
        }

        [Fact]
        public void Delegate_RemoveEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var handler = new PropertyChangedEventHandler((sender, e) => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(handler, whitespaceName));
        }

        #endregion

        #region HandleEvent Tests

        [Fact]
        public void Delegate_HandleEvent_ValidImplementation_ShouldExecute()
        {
            //Arrange
            var eventFired = false;
            var eventArgs = new PropertyChangedEventArgs("Test");
            PropertyChanged += HandleAction;

            void HandleAction(object sender, PropertyChangedEventArgs e)
            {
                //Assert
                Assert.NotNull(sender);
                Assert.Equal(GetType(), sender.GetType());
                Assert.NotNull(e);

                //Act
                eventFired = true;

                //Arrange
                PropertyChanged -= HandleAction;
            }

            //Act
            WeakEventManager.HandleEvent(this, eventArgs, nameof(PropertyChanged));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void Delegate_HandleEvent_InvalidEventName_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            var eventArgs = new PropertyChangedEventArgs("Test");
            PropertyChanged += HandleEvent;

            void HandleEvent(object sender, PropertyChangedEventArgs e) => eventFired = true;

            //Act
            WeakEventManager.HandleEvent(this, eventArgs, nameof(InvalidEvent));

            //Assert
            Assert.False(eventFired);

            //Arrange
            PropertyChanged -= HandleEvent;
        }

        [Fact]
        public void Delegate_HandleEvent_UnAssignedEvent_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            PropertyChanged += HandleEvent;
            PropertyChanged -= HandleEvent;

            void HandleEvent(object sender, PropertyChangedEventArgs e) => eventFired = true;

            //Act
            WeakEventManager.HandleEvent(nameof(PropertyChanged));

            //Assert
            Assert.False(eventFired);
        }

        [Fact]
        public void Delegate_HandleEvent_UnAssignedWeakEventManager_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            var unassignedWeakEventManager = new WeakEventManager();
            PropertyChanged += HandleEvent;

            void HandleEvent(object sender, PropertyChangedEventArgs e) => eventFired = true;

            //Act
            unassignedWeakEventManager.HandleEvent(nameof(PropertyChanged));

            //Assert
            Assert.False(eventFired);

            //Arrange
            PropertyChanged -= HandleEvent;
        }

        [Fact]
        public void Delegate_HandleEvent_NullSender_ShouldFire()
        {
            //Arrange
            var eventFired = false;
            var eventArgs = new PropertyChangedEventArgs("Test");
            PropertyChanged += HandleEvent;

            void HandleEvent(object sender, PropertyChangedEventArgs e)
            {
                //Assert
                Assert.Null(sender);
                Assert.NotNull(e);

                //Act
                eventFired = true;

                //Arrange
                PropertyChanged -= HandleEvent;
            }

            //Act
            WeakEventManager.HandleEvent(null, eventArgs, nameof(PropertyChanged));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void Delegate_HandleEvent_NullEventArgs_ShouldFire()
        {
            //Arrange
            var eventFired = false;
            PropertyChangedEventArgs? nullEventArgs = null;
            PropertyChanged += HandleEvent;

            void HandleEvent(object sender, PropertyChangedEventArgs e)
            {
                //Assert
                Assert.NotNull(sender);
                Assert.Equal(GetType(), sender?.GetType());
                Assert.Null(e);

                //Act
                eventFired = true;

                //Arrange
                PropertyChanged -= HandleEvent;
            }

            //Act
            WeakEventManager.HandleEvent(this, nullEventArgs, nameof(PropertyChanged));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void Delegate_HandleEvent_InvalidHandleEvent_ThrowsInvalidHandleEventException()
        {
            //Arrange
            var eventFired = false;
            PropertyChanged += HandleEvent;

            void HandleEvent(object sender, PropertyChangedEventArgs e) => eventFired = true;

            //Act

            //Assert
            Assert.Throws<InvalidHandleEventException>(() => WeakEventManager.HandleEvent(nameof(PropertyChanged)));
            Assert.False(eventFired);

            //Arrange
            PropertyChanged -= HandleEvent;
        }

        #endregion

#pragma warning restore CS8604 // Possible null reference argument.
    }
}
