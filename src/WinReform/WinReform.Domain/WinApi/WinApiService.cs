using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WinReform.Domain.WinApi.Types;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a class that acts as a service that allows for the execution of unmanaged code
    /// </summary>
    public class WinApiService : IWinApiService
    {
        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-drawmenubar"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "DrawMenuBar", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DrawMenuBar(IntPtr hwnd);

        /// <inheritdoc/>
        public void RedrawMenuBar(IntPtr hwnd)
        {
            if (!DrawMenuBar(hwnd))
            {
                HResult.ThrowLastError();
            }
        }

        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetWindowRect", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        /// <inheritdoc/>
        public Rect GetWindowRect(IntPtr hwnd)
        {
            if (!GetWindowRect(hwnd, out var rect))
            {
                HResult.ThrowLastError();
            }
            return rect;
        }

        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos"/
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SwpType uFlags);

        /// <inheritdoc/>
        public void SetWindowPos(IntPtr hwnd, Rect position, SwpType uFlags)
        {
            if (!SetWindowPos(hwnd, IntPtr.Zero, position.Left, position.Top, position.Right, position.Bottom, uFlags))
            {
                HResult.ThrowLastError();
            }
        }

        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hwnd, GwlType nIndex);

        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongptra"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hwnd, GwlType nIndex);

        /// <inheritdoc/>
        public IntPtr GetWindowLongPtr(IntPtr hwnd, GwlType nIndex)
        {
            IntPtr returnValue;
            if (IntPtr.Size == 8)
            {
                // 64 bit system
                returnValue = GetWindowLongPtr64(hwnd, nIndex);
            }
            else
            {
                // 32 bit system
                returnValue = GetWindowLongPtr32(hwnd, nIndex);
            }

            if (returnValue == IntPtr.Zero)
            {
                HResult.ThrowLastError();
            }

            return returnValue;
        }

        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int SetWindowLongPtr32(IntPtr hWnd, GwlType nIndex, int dwNewLong);

        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongptra"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, GwlType nIndex, IntPtr dwNewLong);

        /// <inheritdoc/>
        public IntPtr SetWindowLongPtr(IntPtr hwnd, GwlType nIndex, IntPtr dwNewLong)
        {
            IntPtr returnValue;
            if (IntPtr.Size == 8)
            {
                // 64 bit system
                returnValue = SetWindowLongPtr64(hwnd, nIndex, dwNewLong);
            }
            else
            {
                // 32 bit system
                returnValue = (IntPtr)SetWindowLongPtr32(hwnd, nIndex, dwNewLong.ToInt32());
            }

            if (returnValue == IntPtr.Zero)
            {
                HResult.ThrowLastError();
            }

            return returnValue;
        }

        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfoa"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetMonitorInfoA", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref Monitor lpmi);

        /// <summary>
        /// Callback used by <see cref="EnumDisplayMonitors(IntPtr, IntPtr, EnumDisplayMonitorsDelegate, IntPtr)"/>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-monitorenumproc"/>
        /// </summary>
        private delegate bool EnumDisplayMonitorsDelegate(IntPtr hMonitor, IntPtr hdc, ref Rect lpRect, IntPtr dwData);

        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaymonitors"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "EnumDisplayMonitors", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumDisplayMonitorsDelegate lpfnEnum, IntPtr dwData);

        /// <inheritdoc/>
        public List<Monitor> GetAllMonitors()
        {
            var monitors = new List<Monitor>();

            bool Callback(IntPtr hMonitor, IntPtr hdc, ref Rect lpRect, IntPtr dwData)
            {
                var monitor = new Monitor
                {
                    Size = 40, // We harcode the value as Marshal.SizeOf returns a invalid value as our struct contains extra data (40 = MONITORINFO , 72 = MONITORINFOEX)
                    MonitorHandle = hMonitor
                };

                if (!GetMonitorInfo(hMonitor, ref monitor))
                {
                    HResult.ThrowLastError();
                }

                monitors.Add(monitor);
                return true;
            }

            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, Callback, IntPtr.Zero);

            return monitors;
        }

        /// <summary>
        /// Retrieves the title bar text of a specified window.
        /// </summary>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="lpString">Buffer to receive the title text.</param>
        /// <param name="nMaxCount">Maximum number of characters to copy, including null terminator.</param>
        /// <returns>Length of the copied string (excluding null terminator), or 0 if the window has no title.</returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtext"/>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <inheritdoc/>
        public string GetWindowTitle(IntPtr windowHandle)
        {
            const int nChars = 256;
            var buffer = new StringBuilder(256);

            if (windowHandle != IntPtr.Zero)
            {
                if (GetWindowText(windowHandle, buffer, nChars) > 0)
                {
                    return buffer.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Hooks into Windows events to track window position changes.
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwineventhook">SetWinEventHook</a>
        /// </summary>
        /// <param name="eventMin">The lowest event value to track.</param>
        /// <param name="eventMax">The highest event value to track.</param>
        /// <param name="hmodWinEventProc">Handle to the DLL containing the event hook function (null for inline).</param>
        /// <param name="lpfnWinEventProc">The callback function to be called when an event is triggered.</param>
        /// <param name="idProcess">The process ID to track (0 for all processes).</param>
        /// <param name="idThread">The thread ID to track (0 for all threads).</param>
        /// <param name="dwFlags">Flags to control event hook behavior.</param>
        /// <returns>A handle to the event hook.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        /// <summary>
        /// Removes a previously registered event hook.
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwinevent">UnhookWinEvent</a>
        /// </summary>
        /// <param name="hWinEventHook">Handle to the event hook to be removed.</param>
        /// <returns><see langword="true"/> if the hook was successfully removed, otherwise <see langword="false"/>.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        /// <summary>
        /// Stores active window event hooks to prevent garbage collection.
        /// This ensures that callbacks remain valid for the lifetime of the hook.
        /// </summary>
        private static readonly Dictionary<IntPtr, WinEventDelegate> _eventHooks = new();

        /// <summary>
        /// Delegate for handling Windows event hook callbacks.
        /// This is used to receive notifications when specific window events occur,
        /// such as window position changes.
        /// </summary>
        /// <param name="hWinEventHook">Handle to the event hook.</param>
        /// <param name="eventType">The type of event that was triggered.</param>
        /// <param name="hwnd">The handle to the window associated with the event.</param>
        /// <param name="idObject">The object identifier for the event.</param>
        /// <param name="idChild">The child identifier of the event target.</param>
        /// <param name="dwEventThread">The thread ID where the event was generated.</param>
        /// <param name="dwmsEventTime">The timestamp of the event, in milliseconds.</param>
        private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        /// <inheritdoc/>
        public IntPtr RegisterWindowMoveHook(IntPtr windowHandle, Action callback)
        {
            var winEventCallback = new WinEventDelegate((hWinEventHook, eventType, hwnd, idObject, idChild, dwEventThread, dwmsEventTime) =>
            {
                if (hwnd == windowHandle)
                {
                    callback();
                }
            });

            var hookHandle = SetWinEventHook(0x800B, 0x800B, IntPtr.Zero, winEventCallback, 0, 0, 0);
            if (hookHandle != IntPtr.Zero)
            {
                _eventHooks[hookHandle] = winEventCallback;
            }

            return hookHandle;
        }

        /// <inheritdoc/>
        public void UnregisterWindowMoveHook(IntPtr hookHandle)
        {
            if (hookHandle != IntPtr.Zero)
            {
                UnhookWinEvent(hookHandle);
                _eventHooks.Remove(hookHandle);
            }
        }

        /// <summary>
        /// Retrieves the extended frame bounds of a window, excluding non-client areas.
        /// <a href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute">DwmGetWindowAttribute</a>
        /// </summary>
        /// <param name="hwnd">Handle to the window.</param>
        /// <param name="dwAttribute">The attribute to retrieve (e.g., DWMWA_EXTENDED_FRAME_BOUNDS).</param>
        /// <param name="pvAttribute">Receives the attribute value.</param>
        /// <param name="cbAttribute">Size of the output buffer.</param>
        /// <returns>0 if successful, otherwise a Win32 error code.</returns>
        [DllImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute", PreserveSig = true)]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rect pvAttribute, int cbAttribute);

        /// <inheritdoc/>
        public Rect GetVisibleWindowRect(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                throw new ArgumentException("Invalid window handle.", nameof(hwnd));
            }

            var result = DwmGetWindowAttribute(hwnd, (int)DwmWindowAttributeType.ExtendedFrameBounds, out var rect, Marshal.SizeOf(typeof(Rect)));
            if (result != 0)
            {
                throw new InvalidOperationException($"DwmGetWindowAttribute failed with error code {result}");
            }

            return rect;
        }
    }
}
