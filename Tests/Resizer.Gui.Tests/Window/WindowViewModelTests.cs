using Resizer.Domain.Tests.Mocks;
using Resizer.Domain.Tests.Settings.Mocks;
using Resizer.Gui.Settings;
using Resizer.Gui.Tests.Mocks;
using Resizer.Gui.Window;
using System;
using Xunit;

namespace Resizer.Gui.Tests.Window
{
    /// <summary>
    /// Tests for the <see cref="WindowViewModel"/>
    /// </summary>
    public class WindowViewModelTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_ValidConstruction_ShouldCreateViewModel()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var applicationSettings = new ApplicationSettingsViewModelMock();
            var activeWindows = new ActiveWindowsViewModelMock();

            // Act
            var viewModel = new WindowViewModel(settingFactoryMock, eventAggregatorMock, applicationSettings, activeWindows);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var applicationSettings = new ApplicationSettingsViewModelMock();
            var activeWindows = new ActiveWindowsViewModelMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(null!, eventAggregatorMock, applicationSettings, activeWindows);
            });
        }

        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var applicationSettings = new ApplicationSettingsViewModelMock();
            var activeWindows = new ActiveWindowsViewModelMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(settingFactoryMock, null!, applicationSettings, activeWindows);
            });
        }

        [Fact]
        public void Construct_NullApplicationSettings_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var activeWindows = new ActiveWindowsViewModelMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(settingFactoryMock, eventAggregatorMock, null!, activeWindows);
            });
        }

        [Fact]
        public void Construct_NullActiveWindows_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var applicationSettings = new ApplicationSettingsViewModelMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(settingFactoryMock, eventAggregatorMock, applicationSettings, null!);
            });
        }

        #endregion Constructor Tests
    }
}