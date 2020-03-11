using System;
using Resizer.Gui.Helpers.EventManager;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.EventManager
{
    /// <summary>
    /// Defines all the <see cref="Action"/> tests for the <see cref="WeakEventManager"/>
    /// </summary>
    public class WeakEventManagerActionTests : WeakEventManagerTestsBase
    {
        public event Action ActionEvent
        {
            add => WeakEventManager.AddEventHandler(value);
            remove => WeakEventManager.RemoveEventHandler(value);
        }

        public event Action<string> ActionEventType
        {
            add => WeakEventManagerType.AddEventHandler(value);
            remove => WeakEventManagerType.RemoveEventHandler(value);
        }

#pragma warning disable CS8604 // Possible null reference argument.

        #region AddEventHandler Tests

        [Fact]
        public void Action_AddEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            Action? nullAction = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(nullAction, nameof(ActionEvent)));
        }

        [Fact]
        public void ActionT_AddEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            Action<string>? nullAction = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.AddEventHandler(nullAction, nameof(ActionEventType)));
        }

        [Fact]
        public void Action_AddEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action(() => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(action, nullName));
        }

        [Fact]
        public void ActionT_AddEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action<string>((string message) => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.AddEventHandler(action, nullName));
        }

        [Fact]
        public void Action_AddEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action(() => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(action, emptyName));
        }

        [Fact]
        public void ActionT_AddEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action<string>((string message) => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.AddEventHandler(action, emptyName));
        }

        [Fact]
        public void Action_AddEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action(() => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.AddEventHandler(action, whitespaceName));
        }

        [Fact]
        public void ActionT_AddEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action<string>((string message) => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.AddEventHandler(action, whitespaceName));
        }

        #endregion

        #region RemoveEventHandler Tests

        [Fact]
        public void Action_RemoveEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            Action? nullAction = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(nullAction, nameof(ActionEvent)));
        }

        [Fact]
        public void ActionT_RemoveEventHandler_NullHandler_ThrowsArgumentNullException()
        {
            //Arrange
            Action<string>? nullAction = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.RemoveEventHandler(nullAction, nameof(ActionEventType)));
        }

        [Fact]
        public void Action_RemoveEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action(() => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(action, nullName));
        }

        [Fact]
        public void ActionT_RemoveEventHandler_NullEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action<string>((string message) => { });
            string? nullName = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.RemoveEventHandler(action, nullName));
        }

        [Fact]
        public void Action_RemoveEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action(() => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(action, emptyName));
        }

        [Fact]
        public void ActionT_RemoveEventHandler_EmptyEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action<string>((string message) => { });
            var emptyName = string.Empty;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.RemoveEventHandler(action, emptyName));
        }

        [Fact]
        public void Action_RemoveEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action(() => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManager.RemoveEventHandler(action, whitespaceName));
        }

        [Fact]
        public void ActionT_RemoveEventHandler_WhitespaceEventName_ThrowsArgumentNullException()
        {
            //Arrange
            var action = new Action<string>((string message) => { });
            var whitespaceName = "   ";

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => WeakEventManagerType.RemoveEventHandler(action, whitespaceName));
        }


        #endregion

        #region HandleEvent Tests

        [Fact]
        public void Action_HandleEvent_ValidImplementation_ShouldExecute()
        {
            //Arrange
            var eventFired = false;
            ActionEvent += HandleAction;

            void HandleAction()
            {
                eventFired = true;
                ActionEvent -= HandleAction;
            }

            //Act
            WeakEventManager.HandleEvent(nameof(ActionEvent));

            //Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void ActionT_HandleEvent_ValidImplementation_ShouldExecute()
        {
            //Arrange
            var eventFired = false;
            string? eventMessage = null;
            ActionEventType += HandleAction;

            void HandleAction(string message)
            {
                eventFired = true;
                eventMessage = message;
                ActionEventType -= HandleAction;
            }

            //Act
            WeakEventManagerType.HandleEvent("Test", nameof(ActionEventType));

            //Assert
            Assert.True(eventFired);
            Assert.NotNull(eventMessage);
            Assert.NotEmpty(eventMessage);
        }

        [Fact]
        public void Action_HandleEvent_InvalidEventName_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            ActionEvent += HandleAction;

            void HandleAction() => eventFired = true;

            //Act
            WeakEventManager.HandleEvent(nameof(TestEvent));

            //Assert
            Assert.False(eventFired);

            //Arrange
            ActionEvent -= HandleAction;

        }

        [Fact]
        public void ActionT_HandleEvent_InvalidEventName_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            ActionEventType += HandleAction;

            void HandleAction(string message) => eventFired = true;

            //Act
            WeakEventManagerType.HandleEvent("Test", nameof(TestEvent));

            //Assert
            Assert.False(eventFired);

            //Arrange
            ActionEventType -= HandleAction;
        }

        [Fact]
        public void Action_HandleEvent_UnAssignedEvent_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            ActionEvent += HandleAction;
            ActionEvent -= HandleAction;

            void HandleAction() => eventFired = true;

            //Act
            WeakEventManager.HandleEvent(nameof(ActionEvent));

            //Assert
            Assert.False(eventFired);
        }

        [Fact]
        public void ActionT_HandleEvent_UnAssignedEvent_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            ActionEventType += HandleAction;
            ActionEventType -= HandleAction;

            void HandleAction(string message) => eventFired = true;

            //Act
            WeakEventManagerType.HandleEvent("Test", nameof(ActionEvent));

            //Assert
            Assert.False(eventFired);
        }

        [Fact]
        public void Action_HandleEvent_UnAssignedWeakEventManager_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            var unassignedWeakEventManager = new WeakEventManager();
            ActionEvent += HandleAction;

            void HandleAction() => eventFired = true;

            //Act
            unassignedWeakEventManager.HandleEvent(nameof(ActionEvent));

            //Assert
            Assert.False(eventFired);

            //Arrange
            ActionEvent -= HandleAction;
        }

        [Fact]
        public void ActionT_HandleEvent_UnAssignedWeakEventManager_ShouldNotFire()
        {
            //Arrange
            var eventFired = false;
            var unassignedWeakEventManager = new WeakEventManager<string>();
            ActionEventType += HandleAction;

            void HandleAction(string message) => eventFired = true;

            //Act
            unassignedWeakEventManager.HandleEvent("Test", nameof(ActionEvent));

            //Assert
            Assert.False(eventFired);

            //Arrange
            ActionEventType -= HandleAction;
        }

        [Fact]
        public void Action_HandleEvent_InvalidHandleEvent_ThrowsInvalidHandleEventException()
        {
            //Arrange
            var eventFired = false;
            ActionEvent += HandleAction;

            void HandleAction() => eventFired = true;

            //Act

            //Assert
            Assert.Throws<InvalidHandleEventException>(() => WeakEventManager.HandleEvent(this, EventArgs.Empty, nameof(ActionEvent)));
            Assert.False(eventFired);

            //Arrange
            ActionEvent -= HandleAction;
        }

        [Fact]
        public void ActionT_HandleEvent_InvalidHandleEvent_ThrowsInvalidHandleEventException()
        {
            //Arrange
            var eventFired = false;
            ActionEventType += HandleAction;

            void HandleAction(string message) => eventFired = true;

            //Act

            //Assert
            Assert.Throws<InvalidHandleEventException>(() => WeakEventManagerType.HandleEvent(this, "Test", nameof(ActionEventType)));
            Assert.False(eventFired);

            //Arrange
            ActionEventType -= HandleAction;
        }

        #endregion

#pragma warning restore CS8604 // Possible null reference argument.
    }
}
