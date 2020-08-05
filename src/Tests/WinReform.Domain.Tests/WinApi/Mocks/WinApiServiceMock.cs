using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Domain.WinApi;

namespace WinReform.Domain.Tests.WinApi.Mocks
{
    /// <summary>
    /// Mock implementation of <see cref="IWinApiService"/>
    /// </summary>
    public class WinApiServiceMock : IWinApiService
    {
        /// <summary>
        /// The <see cref="Rect"/> to be returned by <see cref="GetWindowRect(IntPtr)"/>
        /// </summary>
        public Rect RectToReturn { get; set; }

        /// <summary>
        /// The <see cref="IntPtr"/> returned by <see cref="GetWindowLongPtr(IntPtr, GwlType)"/>
        /// </summary>
        public IntPtr LongToReturn { get; set; }

        ///<inheritdoc/>
        public Rect GetWindowRect(IntPtr hwnd) => RectToReturn;

        ///<inheritdoc/>
        public IntPtr GetWindowLongPtr(IntPtr hwnd, GwlType nIndex) => LongToReturn;

        ///<inheritdoc/>
        public IntPtr SetWindowLongPtr(IntPtr hwnd, GwlType nIndex, IntPtr dwNewLong) => dwNewLong;
    }
}
