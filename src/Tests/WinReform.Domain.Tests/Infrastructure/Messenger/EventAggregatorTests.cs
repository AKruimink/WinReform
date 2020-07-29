using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Tests.Infrastructure.Messenger.Mocks;
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
            var instance1 = eventAggregator.GetEvent<EventBaseMock>();
            var instance2 = eventAggregator.GetEvent<EventBaseMock>();

            // Assert
            Assert.Equal(instance1, instance2);
        }

        #endregion GetEvent Tests
    }
}