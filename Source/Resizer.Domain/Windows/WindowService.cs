using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Resizer.Domain.Infrastructure.Events;
using Resizer.Domain.Infrastructure.Messenger;

namespace Resizer.Domain.Windows
{
    /// <summary>
    /// Defines a class that acts as a service for managing active windows
    /// </summary>
    public class WindowService : IWindowService
    {
        /// <summary>
        /// Gets the dimensions of a window
        /// TODO: should probably move GetWindowsRect to its own service, as it will be required for more then just this
        /// </summary>
        /// <param name="hwnd">The <see cref="IntPtr"/> that points towarts the window to get the dimensions of</param>
        /// <param name="lpRect">The <see cref="Dimensions"/> that is returned</param>
        /// <returns>Returns <see cref="Dimensions"/> containing the dimensions of the window</returns>
        [DllImport("user32.dll", SetLastError =true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Dimensions lpRect);

        /// <inheritdoc/>
        public IEnumerable<Window> GetActiveWindows()
        {
            var windows = new List<Window>();

            foreach(var process in Process.GetProcesses())
            {
                if(process.MainWindowHandle == IntPtr.Zero)
                {
                    continue; // Process doesn't own a window
                }

                windows.Add(new Window()
                {
                    Id = process.Id,
                    WindowHandle = process.MainWindowHandle,
                    Description = process.MainModule.FileVersionInfo.FileDescription,
                    Icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName).ToBitmap(),
                    Dimensions = GetWindowDimensions(process.MainWindowHandle)
                });
            }

            return windows;
        }

        /// <summary>
        /// Get the dimensions of a window
        /// </summary>
        /// <param name="handle">The <see cref="IntPtr"/> of the window to get the dimensions from</param>
        /// <returns>Returns <see cref="Dimensions"/> containing all the dimensions of the window</returns>
        private Dimensions GetWindowDimensions(IntPtr handle)
        {
            if(GetWindowRect(handle, out var dimensions))
            {
                return dimensions;
            }
            throw new ArgumentException("Provided handle does not correlated with an existing window", nameof(handle));
        }
    }
}
