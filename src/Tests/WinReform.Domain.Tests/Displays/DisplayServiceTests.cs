using System;
using System.Collections.Generic;
using Moq;
using WinReform.Domain.Displays;
using WinReform.Domain.WinApi;
using Xunit;

namespace WinReform.Domain.Tests.Displays
{
    /// <summary>
    /// Tests for the <see cref="DisplayService"/>
    /// </summary>
    public class DisplayServiceTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_ValidArguments_ShouldConstructWindowService()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();

            // Act
            var displayService = new DisplayService(winApiServiceMock.Object);

            // Assert
            Assert.NotNull(displayService);
        }

        [Fact]
        public void Construct_NullWinApiService_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var displayService = new DisplayService(null!);
            });
        }

        #endregion Constructor Tests

        #region GetDisplays Tests

        [Fact]
        public void GetDisplays_NoDisplays_ShoulReturnEmptyDisplayList()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            winApiServiceMock.Setup(x => x.GetAllMonitors()).Returns(new List<Monitor>());
            var displayService = new DisplayService(winApiServiceMock.Object);

            // Act
            var result = displayService.GetAllDisplays();

            // Assert
            Assert.Equal(new List<Display>(), result);
        }

        [Fact]
        public void GetDisplays_ExistingDisplays_ShoulReturnListWithDisplays()
        {
            // Prepare
            var monitorList = new List<Monitor>
            {
                new Monitor
                {
                    Size = 1,
                    MonitorSize = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                    WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                    Flags = 1,
                    MonitorHandle = (IntPtr)1
                },
                new Monitor
                {
                    Size = 2,
                    MonitorSize = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                    WorkArea = new Rect { Left = 1, Top = 2, Right = 3, Bottom = 4 },
                    Flags = 1,
                    MonitorHandle = (IntPtr)2
                }
            };

            var winApiServiceMock = new Mock<IWinApiService>();
            winApiServiceMock.Setup(x => x.GetAllMonitors()).Returns(monitorList);
            var displayService = new DisplayService(winApiServiceMock.Object);

            // Act
            var result = displayService.GetAllDisplays();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        #endregion GetDisplays Tests
    }
}
