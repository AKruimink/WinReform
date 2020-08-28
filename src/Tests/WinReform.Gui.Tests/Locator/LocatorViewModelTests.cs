using System;
using System.Collections.Generic;
using Moq;
using WinReform.Domain.Displays;
using WinReform.Domain.Infrastructure.Messanger;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.WinApi;
using WinReform.Domain.Windows;
using WinReform.Gui.Locator;
using Xunit;

namespace WinReform.Gui.Tests.Locator
{
    /// <summary>
    /// Defines a class that provides tests for <see cref="LocatorViewModel"/>
    /// </summary>
    public class LocatorViewModelTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_ValidConstruction_ShouldCreateUtilitiesViewModel()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            var windowServiceMock = new Mock<IWindowService>();
            var displayServiceMock = new Mock<IDisplayService>();

            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            // Act
            var viewModel = new LocatorViewModel(eventAggregatorMock.Object, windowServiceMock.Object, displayServiceMock.Object);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var windowServiceMock = new Mock<IWindowService>();
            var displayServiceMock = new Mock<IDisplayService>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new LocatorViewModel(null!, windowServiceMock.Object, displayServiceMock.Object);
            });
        }

        [Fact]
        public void Construct_NullWindowService_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            var displayServiceMock = new Mock<IDisplayService>();

            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new LocatorViewModel(eventAggregatorMock.Object, null!, displayServiceMock.Object);
            });
        }

        [Fact]
        public void Construct_NullDisplayService_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            var windowServiceMock = new Mock<IWindowService>();

            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new LocatorViewModel(eventAggregatorMock.Object, windowServiceMock.Object, null!);
            });
        }

        [Fact]
        public void Construct_ValidConstruction_ShouldGetAllDisplays()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            var windowServiceMock = new Mock<IWindowService>();
            var displayServiceMock = new Mock<IDisplayService>();

            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);
            displayServiceMock.Setup(x => x.GetAllDisplays()).Returns(new List<Display> { new Display { Id = 1, DisplayHandle = (IntPtr)1, Primary = false, WorkArea = new Rect() } });

            // Act
            var viewModel = new LocatorViewModel(eventAggregatorMock.Object, windowServiceMock.Object, displayServiceMock.Object);

            // Assert
            displayServiceMock.Verify(x => x.GetAllDisplays(), Times.Once);
            Assert.NotNull(viewModel.AvailableDisplays);
            Assert.True(viewModel.AvailableDisplays.Count == 1);
        }

        #endregion Constructor Tests

        #region ApplyCustomLocation Tests

        [Theory]
        [InlineData(10, 20)]
        [InlineData(null, 20)]
        [InlineData(10, null)]
        [InlineData(null, null)]
        public void ApplyCustomLocation_ShouldCreateRect(int? horizontalLocation, int? verticalLocation)
        {
            // Prepare
            Rect? createdRect = default;

            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();
            windowServiceMock.Setup(x => x.RelocateWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()))
                .Callback<Domain.Windows.Window, Rect>((window, rect) => createdRect = rect);

            var displayServiceMock = new Mock<IDisplayService>();

            var viewModel = new LocatorViewModel(eventAggregatorMock.Object, windowServiceMock.Object, displayServiceMock.Object)
            {
                NewHorizontalLocation = horizontalLocation.ToString() ?? string.Empty,
                NewVerticalLocation = verticalLocation.ToString() ?? string.Empty
            };
            viewModel.SelectedWindows.Add(new Domain.Windows.Window());

            // Act
            viewModel.ApplyCustomLocation();

            // Assert
            windowServiceMock.Verify(x => x.RelocateWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.AtLeastOnce());
            Assert.Equal(horizontalLocation ?? 0, createdRect?.Left);
            Assert.Equal(verticalLocation ?? 0, createdRect?.Top);
        }

        [Fact]
        public void ApplyCustomLocation_NoSelectedWindows_ShouldNotCallResizeWindow()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();
            var displayServiceMock = new Mock<IDisplayService>();

            var viewModel = new LocatorViewModel(eventAggregatorMock.Object, windowServiceMock.Object, displayServiceMock.Object);

            // Act
            viewModel.ApplyCustomLocation();

            // Assert
            windowServiceMock.Verify(x => x.RelocateWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.Never());
        }

        #endregion ApplyCustomLocation Tests

        #region ApplyPresetLocation Tests

        [Fact]
        public void ApplyPresetLocation_ValidLocation_ShouldCreateRect()
        {
            // Prepare
            var location = new Location(HorizontalLocationType.Left, VerticalLocationType.Top);
            Rect? sendRect = default;

            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();
            windowServiceMock.Setup(x => x.RelocateWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()))
                .Callback<Domain.Windows.Window, Rect>((window, rect) => sendRect = rect);

            var displayServiceMock = new Mock<IDisplayService>();

            var viewModel = new LocatorViewModel(eventAggregatorMock.Object, windowServiceMock.Object, displayServiceMock.Object)
            {
                SelectedDisplay = new Display { WorkArea = new Rect { Left = 0, Top = 0, Right = 500, Bottom = 500 } }
            };
            viewModel.SelectedWindows.Add(new Domain.Windows.Window());

            // Act
            viewModel.ApplyPresetLocation(location);

            // Assert
            windowServiceMock.Verify(x => x.RelocateWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.Once());
            Assert.Equal(new Rect(), sendRect);
        }

        [Fact]
        public void ApplyPresetLocation_NullLocation_ShouldCallRelocateWindow()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();
            var displayServiceMock = new Mock<IDisplayService>();

            var viewModel = new LocatorViewModel(eventAggregatorMock.Object, windowServiceMock.Object, displayServiceMock.Object);
            viewModel.SelectedWindows.Add(new Domain.Windows.Window());

            // Act
            viewModel.ApplyPresetLocation(null!);

            // Assert
            windowServiceMock.Verify(x => x.RelocateWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.Never());
        }

        [Fact]
        public void ApplyPresetLocation_NoSelectedWindows_ShouldNotCallRelocateWindow()
        {
            // Prepare
            var location = new Location(HorizontalLocationType.Left, VerticalLocationType.Center);

            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();
            var displayServiceMock = new Mock<IDisplayService>();

            var viewModel = new LocatorViewModel(eventAggregatorMock.Object, windowServiceMock.Object, displayServiceMock.Object)
            {
                SelectedDisplay = new Display { WorkArea = new Rect { Left = 0, Top = 0, Right = 500, Bottom = 500 } }
            };

            // Act
            viewModel.ApplyPresetLocation(location);

            // Assert
            windowServiceMock.Verify(x => x.RelocateWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.Never());
        }

        #endregion ApplyPresetLocation Tests
    }
}
