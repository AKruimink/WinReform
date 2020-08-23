using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a class that represents all possible SWP type for the Set WindowPos method
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos">List of Swp values</a>
    /// </summary>
    public enum SwpType : long
    {
        /// <summary>
        /// Posts the request to the thread that own the window
        /// NOTE: this prevents the calling thread from blocking its execution while another thread processes the request
        /// </summary>
        AsyncWindowPos = 0x4000,

        /// <summary>
        /// Prevents the generation of the <see cref="SyncPaint"/> message
        /// </summary>
        DeferErase = 0x2000,

        /// <summary>
        /// Applies new frame styles set using the SetWindowLong function
        /// NOTE: Sends a WM_NCCALCSIZE message to the window even when the size doesn't chage unless specified
        /// </summary>
        FrameChanged = 0x0020,

        /// <summary>
        /// Hides the window
        /// </summary>
        HideWindow = 0x0080,

        /// <summary>
        /// Does not active the window
        /// NOTE: If not specified the window will be activated and moved to either the top most of non-topmost group
        /// </summary>
        NoActive = 0x0010,

        /// <summary>
        /// Discards the entire contents of the client area
        /// NOTE: if not specified the contents of the client area are saved and copied back into the client area after the window is resized or repositioned
        /// </summary>
        NoCopyBits = 0x0100,

        /// <summary>
        /// Retains the current position
        /// NOTE: ignores the x and y parameters
        /// </summary>
        NoMove = 0x0002,

        /// <summary>
        /// Does not change the owner window's position in the Z order
        /// </summary>
        NoOwnerZOrder = 0x0200,

        /// <summary>
        /// Does not redraw changes
        /// NOTE: this includes the titlebar and scrollbar
        /// </summary>
        NoRedraw = 0x0008,

        /// <summary>
        /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message
        /// </summary>
        NoSendChanging = 0x0400,

        /// <summary>
        /// Retains the current size
        /// NOTE: ignores the cx and cy parameters
        /// </summary>
        NoSize = 0x0001,

        /// <summary>
        /// Retains the current Z order
        /// NOTE: ignores the hwndInsertAfter parameter
        /// </summary>
        NoZOrder = 0x0004,

        /// <summary>
        /// Displays the window
        /// </summary>
        ShowWindow = 0x0040,
    }
}
