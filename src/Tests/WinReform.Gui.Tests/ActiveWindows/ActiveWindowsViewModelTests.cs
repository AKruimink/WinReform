using WinReform.Gui.ActiveWindows;
using System;
using Xunit;
using Moq;
using WinReform.Domain.Windows;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Settings;

namespace WinReform.Gui.Tests.ActiveWindows
{
    /// <summary>
    /// Tests for the <see cref="ActiveWindowsViewModel"/>
    /// </summary>
    public class ActiveWindowsViewModelTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_ValidArguments_ShouldConstructActiveWindowsViewModel()
        {
            // Prepare
            var settingFactoryMock = new Mock<ISettingFactory>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var windowServiceMock = new Mock<IWindowService>();

            // Act
            var viewModel = new ActiveWindowsViewModel(settingFactoryMock.Object, eventAggregatorMock.Object, windowServiceMock.Object);

            // Assert
            Assert.NotNull(viewModel);
        }


        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var windowServiceMock = new Mock<IWindowService>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(null!, eventAggregatorMock.Object, windowServiceMock.Object);
            });
        }


        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var settingFactoryMock = new Mock<ISettingFactory>();
            var windowServiceMock = new Mock<IWindowService>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(settingFactoryMock.Object, null!, windowServiceMock.Object);
            });
        }

        [Fact]
        public void Construct_NullWindowService_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var settingFactoryMock = new Mock<ISettingFactory>();
            var eventAggregatorMock = new Mock<IEventAggregator>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(settingFactoryMock.Object, eventAggregatorMock.Object, null!);
            });
        }

        #endregion Constructor Tests

        #region RefreshActiveWindows tests

        [Fact]
        public void RefreshActiveWindows_Execution_ShouldCallWindowService()
        {
            // Prepare
            var settingFactoryMock = new Mock<ISettingFactory>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var windowServiceMock = new Mock<IWindowService>();
            var viewmodel = new ActiveWindowsViewModel(settingFactoryMock.Object, eventAggregatorMock.Object, windowServiceMock.Object);

            // Act
            viewmodel.RefreshActiveWindows();

            // Assert
            windowServiceMock.Verify(x => x.GetActiveWindows(), Times.AtLeastOnce());
        }

        #endregion RefreshActiveWindows tests
    }
}
