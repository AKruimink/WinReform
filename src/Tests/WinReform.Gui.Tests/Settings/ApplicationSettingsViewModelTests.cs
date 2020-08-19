using WinReform.Gui.Settings;
using System;
using Xunit;
using Moq;
using WinReform.Domain.Settings;

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
            var applicationSettingMock = new Mock<ISetting<ApplicationSettings>>();
            applicationSettingMock.Setup(x => x.CurrentSetting).Returns(new Mock<ApplicationSettings>().Object);

            // Act
            var viewModel = new ApplicationSettingsViewModel(applicationSettingMock.Object);

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
