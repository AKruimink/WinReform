using Resizer.Domain.Tests.Mocks;
using Resizer.Domain.Tests.Settings.Mocks;
using Resizer.Gui.Settings;
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
            var applicationSettings = new ApplicationSettingsViewModel(settingFactoryMock); // TODO: Probably wanna mock view model by implementing a interface for them

            // Act
            var viewModel = new WindowViewModel(settingFactoryMock, eventAggregatorMock, applicationSettings);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var applicationSettings = new ApplicationSettingsViewModel(settingFactoryMock);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(null!, eventAggregatorMock, applicationSettings);
            });
        }

        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var applicationSettings = new ApplicationSettingsViewModel(settingFactoryMock);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(settingFactoryMock, null!, applicationSettings);
            });
        }

        [Fact]
        public void Construct_NullApplicationSettings_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(settingFactoryMock, eventAggregatorMock, null!);
            });
        }

        #endregion
    }
}
