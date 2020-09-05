using WinReform.Domain.WinApi;
using Xunit;

namespace WinReform.Domain.Tests.WinApi
{
    /// <summary>
    /// Tests for the <see cref="Rect"/>
    /// </summary>
    public class RectTests
    {
        #region Union Tests

        [Fact]
        public void Union_ValidRects_ShouldFitBothRects()
        {
            // Prepare
            var rect1 = new Rect(0, 0, 500, 500);
            var rect2 = new Rect(0, 0, 600, 400);

            // Act
            var unionRect = Rect.Union(rect1, rect2);

            // Assert
            Assert.True(unionRect.Left >= rect1.Left);
            Assert.True(unionRect.Top >= rect1.Top);
            Assert.True(unionRect.Right >= rect2.Right);
            Assert.True(unionRect.Bottom >= rect1.Bottom);
        }

        #endregion Union Tests

        #region Equals Tests

        [Fact]
        public void Equals_EqualRects_ShouldReturnTrue()
        {
            // Prepare
            var rect1 = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 };
            var rect2 = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 };

            // Act

            // Assert
            Assert.True(rect1.Equals(rect2));
        }

        [Fact]
        public void Equals_UnEqualRects_ShouldReturnFalse()
        {
            // Prepare
            var rect1 = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 };
            var rect2 = new Rect { Left = 4, Top = 3, Right = 2, Bottom = 1 };

            // Act

            // Assert
            Assert.False(rect1.Equals(rect2));
        }

        [Fact]
        public void Equals_InvalidType_ShouldReturnFalse()
        {
            // Prepare
            var rect1 = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 };

            // Act

            // Assert
            Assert.False(rect1.Equals(1));
        }

        #endregion Equals Tests
    }
}
