using WinReform.Domain.Windows;
using System;
using Xunit;

namespace WinReform.Domain.Tests.Windows
{
    /// <summary>
    /// Tests for the <see cref="Window"/>
    /// </summary>
    public class WindowTests
    {
        #region Equals Tests

        [Fact]
        public void Equals_EqualWindow_ShouldReturnTrue()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window", Dimensions = new Dimension() };
            var window2 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window", Dimensions = new Dimension() };

            // Act

            // Assert
            Assert.True(window1.Equals(window2));
        }

        [Fact]
        public void Equals_UnEqualWindow_ShouldReturnFalse()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Dimension() };
            var window2 = new Window { Id = 2, WindowHandle = (IntPtr)2, Description = "Window2", Dimensions = new Dimension() };

            // Act

            // Assert
            Assert.False(window1.Equals(window2));
        }

        [Fact]
        public void Equals_InvalidType_ShouldReturnFalse()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window", Dimensions = new Dimension() };

            // Act

            // Assert
            Assert.False(window1.Equals("Invalid Type"));
        }

        #endregion Equals Tests

        #region GetHashCode Tests

        [Fact]
        public void GetHashCode_EqualInstances_ShouldReturnEqualHashCode()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window", Dimensions = new Dimension() };
            var window2 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window", Dimensions = new Dimension() };

            // Act
            var hashCode1 = window1.GetHashCode();
            var hashCode2 = window2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_UnEqualInstances_ShouldReturnUniqueHashCode()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Dimension() };
            var window2 = new Window { Id = 2, WindowHandle = (IntPtr)2, Description = "Window2", Dimensions = new Dimension() };

            // Act
            var hashCode1 = window1.GetHashCode();
            var hashCode2 = window2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }

        #endregion GetHashCode Tests

        #region CompareTo Tests

        [Fact]
        public void CompareTo_EqualId_ShouldReturnZero()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Dimension() };
            var window2 = new Window { Id = 1, WindowHandle = (IntPtr)2, Description = "Window2", Dimensions = new Dimension() };

            // Act

            // Assert
            Assert.True(window1.CompareTo(window2) == 0);
        }

        [Fact]
        public void CompareTo_UnEqualId_ShouldReturnNegativeOne()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Dimension() };
            var window2 = new Window { Id = 2, WindowHandle = (IntPtr)2, Description = "Window2", Dimensions = new Dimension() };

            // Act

            // Assert
            Assert.True(window1.CompareTo(window2) == -1);
        }

        [Fact]
        public void CompareTo_NullInstances_ShouldReturnNegativeOne()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Dimension() };

            // Act

            // Assert
            Assert.True(window1.CompareTo(null) == -1);
        }

        #endregion CompareTo Tests
    }
}