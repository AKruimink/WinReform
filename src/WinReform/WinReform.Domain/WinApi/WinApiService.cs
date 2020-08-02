using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a class that acts as a service that allows for the execution of unmanaged code
    /// </summary>
    public class WinApiService : IWinApiService
    {
        /// <summary>
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect"/>
        /// </summary>
        [DllImport("user32.dll", EntryPoint ="GetWindowRect", SetLastError =true)]
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

            if(returnValue == IntPtr.Zero)
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
        public IntPtr GetWindowLongPtr(IntPtr hwnd, GwlType nIndex, IntPtr dwNewLong)
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
    }
}
