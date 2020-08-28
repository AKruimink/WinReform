using System;
using Moq;
using WinReform.Domain.Infrastructure.Messanger;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Settings;
using WinReform.Gui.Window;
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
        public void Construct_ValidConstruction_ShouldCreateWindowViewModel()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var applicationSettingMock = new Mock<ISetting<ApplicationSettings>>();

            eventAggregatorMock.Setup(x => x.GetEvent<SettingChangedEvent<ApplicationSettings>>()).Returns(new Mock<SettingChangedEvent<ApplicationSettings>>().Object);

            // Act
            var viewModel = new WindowViewModel(eventAggregatorMock.Object, applicationSettingMock.Object);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(x => x.GetEvent<SettingChangedEvent<ApplicationSettings>>()).Returns(new Mock<SettingChangedEvent<ApplicationSettings>>().Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(eventAggregatorMock.Object, null!);
            });
        }

        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var applicationSettingMock = new Mock<ISetting<ApplicationSettings>>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new WindowViewModel(null!, applicationSettingMock.Object);
            });
        }

        #endregion Constructor Tests
    }
}
