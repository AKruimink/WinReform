using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using WinReform.Domain.Process;
using WinReform.Domain.WinApi;
using WinReform.Domain.WinApi.Types;

namespace WinReform.Domain.Windows
{
    /// <summary>
    /// Defines a class that acts as a service for managing active windows
    /// </summary>
    public class WindowService : IWindowService
    {
        /// <summary>
        /// <see cref="IWinApiService"/> used to manage existing windows
        /// </summary>
        private readonly IWinApiService _winApiService;

        /// <summary>
        /// <see cref="IProcessService"/> used to active processes
        /// </summary>
        private readonly IProcessService _processService;

        /// <summary>
        /// Create a new instance of <see cref="WindowService"/>
        /// </summary>
        /// <param name="winApiService">Instance of <see cref="IWinApiService"/> used to manage existing windows</param>
        /// <param name="processService">Instance of <see cref="IProcessService"/> used to manage active processes</param>
        public WindowService(IWinApiService winApiService, IProcessService processService)
        {
            _winApiService = winApiService ?? throw new ArgumentNullException(nameof(winApiService));
            _processService = processService ?? throw new ArgumentNullException(nameof(processService));
        }

        /// <inheritdoc/>
        public IEnumerable<Window> GetActiveWindows()
        {
            var windows = new List<Window>();

            foreach (var process in _processService.GetActiveProcesses())
            {
                try
                {
                    if (process.MainWindowHandle == IntPtr.Zero)
                    {
                        continue; // Process doesn't own a window
                    }

                    windows.Add(new Window()
                    {
                        Id = process.Id,
                        WindowHandle = process.MainWindowHandle,
                        Description = process.MainModule?.FileVersionInfo.FileDescription ?? string.Empty,
                        Icon = Icon.ExtractAssociatedIcon(process.MainModule?.FileName).ToBitmap(),
                        Dimensions = _winApiService.GetWindowRect(process.MainWindowHandle)
                    });
                }
                catch (Win32Exception)
                {
                    continue;
                }
            }

            return windows.OrderBy(w => w.Description).ToList();
        }

        /// <inheritdoc/>
        public void ResizeWindow(Window window, Rect resolution)
        {
            var newWidth = resolution.Right == 0 ? window.Dimensions.Right : resolution.Right;
            var newHeight = resolution.Bottom == 0 ? window.Dimensions.Bottom : resolution.Bottom;
            var newPosition = new Rect { Left = window.Dimensions.Left, Top = window.Dimensions.Top, Right = newWidth, Bottom = newHeight };

            _winApiService.SetWindowPos(window.WindowHandle, newPosition, SwpType.NoMove | SwpType.NoActive | SwpType.NoZOrder);
        }

        /// <inheritdoc/>
        public void RelocateWindow(Window window, Rect location)
        {
            _winApiService.SetWindowPos(window.WindowHandle, location, SwpType.NoSize | SwpType.NoActive | SwpType.NoZOrder);
        }

        /// <inheritdoc/>
        public bool SetResizableBorder(Window window)
        {
            try
            {
                var currentStyle = _winApiService.GetWindowLongPtr(window.WindowHandle, GwlType.Style);
                if (_winApiService.SetWindowLongPtr(window.WindowHandle, GwlType.Style, (IntPtr)((long)currentStyle | (long)WsStyleType.OverlappedWindow)) != IntPtr.Zero)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public void RedrawWindow(Window window)
        {
            _winApiService.RedrawMenuBar(window.WindowHandle);
        }
    }
}
