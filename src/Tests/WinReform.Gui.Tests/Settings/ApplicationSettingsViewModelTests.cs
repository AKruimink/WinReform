using WinReform.Domain.Tests.Settings.Mocks;
using WinReform.Gui.Settings;
using System;
using Xunit;

namespace WinReform.Gui.Tests.Settings
{
    /// <summary>
    /// Tests for the <see cref="ApplicationSettingsViewModel"/>
    /// </summary>
    public class ApplicationSettingsViewModelTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_ValidConstruction_ShouldCreateViewModel()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();

            // Act
            var viewModel = new ApplicationSettingsViewModel(settingFactoryMock);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ApplicationSettingsViewModel(null!);
            });
        }

        #endregion Constructor Tests
    }
}