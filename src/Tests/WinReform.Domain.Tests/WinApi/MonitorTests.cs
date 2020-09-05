using System;
using WinReform.Domain.WinApi;
using Xunit;

namespace WinReform.Domain.Tests.WinApi
{
    /// <summary>
    /// Tests for the <see cref="Monitor"/>
    /// </summary>
    public class MonitorTests
    {
        #region Equals Tests

        [Fact]
        public void Equals_EqualMonitors_ShouldReturnTrue()
        {
            // Prepare
            var monitor1 = new Monitor
            {
                Size = 1,
                MonitorSize = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                Flags = 1,
                MonitorHandle = (IntPtr)1
            };
            var monitor2 = new Monitor
            {
                Size = 1,
                MonitorSize = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                Flags = 1,
                MonitorHandle = (IntPtr)1
            };

            // Assert
            Assert.True(monitor1.Equals(monitor2));
        }

        [Fact]
        public void Equals_UnEqualMonitors_ShouldReturnFalse()
        {
            // Prepare
            var monitor1 = new Monitor
            {
                Size = 1,
                MonitorSize = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                Flags = 1,
                MonitorHandle = (IntPtr)1
            };
            var monitor2 = new Monitor
            {
                Size = 2,
                MonitorSize = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                Flags = 2,
                MonitorHandle = (IntPtr)2
            };

            // Assert
            Assert.False(monitor1.Equals(monitor2));
        }

        [Fact]
        public void Equals_InvalidType_ShouldReturnFalse()
        {
            // Prepare
            var monitor = new Monitor
            {
                Size = 1,
                MonitorSize = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                Flags = 1,
                MonitorHandle = (IntPtr)1
            };

            // Assert
            Assert.False(monitor.Equals(1));
        }

        #endregion Equals Tests
    }
}
