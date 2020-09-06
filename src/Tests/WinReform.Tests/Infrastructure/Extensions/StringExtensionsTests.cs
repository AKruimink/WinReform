using System;
using Xunit;

namespace WinReform.Tests.Infrastructure.Extensions
{
    /// <summary>
    /// Tests for the <see cref="StringExtensions"/>
    /// </summary>
    public class StringExtensionsTests
    {
        #region UpdateCollection Tests

        [Fact]
        public void Contains_DoesContainString_ShouldReturnTrue()
        {
            // Prepare
            var source = "Some Random String";
            var other = "Random";

            // Act
            var result = source.Contains(other, StringComparison.OrdinalIgnoreCase);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Contains_DoesNotContainString_ShouldReturnFalse()
        {
            // Prepare
            var source = "Some Random String";
            var other = "Invalid";

            // Act
            var result = source.Contains(other, StringComparison.OrdinalIgnoreCase);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Contains_CaseSensative_ShouldReturnFalse()
        {
            // Prepare
            var source = "Some Random String";
            var other = "random";

            // Act
            var result = source.Contains(other, StringComparison.InvariantCulture);

            // Assert
            Assert.False(result);
        }

        #endregion UpdateCollection Tests
    }
}
