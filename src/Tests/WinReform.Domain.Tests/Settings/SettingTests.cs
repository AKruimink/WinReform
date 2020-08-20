using System;
using Moq;
using WinReform.Domain.Infrastructure.Events;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Settings;
using Xunit;

namespace WinReform.Domain.Tests.Settings
{
    /// <summary>
    /// Tests for the <see cref="Setting"/>
    /// </summary>
    public class SettingTests
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_Valid_ShouldCreateSetting()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            ISetting<ApplicationSettings> setting;

            // Act
            setting = new Setting<ApplicationSettings>(settingStoreMock.Object, eventAggregatorMock.Object);

            // Assert
            Assert.NotNull(setting);
        }

        [Fact]
        public void Constructor_NullSettingStore_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            ISetting<ApplicationSettings> setting;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                setting = new Setting<ApplicationSettings>(null!, eventAggregatorMock.Object);
            });
        }

        [Fact]
        public void Constructor_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            ISetting<ApplicationSettings> setting;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                setting = new Setting<ApplicationSettings>(settingStoreMock.Object, null!);
            });
        }

        [Fact]
        public void Constructor_LoadSettings_ShouldLoadSettingsOnConstruct()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            var eventAggregatorMock = new Mock<IEventAggregator>();

            // Act
            _ = new Setting<ApplicationSettings>(settingStoreMock.Object, eventAggregatorMock.Object);

            // Assert
            settingStoreMock.Verify(x => x.Load<ApplicationSettings>(), Times.Once());
        }

        #endregion Constructor Tests

        #region Save Tests

        [Fact]
        public void Save_Setting_ShouldSaveSettings()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            settingStoreMock.Setup(x => x.Load<ApplicationSettings>()).Returns(new ApplicationSettings());
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var setting = new Setting<ApplicationSettings>(settingStoreMock.Object, eventAggregatorMock.Object);

            // Act
            setting.CurrentSetting.UseDarkTheme = true;
            setting.Save();

            // Assert
            settingStoreMock.Verify(x => x.Save(It.IsAny<ApplicationSettings>()), Times.Once());
        }

        [Fact]
        public void Save_Settings_ShouldNotifySubscribers()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var settingsChangedEventMock = new Mock<SettingChangedEvent<ApplicationSettings>>();
            var setting = new Setting<ApplicationSettings>(settingStoreMock.Object, eventAggregatorMock.Object);

            eventAggregatorMock.Setup(x => x.GetEvent<SettingChangedEvent<ApplicationSettings>>()).Returns(settingsChangedEventMock.Object);

            // Act
            setting.Save();

            // Assert
            settingsChangedEventMock.Verify(x => x.Publish(It.IsAny<ISetting<ApplicationSettings>>()), Times.Once());
        }

        #endregion Save Tests

        #region Equal Tests

        [Fact]
        public void Equal_EqualSettings_ShouldReturnTrue()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            settingStoreMock.Setup(x => x.Load<ApplicationSettings>()).Returns(new ApplicationSettings());
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var setting = new Setting<ApplicationSettings>(settingStoreMock.Object, eventAggregatorMock.Object);

            // Assert
            Assert.True(setting.Equals(setting.CurrentSetting));
        }

        [Fact]
        public void Equal_UnequalSettings_ShouldReturnFalse()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var setting1 = new Setting<ApplicationSettings>(settingStoreMock.Object, eventAggregatorMock.Object);
            var setting2 = new Setting<ApplicationSettings>(settingStoreMock.Object, eventAggregatorMock.Object);

            // Assert
            Assert.False(setting1.Equals(setting2.CurrentSetting));
        }

        [Fact]
        public void Equal_NullSettings_ShouldReturnFalse()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var setting1 = new Setting<ApplicationSettings>(settingStoreMock.Object, eventAggregatorMock.Object);

            // Assert
            Assert.False(setting1.Equals(null!));
        }

        #endregion Equal Tests
    }
}
