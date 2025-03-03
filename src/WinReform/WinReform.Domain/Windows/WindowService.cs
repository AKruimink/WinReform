using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        /// Stores a mapping between window handles and their corresponding event hook handles.
        /// This ensures that each tracked window has a registered movement event hook,
        /// </summary>
        private readonly Dictionary<IntPtr, IntPtr> _windowHooks = [];

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
        public IEnumerable<Window> GetActiveWindows(bool showZeroSizeWindows = false)
        {
            var windows = new List<Window>();

            foreach (var process in _processService.GetActiveProcesses())
            {
                try
                {
                    // Ensure the process has a valid window
                    if (process.MainWindowHandle == IntPtr.Zero || process.HasExited)
                    {
                        continue; // Skip processes that do not have a window
                    }

                    var dimensions = _winApiService.GetWindowRect(process.MainWindowHandle);
                    var isZeroSize = dimensions.IsEmpty;
                    if (isZeroSize && !showZeroSizeWindows)
                    {
                        continue; // Skip windows with 0x0 size
                    }

                    var description = string.Empty;
                    var windowTitle = _winApiService.GetWindowTitle(process.MainWindowHandle);
                    var processName = process.ProcessName;
                    var processId = process.Id;
                    Bitmap? iconBitmap = null;

                    if (CanAccessProcess(process))
                    {
                        description = process.MainModule?.FileVersionInfo?.FileDescription ?? string.Empty;

                        if (File.Exists(process.MainModule?.FileName))
                        {
                            var icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                            iconBitmap = icon?.ToBitmap();
                        }
                    }

                    // Fallbacks: If description is empty, use process name or window title
                    if (string.IsNullOrWhiteSpace(description))
                    {
                        description = !string.IsNullOrWhiteSpace(windowTitle) ? windowTitle : processName;
                    }

                    windows.Add(new Window()
                    {
                        Id = processId,
                        WindowHandle = process.MainWindowHandle,
                        Description = description,
                        WindowTitle = windowTitle,
                        ProcessName = processName,
                        Icon = iconBitmap,
                        Dimensions = dimensions
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error processing process {process.ProcessName}: {ex.Message}");
                }
            }

            return windows.OrderBy(w => w.Description).ToList();
        }

        /// <summary>
        /// Determines whether the specified process is accessible by checking if its <see cref="Process.MainModule"/> can be read.
        /// </summary>
        /// <param name="process">The <see cref="Process"/> to check for accessibility.</param>
        /// <returns>
        /// <see langword="true"/> if the process is accessible and its modules can be read; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// Some system or protected processes restrict access to their module information,
        /// which can cause exceptions. This method prevents unnecessary exceptions
        /// by safely checking access permissions before retrieving process details.
        /// </remarks>
        private static bool CanAccessProcess(System.Diagnostics.Process process)
        {
            try
            {
                _ = process.MainModule;
                return true;
            }
            catch (Win32Exception)
            {
                return false; // Process is protected or restricted
            }
            catch (UnauthorizedAccessException)
            {
                return false; // Process access is denied
            }
        }

        /// <inheritdoc/>
        public Window UpdateWindow(Window window)
        {
            ArgumentNullException.ThrowIfNull(window);

            var updatedRect = _winApiService.GetVisibleWindowRect(window.WindowHandle);
            return new Window
            {
                Id = window.Id,
                WindowHandle = window.WindowHandle,
                Description = window.Description,
                WindowTitle = window.WindowTitle,
                ProcessName = window.ProcessName,
                Icon = window.Icon,
                Dimensions = updatedRect
            };
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

        /// <inheritdoc/>
        public void HookWindowMoveEvent(Window targetWindow, Action callback)
        {
            ArgumentNullException.ThrowIfNull(targetWindow);

            if (_windowHooks.ContainsKey(targetWindow.WindowHandle))
            {
                return; // Prevent duplicate hooks
            }

            var hook = _winApiService.RegisterWindowMoveHook(targetWindow.WindowHandle, callback);
            _windowHooks[targetWindow.WindowHandle] = hook;
        }

        /// <inheritdoc/>
        public void UnhookWindowMoveEvent(Window targetWindow)
        {
            ArgumentNullException.ThrowIfNull(targetWindow);

            if (_windowHooks.TryGetValue(targetWindow.WindowHandle, out var hookHandle))
            {
                _winApiService.UnregisterWindowMoveHook(hookHandle);
                _windowHooks.Remove(targetWindow.WindowHandle);
            }
        }

        /// <inheritdoc/>
        public void EnableClickThrough(Window window)
        {
            ArgumentNullException.ThrowIfNull(window);

            var extendedStyle = _winApiService.GetWindowLongPtr(window.WindowHandle, GwlType.ExStyle).ToInt32();
            _winApiService.SetWindowLongPtr(window.WindowHandle, GwlType.ExStyle, new IntPtr(extendedStyle | (int)ExWsStyleType.Transparent | (int)ExWsStyleType.Layered));
        }

        /// <inheritdoc/>
        public void UpdateOverlayPosition(Window overlayWindow, Rect transformedRect)
        {
            ArgumentNullException.ThrowIfNull(overlayWindow);

            // Use pre-transformed coordinates to update overlay position
            _winApiService.SetWindowPos(overlayWindow.WindowHandle, transformedRect, SwpType.NoActive | SwpType.NoZOrder);
        }
    }
}
