using Resizer.Domain.Tests.Infrastructure.Messenger.Mocks;
using Resizer.Domain.Tests.Settings.Mocks;
using Resizer.Domain.Tests.Windows.Mocks;
using Resizer.Gui.ActiveWindows;
using System;
using Xunit;

namespace Resizer.Gui.Tests.ActiveWindows
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
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var windowService = new WindowServiceMock();

            // Act
            var viewModel = new ActiveWindowsViewModel(settingFactoryMock, eventAggregatorMock, windowService);

            // Assert
            Assert.NotNull(viewModel);
        }


        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var eventAggregatorMock = new EventAggregatorMock();
            var windowService = new WindowServiceMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(null!, eventAggregatorMock, windowService);
            });
        }


        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var settingFactoryMock = new SettingFactoryMock();
            var windowService = new WindowServiceMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(settingFactoryMock, null!, windowService);
            });
        }

        [Fact]
        public void Construct_NullWindowService_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(settingFactoryMock, eventAggregatorMock, null!);
            });
        }

        #endregion Constructor Tests

        #region RefreshActiveWindows tests

        [Fact]
        public void RefreshActiveWindows_Execution_ShouldCallWindowService()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var windowService = new WindowServiceMock();
            var viewmodel = new ActiveWindowsViewModel(settingFactoryMock, eventAggregatorMock, windowService);

            // Act
            windowService.GetActiveWindowsCalled = false;
            viewmodel.RefreshActiveWindows();

            // Assert
            Assert.True(windowService.GetActiveWindowsCalled);
        }

        #endregion RefreshActiveWindows tests
    }
}