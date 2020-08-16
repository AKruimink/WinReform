using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Tests.Fixtures;
using Xunit;

namespace WinReform.Domain.Tests.Infrastructure.Messenger
{
    /// <summary>
    /// Tests for the <see cref="IEventAggregator"/>
    /// </summary>
    public class EventAggregatorTests
    {
        #region GetEvent Tests

        [Fact]
        public void GetEvent_GetNewAndExistingEvent_ShouldReturnTheSameEvent()
        {
            // Prepare
            var eventAggregator = new EventAggregator();

            // Act
            var instance1 = eventAggregator.GetEvent<EventFixture>();
            var instance2 = eventAggregator.GetEvent<EventFixture>();

            // Assert
            Assert.Equal(instance1, instance2);
        }

        #endregion GetEvent Tests
    }
}
