using System;
using Moq;
using WinReform.Domain.Infrastructure.Messanger;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Windows;
using WinReform.Utilities;
using Xunit;

namespace WinReform.Tests.Utilities
{
    /// <summary>
    /// Defines a class that provides tests for <see cref="UtilitiesViewModel"/>
    /// </summary>
    public class UtilitiesViewModelTests
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
            var viewModel = new UtilitiesViewModel(eventAggregatorMock.Object, windowServiceMock.Object);

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
                var viewModel = new UtilitiesViewModel(null!, windowServiceMock.Object);
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
                var viewModel = new UtilitiesViewModel(eventAggregatorMock.Object, null!);
            });
        }

        #endregion Constructor Tests
    }
}
