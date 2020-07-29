using WinReform.Domain.Settings;
using WinReform.Domain.Tests.Infrastructure.Messenger.Mocks;
using WinReform.Domain.Tests.Settings.Mocks;
using System;
using Xunit;

namespace WinReform.Domain.Tests.Settings
{
    /// <summary>
    /// Tests for the <see cref="SettingFactory"/>
    /// </summary>
    public class SettingFactoryTests
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_Valid_ShouldCreateSettingFactory()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();
            ISettingFactory settingFactory;

            // Act
            settingFactory = new SettingFactory(settingStoreMock, eventAggregatorMock);

            // Assert
            Assert.NotNull(settingFactory);
        }

        [Fact]
        public void Constructor_NullSettingStore_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new EventAggregatorMock();
            ISettingFactory settingFactory;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                settingFactory = new SettingFactory(null!, eventAggregatorMock);
            });
        }

        [Fact]
        public void Constructor_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            var settingStoreMock = new SettingStoreMock();
            ISettingFactory settingFactory;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                settingFactory = new SettingFactory(settingStoreMock, null!);
            });
        }

        #endregion Constructor Tests

        #region Create Tests

        [Fact]
        public void Create_GetNewAndExistingEvent_ShouldReturnTheSameSettings()
        {
            // Prepare
            var settingStoreMock = new SettingStoreMock();
            var eventAggregatorMock = new EventAggregatorMock();
            ISettingFactory settingFactory = new SettingFactory(settingStoreMock, eventAggregatorMock);

            // Act
            var setting1 = settingFactory.Create<ApplicationSettings>();
            var setting2 = settingFactory.Create<ApplicationSettings>();

            // Assert
            Assert.Equal(setting1, setting2);
        }

        #endregion Create Tests
    }
}