using Resizer.Domain.Settings;
using Resizer.Domain.Tests.Mocks;
using Resizer.Domain.Tests.Settings.Mocks;
using System;
using Xunit;

namespace Resizer.Domain.Tests.Settings
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
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();
            ISetting<ApplicationSettings> setting;

            // Act
            setting = new Setting<ApplicationSettings>(settingStoreMock, eventAggregatorMock);

            // Assert
            Assert.NotNull(setting);
        }

        [Fact]
        public void Constructor_NullSettingStore_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new EventAggregatorMock();
            ISetting<ApplicationSettings> setting;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                setting = new Setting<ApplicationSettings>(null!, eventAggregatorMock);
            });
        }

        [Fact]
        public void Constructor_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            ISetting<ApplicationSettings> setting;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                setting = new Setting<ApplicationSettings>(settingStoreMock, null!);
            });
        }

        [Fact]
        public void Constructor_LoadSettings_ShouldLoadSettingsOnConstruct()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();

            // Assert
            Assert.False(settingStoreMock.Executed);

            // Act
            _ = new Setting<ApplicationSettings>(settingStoreMock, eventAggregatorMock);

            // Assert
            Assert.True(settingStoreMock.Executed);
        }

        #endregion

        #region Save Tests

        [Fact]
        public void Save_Setting_ShouldSaveSettings()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var setting = new Setting<ApplicationSettings>(settingStoreMock, eventAggregatorMock);
            settingStoreMock.Executed = false;

            // Assert
            Assert.False(settingStoreMock.Executed);

            // Act
            setting.CurrentSetting.UseDarkTheme = true;
            setting.Save();

            // Assert
            Assert.True(settingStoreMock.Executed);
        }

        [Fact]
        public void Save_Settings_ShouldNotifySubscribers()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var setting = new Setting<ApplicationSettings>(settingStoreMock, eventAggregatorMock);

            // Assert
            Assert.False(eventAggregatorMock.Executed);

            // Act
            setting.Save();

            // Assert
            Assert.True(eventAggregatorMock.Executed);
        }

        #endregion

        #region Equal Tests


        [Fact]
        public void Equal_EqualSettings_ShouldReturnTrue()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var setting = new Setting<ApplicationSettings>(settingStoreMock, eventAggregatorMock);

            // Assert
            Assert.True(setting.Equals(setting.CurrentSetting));
        }

        [Fact]
        public void Equal_UnequalSettings_ShouldReturnFalse()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var setting1 = new Setting<ApplicationSettings>(settingStoreMock, eventAggregatorMock);
            var setting2 = new Setting<ApplicationSettings>(settingStoreMock, eventAggregatorMock);

            // Assert
            Assert.False(setting1.Equals(setting2.CurrentSetting));
        }

        [Fact]
        public void Equal_NullSettings_ShouldReturnFalse()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();
            var setting1 = new Setting<ApplicationSettings>(settingStoreMock, eventAggregatorMock);

            // Assert
            Assert.False(setting1.Equals(null!));
        }

        #endregion
    }
}
