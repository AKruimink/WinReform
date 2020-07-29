using WinReform.Domain.Tests.Infrastructure.Messenger.Mocks;
using WinReform.Domain.Tests.Settings.Mocks;
using WinReform.Gui.Window;
using System;
using Xunit;

namespace WinReform.Gui.Tests.Window
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

            // Act
            var viewModel = new WindowViewModel(settingFactoryMock, eventAggregatorMock);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();
            var eventAggregatorMock = new EventAggregatorMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(null!, eventAggregatorMock);
            });
        }

        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new SettingFactoryMock();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(settingFactoryMock, null!);
            });
        }

        #endregion Constructor Tests
    }
}