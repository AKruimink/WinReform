using System;
using WinReform.Domain.Displays;
using WinReform.Domain.WinApi;
using Xunit;

namespace WinReform.Domain.Tests.Displays
{
    /// <summary>
    /// Tests for the <see cref="Display"/>
    /// </summary>
    public class DisplayTests
    {
        #region Equals Tests

        [Fact]
        public void Equals_EqualDisplay_ShouldReturnTrue()
        {
            // Prepare
            var display1 = new Display
            {
                Id = 1,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };
            var display2 = new Display
            {
                Id = 1,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };

            // Act

            // Assert
            Assert.True(display1.Equals(display2));
        }

        [Fact]
        public void Equals_UnEqualDisplay_ShouldReturnFalse()
        {
            // Prepare
            var display1 = new Display
            {
                Id = 1,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };
            var display2 = new Display
            {
                Id = 2,
                DisplayHandle = (IntPtr)2,
                Primary = false,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };

            // Act

            // Assert
            Assert.False(display1.Equals(display2));
        }

        [Fact]
        public void Equals_InvalidType_ShouldReturnFalse()
        {
            // Prepare
            var display = new Display
            {
                Id = 1,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };

            // Act

            // Assert
            Assert.False(display.Equals("Invalid Type"));
        }

        #endregion Equals Tests

        #region CompareTo Tests

        [Fact]
        public void CompareTo_EqualId_ShouldReturnZero()
        {
            // Prepare
            var display1 = new Display
            {
                Id = 1,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };
            var display2 = new Display
            {
                Id = 1,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };

            // Act

            // Assert
            Assert.True(display1.CompareTo(display2) == 0);
        }

        [Fact]
        public void CompareTo_UnEqualId_ShouldReturnNegativeOne()
        {
            // Prepare
            var display1 = new Display
            {
                Id = 1,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };
            var display2 = new Display
            {
                Id = 2,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };

            // Act

            // Assert
            Assert.True(display1.CompareTo(display2) == -1);
        }

        [Fact]
        public void CompareTo_NullInstances_ShouldReturnNegativeOne()
        {
            // Prepare
            var display1 = new Display
            {
                Id = 1,
                DisplayHandle = (IntPtr)1,
                Primary = true,
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 }
            };

            // Act

            // Assert
            Assert.True(display1.CompareTo(null) == -1);
        }

        #endregion CompareTo Tests
    }
}
