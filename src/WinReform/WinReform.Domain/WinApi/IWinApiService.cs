using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Represents a class that acts as a service that allows for the execution of unmanaged code
    /// </summary>
    public interface IWinApiService
    {
        /// <summary>
        /// Redraws the menu bar of a window
        /// </summary>
        /// <param name="hwnd"><see cref="IntPtr"/> containing the handle of the window to redraw the menu of</param>
        void RedrawMenuBar(IntPtr hwnd);

        /// <summary>
        /// Get the <see cref="Rect"/> of a given window
        /// </summary>
        /// <param name="hwnd"><see cref="IntPtr"/> containing the handle of the window to get the <see cref="Rect"/> of</param>
        /// <returns>Returns <see cref="Rect"/> containing the dimensions of the given window</returns>
        Rect GetWindowRect(IntPtr hwnd);

        /// <summary>
        /// Gets values of a window of a specified <see cref="GwlType"/>
        /// </summary>
        /// <param name="hwnd"><see cref="IntPtr"/> containing the handle of the window to retrieve information from</param>
        /// <param name="nIndex"><see cref="GwlType"/> of the data to be requested</param>
        /// <returns>Returns <see cref="IntPtr"/> containing the requested data</returns>
        IntPtr GetWindowLongPtr(IntPtr hwnd, GwlType nIndex);

        /// <summary>
        /// Sets new values of a window for a specified <see cref="GwlType"/>
        /// </summary>
        /// <param name="hwnd"><see cref="IntPtr"/> containing the handle of the window to retrieve information from</param>
        /// <param name="nIndex"><see cref="GwlType"/> of the data to be requested</param>
        /// <param name="dwNewLong"><see cref="IntPtr"/> containing the new values to be set</param>
        /// <returns>Returns <see cref="IntPtr"/> with the newly set values</returns>
        IntPtr SetWindowLongPtr(IntPtr hwnd, GwlType nIndex, IntPtr dwNewLong);
    }
}
