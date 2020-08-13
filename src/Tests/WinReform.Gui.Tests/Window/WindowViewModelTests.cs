using WinReform.Gui.Window;
using System;
using Xunit;
using WinReform.Domain.Settings;
using Moq;
using WinReform.Domain.Infrastructure.Messenger;

namespace WinReform.Gui.Tests.Window
{
    /// <summary>
    /// Tests for the <see cref="WindowViewModel"/>
    /// </summary>
    public class WindowViewModelTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_ValidConstruction_ShouldCreateWindowViewModel()
        {
            // Prepare
            var settingFactoryMock = new Mock<ISettingFactory>();
            var eventAggregatorMock = new Mock<IEventAggregator>();

            // Act
            var viewModel = new WindowViewModel(settingFactoryMock.Object, eventAggregatorMock.Object);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(null!, eventAggregatorMock.Object);
            });
        }

        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingFactoryMock = new Mock<ISettingFactory>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(settingFactoryMock.Object, null!);
            });
        }

        #endregion Constructor Tests
    }
}
