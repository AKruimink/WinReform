using WinReform.Domain.Settings;
using System;
using Xunit;
using Moq;
using WinReform.Domain.Infrastructure.Messenger;

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
            var settingStoreMock = new Mock<ISettingStore>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            ISettingFactory settingFactory;

            // Act
            settingFactory = new SettingFactory(settingStoreMock.Object, eventAggregatorMock.Object);

            // Assert
            Assert.NotNull(settingFactory);
        }

        [Fact]
        public void Constructor_NullSettingStore_ShouldThrowArgumentNullException()
        {
            // Prepare
            var eventAggregatorMock = new Mock<IEventAggregator>();
            ISettingFactory settingFactory;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                settingFactory = new SettingFactory(null!, eventAggregatorMock.Object);
            });
        }

        [Fact]
        public void Constructor_NullEventAggregator_ShouldThrowArgumentNullException()
        {
            var settingStoreMock = new Mock<ISettingStore>();
            ISettingFactory settingFactory;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                settingFactory = new SettingFactory(settingStoreMock.Object, null!);
            });
        }

        #endregion Constructor Tests

        #region Create Tests

        [Fact]
        public void Create_GetNewAndExistingEvent_ShouldReturnTheSameSettings()
        {
            // Prepare
            var settingStoreMock = new Mock<ISettingStore>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            ISettingFactory settingFactory = new SettingFactory(settingStoreMock.Object, eventAggregatorMock.Object);

            // Act
            var setting1 = settingFactory.Create<ApplicationSettings>();
            var setting2 = settingFactory.Create<ApplicationSettings>();

            // Assert
            Assert.Equal(setting1, setting2);
        }

        #endregion Create Tests
    }
}
