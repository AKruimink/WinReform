using WinReform.Gui.ActiveWindows;
using System;
using Xunit;
using Moq;
using WinReform.Domain.Windows;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Settings;
using WinReform.Domain.Infrastructure.Events;

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
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var windowServiceMock = new Mock<IWindowService>();
            var applicationSettingMock = new Mock<ISetting<ApplicationSettings>>();

            eventAggregatorMock.Setup(x => x.GetEvent<SettingChangedEvent<ApplicationSettings>>()).Returns(new Mock<SettingChangedEvent<ApplicationSettings>>().Object);

            // Act
            var viewModel = new ActiveWindowsViewModel(eventAggregatorMock.Object, windowServiceMock.Object, applicationSettingMock.Object);

            // Assert
            Assert.NotNull(viewModel);
        }


        [Fact]
        public void Construct_NullSettingFactory_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var windowServiceMock = new Mock<IWindowService>();

            eventAggregatorMock.Setup(x => x.GetEvent<SettingChangedEvent<ApplicationSettings>>()).Returns(new Mock<SettingChangedEvent<ApplicationSettings>>().Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(eventAggregatorMock.Object, windowServiceMock.Object, null!);
            });
        }


        [Fact]
        public void Construct_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var windowServiceMock = new Mock<IWindowService>();
            var applicationSettingMock = new Mock<ISetting<ApplicationSettings>>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(null!, windowServiceMock.Object, applicationSettingMock.Object);
            });
        }

        [Fact]
        public void Construct_NullWindowService_ShouldThrowArgumentNullException()
        {
            // Prepare 
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var applicationSettingMock = new Mock<ISetting<ApplicationSettings>>();

            eventAggregatorMock.Setup(x => x.GetEvent<SettingChangedEvent<ApplicationSettings>>()).Returns(new Mock<SettingChangedEvent<ApplicationSettings>>().Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(eventAggregatorMock.Object, null!, applicationSettingMock.Object);
            });
        }

        #endregion Constructor Tests

        #region RefreshActiveWindows tests

        [Fact]
        public void RefreshActiveWindows_Execution_ShouldCallWindowService()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var windowServiceMock = new Mock<IWindowService>();
            var applicationSettingMock = new Mock<ISetting<ApplicationSettings>>();

            eventAggregatorMock.Setup(x => x.GetEvent<SettingChangedEvent<ApplicationSettings>>()).Returns(new Mock<SettingChangedEvent<ApplicationSettings>>().Object);

            var viewmodel = new ActiveWindowsViewModel(eventAggregatorMock.Object, windowServiceMock.Object, applicationSettingMock.Object);

            // Act
            viewmodel.RefreshActiveWindows();

            // Assert
            windowServiceMock.Verify(x => x.GetActiveWindows(), Times.AtLeastOnce());
        }

        #endregion RefreshActiveWindows tests
    }
}
