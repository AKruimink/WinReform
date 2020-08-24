using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using WinReform.Domain.Infrastructure.Events;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.WinApi;
using WinReform.Domain.Windows;
using WinReform.Gui.Resizer;
using Xunit;

namespace WinReform.Gui.Tests.Resizer
{
    /// <summary>
    /// Defines a class that provides tests for <see cref="ResizerViewModel"/>
    /// </summary>
    public class ResizerViewModelTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_ValidConstruction_ShouldCreateUtilitiesViewModel()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            var windowServiceMock = new Mock<IWindowService>();

            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            // Act
            var viewModel = new ResizerViewModel(eventAggregatorMock.Object, windowServiceMock.Object);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var windowServiceMock = new Mock<IWindowService>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ResizerViewModel(null!, windowServiceMock.Object);
            });
        }

        [Fact]
        public void Construct_NullWindowService_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();

            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ResizerViewModel(eventAggregatorMock.Object, null!);
            });
        }

        #endregion

        #region ApplyCustomResolution Tests

        [Theory]
        [InlineData(10, 20)]
        [InlineData(null, 20)]
        [InlineData(10, null)]
        [InlineData(null, null)]
        public void ApplyCustomResolution_ShouldCreateRect(int? width, int? height)
        {
            // Prepare
            Rect? createdRect = default;

            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();
            windowServiceMock.Setup(x => x.ResizeWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()))
                .Callback<Domain.Windows.Window, Rect>((window, rect) => createdRect = rect);

            var viewModel = new ResizerViewModel(eventAggregatorMock.Object, windowServiceMock.Object)
            {
                NewWidth = width.ToString() ?? string.Empty,
                NewHeight = height.ToString() ?? string.Empty
            };
            viewModel.SelectedWindows.Add(new Domain.Windows.Window());

            // Act
            viewModel.ApplyCustomResolution();

            // Assert
            windowServiceMock.Verify(x => x.ResizeWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.AtLeastOnce());
            Assert.Equal(width ?? 0, createdRect?.Right);
            Assert.Equal(height ?? 0, createdRect?.Bottom);
        }

        [Fact]
        public void ApplyCustomResolution_NoSelectedWindows_ShouldNotCallResizeWindow()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();

            var viewModel = new ResizerViewModel(eventAggregatorMock.Object, windowServiceMock.Object);

            // Act
            viewModel.ApplyCustomResolution();

            // Assert
            windowServiceMock.Verify(x => x.ResizeWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.Never());
        }

        #endregion

        #region ApplyPreset Tests

        [Fact]
        public void ApplyPreset_ValidRect_ShouldPassRect()
        {
            // Prepare
            var rect = new Rect { Right = 10, Bottom = 20};
            Rect? sendRect = default;

            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();
            windowServiceMock.Setup(x => x.ResizeWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()))
                .Callback<Domain.Windows.Window, Rect>((window, rect) => sendRect = rect);

            var viewModel = new ResizerViewModel(eventAggregatorMock.Object, windowServiceMock.Object);
            viewModel.SelectedWindows.Add(new Domain.Windows.Window());

            // Act
            viewModel.ApplyPreset(rect);

            // Assert
            windowServiceMock.Verify(x => x.ResizeWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.Once());
            Assert.Equal(rect, sendRect);
        }

        [Fact]
        public void ApplyPreset_NullRect_ShouldNotPassRect()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();

            var viewModel = new ResizerViewModel(eventAggregatorMock.Object, windowServiceMock.Object);
            viewModel.SelectedWindows.Add(new Domain.Windows.Window());

            // Act
            viewModel.ApplyPreset(null!);

            // Assert
            windowServiceMock.Verify(x => x.ResizeWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.Never());
        }

        [Fact]
        public void ApplyPreset_NoSelectedWindows_ShouldNotCallResizeWindow()
        {
            // Prepare
            var rect = new Rect { Right = 10, Bottom = 20 };

            var eventAggregatorMock = new Mock<IEventAggregator>();
            var activeWindowsSelectionChangedEventMock = new Mock<ActiveWindowsSelectionChangedEvent>();
            eventAggregatorMock.Setup(x => x.GetEvent<ActiveWindowsSelectionChangedEvent>()).Returns(activeWindowsSelectionChangedEventMock.Object);

            var windowServiceMock = new Mock<IWindowService>();

            var viewModel = new ResizerViewModel(eventAggregatorMock.Object, windowServiceMock.Object);

            // Act
            viewModel.ApplyPreset(rect);

            // Assert
            windowServiceMock.Verify(x => x.ResizeWindow(It.IsAny<Domain.Windows.Window>(), It.IsAny<Rect>()), Times.Never());
        }

        #endregion
    }
}
