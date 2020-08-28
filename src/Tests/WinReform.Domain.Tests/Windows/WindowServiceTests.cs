using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Moq;
using WinReform.Domain.Process;
using WinReform.Domain.WinApi;
using WinReform.Domain.Windows;
using Xunit;

namespace WinReform.Domain.Tests.Windows
{
    /// <summary>
    /// Tests for the <see cref="WindowService"/>
    /// </summary>
    public class WindowServiceTests : IDisposable
    {
        /// <summary>
        /// <see cref="List{System.Diagnostics.Process}"/> containing proccesses some of which own a window
        /// </summary>
        private readonly List<System.Diagnostics.Process> _windowProcessList = new List<System.Diagnostics.Process>();

        /// <summary>
        /// <see cref="List{System.Diagnostics.Process}"/> containing processes non of which owns a window
        /// </summary>
        private readonly List<System.Diagnostics.Process> _processList = new List<System.Diagnostics.Process>();

        /// <summary>
        /// Create a new instance of <see cref="WindowServiceTests"/>
        /// TODO: Want to improve the way we start our test processes in <see cref="WindowServiceTests"/> as this hardcoded path is messy and might cause issues in the future
        /// </summary>
        public WindowServiceTests()
        {
            for (var i = 0; i <= 2; i++)
            {
                var windowProcess = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = Directory.GetCurrentDirectory().Replace("WinReform.Domain.Tests", "WinReform.Tests.WindowProcess.Fixture") + "//WinReform.Tests.WindowProcess.Fixture.exe"
                    }
                };
                windowProcess.Start();

                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = Directory.GetCurrentDirectory().Replace("WinReform.Domain.Tests", "WinReform.Tests.Process.Fixture") + "//WinReform.Tests.Process.Fixture.exe"
                    }
                };
                process.Start();

                _windowProcessList.Add(windowProcess);
                _windowProcessList.Add(process);
                _processList.Add(process);
            }
            // Wait some time for the processes to be started as tests might otherwise fail
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Disposes all processes used during tests of <see cref="WindowServiceTests"/>
        /// </summary>
        public void Dispose()
        {
            // Note: as the _windowProcessList contains all the proccess we spawned we don't need to kill the _processList separately
            foreach (var process in _windowProcessList)
            {
                process.Kill();
                process.Dispose();
            }

            _windowProcessList.Clear();
            _processList.Clear();
        }

        #region Constructor Tests

        [Fact]
        public void Construct_ValidArguments_ShouldConstructWindowService()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            // Act
            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Assert
            Assert.NotNull(windowService);
        }

        [Fact]
        public void Construct_NullWinApiService_ShouldThrowArgumentNullException()
        {
            // Prepare
            var processServiceMock = new Mock<IProcessService>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var windowService = new WindowService(null!, processServiceMock.Object);
            });
        }

        [Fact]
        public void Construct_NullProcessService_ShouldThrowArgumentNullException()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var windowService = new WindowService(winApiServiceMock.Object, null!);
            });
        }

        #endregion Constructor Tests

        #region GetActiveWindows Tests

        [Fact]
        public void GetActiveWindows_EmptyProcessList_ShouldReturnEmptyWindowList()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();
            var processMock = new Mock<List<System.Diagnostics.Process>>();

            processServiceMock.Setup(x => x.GetActiveProcesses()).Returns(new List<System.Diagnostics.Process>());

            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            var result = windowService.GetActiveWindows();

            // Assert
            Assert.Equal(new List<Window>(), result);
        }

        [Fact]
        public void GetActiveWindows_ProcessesWithoutWindows_ShouldReturnEmptyWindowList()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            processServiceMock.Setup(x => x.GetActiveProcesses()).Returns(_processList);

            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            var result = windowService.GetActiveWindows();

            // Assert
            Assert.Equal(new List<Window>(), result);
        }

        [Fact]
        public void GetActiveWindows_ProcessesWithWindows_ShouldReturnListWithWindows()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            processServiceMock.Setup(x => x.GetActiveProcesses()).Returns(_windowProcessList);

            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            var result = windowService.GetActiveWindows();

            // Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result.FirstOrDefault()?.WindowHandle);
        }

        #endregion GetActiveWindows Tests

        #region ResizeWindow Tests

        [Fact]
        public void ResizeWindow_NewResolution_ShoulUseNewResolution()
        {
            // Prepare
            Rect? sendRect = default;

            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            winApiServiceMock.Setup(x => x.SetWindowPos(It.IsAny<IntPtr>(), It.IsAny<Rect>(), It.IsAny<SwpType>()))
                .Callback<IntPtr, Rect, SwpType>((handle, rect, swpType) => sendRect = rect);

            var windowMock = new Window
            {
                WindowHandle = (IntPtr)1,
                Dimensions = new Rect { Left = 1, Top = 1, Right = 100, Bottom = 200 }
            };
            var resolutionMock = new Rect
            {
                Left = 100,
                Top = 200,
                Right = 150,
                Bottom = 250
            };
            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            windowService.ResizeWindow(windowMock, resolutionMock);

            // Assert
            Assert.NotNull(sendRect);
            Assert.Equal(resolutionMock.Right, sendRect!.Value.Right);
            Assert.Equal(resolutionMock.Bottom, sendRect!.Value.Bottom);
            Assert.Equal(windowMock.Dimensions.Left, sendRect!.Value.Left);
            Assert.Equal(windowMock.Dimensions.Top, sendRect!.Value.Top);
        }

        [Fact]
        public void ResizeWindow_EmptyRect_ShoulUseOriginalDimensions()
        {
            // Prepare
            Rect? sendRect = default;

            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            winApiServiceMock.Setup(x => x.SetWindowPos(It.IsAny<IntPtr>(), It.IsAny<Rect>(), It.IsAny<SwpType>()))
                .Callback<IntPtr, Rect, SwpType>((handle, rect, swpType) => sendRect = rect);

            var windowMock = new Window
            {
                WindowHandle = (IntPtr)1,
                Dimensions = new Rect { Left = 1, Top = 1, Right = 100, Bottom = 200 }
            };
            var resolutionMock = new Rect();
            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            windowService.ResizeWindow(windowMock, resolutionMock);

            // Assert
            Assert.NotNull(sendRect);
            Assert.Equal(windowMock.Dimensions, sendRect);
        }

        #endregion ResizeWindow Tests

        #region RelocateWindow Tests

        [Fact]
        public void RelocateWindow_Rect_ShouldPassRect()
        {
            // Prepare
            Rect? sendRect = default;

            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            winApiServiceMock.Setup(x => x.SetWindowPos(It.IsAny<IntPtr>(), It.IsAny<Rect>(), It.IsAny<SwpType>()))
                .Callback<IntPtr, Rect, SwpType>((handle, rect, swpType) => sendRect = rect);

            var windowMock = new Window { WindowHandle = (IntPtr)1 };
            var resolutionMock = new Rect { Left = 1, Top = 2, Right = 100, Bottom = 200 };
            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            windowService.RelocateWindow(windowMock, resolutionMock);

            // Assert
            Assert.NotNull(sendRect);
            Assert.Equal(resolutionMock, sendRect);
        }

        #endregion RelocateWindow Tests

        #region SetResizableBorder Tests

        [Fact]
        public void SetResizableBorder_ValidWindow_ShouldReturnTrue()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            winApiServiceMock.Setup(x => x.GetWindowLongPtr(It.IsAny<IntPtr>(), It.IsAny<GwlType>())).Returns((IntPtr)1);
            winApiServiceMock.Setup(x => x.SetWindowLongPtr(It.IsAny<IntPtr>(), It.IsAny<GwlType>(), It.IsAny<IntPtr>())).Returns((IntPtr)1);

            var windowMock = new Window { WindowHandle = (IntPtr)1 };
            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            var result = windowService.SetResizableBorder(windowMock);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void SetResizableBorder_InvalidWindow_ShouldReturnFalse()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            winApiServiceMock.Setup(x => x.GetWindowLongPtr(It.IsAny<IntPtr>(), It.IsAny<GwlType>())).Returns((IntPtr)1);
            winApiServiceMock.Setup(x => x.SetWindowLongPtr(It.IsAny<IntPtr>(), It.IsAny<GwlType>(), It.IsAny<IntPtr>())).Returns(IntPtr.Zero);

            var windowMock = new Window { WindowHandle = (IntPtr)1 };
            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            var result = windowService.SetResizableBorder(windowMock);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SetResizableBorder_ThrownException_ShouldReturnFalse()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();

            winApiServiceMock.Setup(x => x.GetWindowLongPtr(It.IsAny<IntPtr>(), It.IsAny<GwlType>())).Returns((IntPtr)1);
            winApiServiceMock.Setup(x => x.SetWindowLongPtr(It.IsAny<IntPtr>(), It.IsAny<GwlType>(), It.IsAny<IntPtr>())).Throws(new Exception());

            var windowMock = new Window { WindowHandle = (IntPtr)1 };
            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            var result = windowService.SetResizableBorder(windowMock);

            // Assert
            Assert.False(result);
        }

        #endregion SetResizableBorder Tests

        #region RedrawWindow Tests

        [Fact]
        public void RedrawWindow_ExecuteWindowRedraw_ShouldCallRedrawMenuBar()
        {
            // Prepare
            var winApiServiceMock = new Mock<IWinApiService>();
            var processServiceMock = new Mock<IProcessService>();
            var windowMock = new Mock<Window>();
            var windowService = new WindowService(winApiServiceMock.Object, processServiceMock.Object);

            // Act
            windowService.RedrawWindow(windowMock.Object);

            // Assert
            winApiServiceMock.Verify(x => x.RedrawMenuBar(It.IsAny<IntPtr>()), Times.Once());
        }

        #endregion RedrawWindow Tests
    }
}
