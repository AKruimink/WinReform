using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Domain.WinApi.Types
{
    /// <summary>
    /// Defines a enum that represents all possible style that can be applied to a window
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles">List of Gwl values</a>
    /// </summary>
    public enum WsStyleType : long
    {
        /// <summary>
        /// Window has a thin border
        /// </summary>
        Border = 0x00800000L,

        /// <summary>
        /// Window has a title bar 
        /// <remarks>Includes <see cref="Border"/></remarks>
        /// </summary>
        Caption = 0x00C00000L,

        /// <summary>
        /// Window is a child window
        /// <remarks>Child window cannot have a menu bar</remarks>
        /// <remarks>Style cannot be used with <see cref="Popup"/></remarks>
        /// </summary>
        ChildWindow = 0x40000000L,

        /// <summary>
        /// Excludes the area occupied by child windows when drawing occurs within the parent window
        /// </summary>
        ClipChildren = 0x02000000L,

        /// <summary>
        /// Clips all other overlapping child windows out of the region of the child window to be updated
        /// </summary>
        ClipSiblings = 0x04000000L,

        /// <summary>
        /// Window is initially disabled
        /// </summary>
        Disabled = 0x08000000L,

        /// <summary>
        /// Window has a dialog border style
        /// <remarks>Window cannot have a title bar</remarks>
        /// </summary>
        DlgFrame = 0x00400000L,

        /// <summary>
        /// Window is the first control of a group of controls
        /// <remarks>The group consists of this first control and all controls defined after it, up to the next control with the <see cref="Group"/> style</remarks>
        /// </summary>
        Group = 0x00020000L,

        /// <summary>
        /// Window has a horizontal scrollbar
        /// </summary>
        HScroll = 0x00100000L,

        /// <summary>
        /// Window is initially minimized
        /// </summary>
        Iconic = 0x20000000L,

        /// <summary>
        /// Window is initially maximized
        /// </summary>
        Maximize = 0x01000000L,

        /// <summary>
        /// Window has a maximize button
        /// <remarks><see cref="SysMenu"/> also needs to be specified</remarks>
        /// <remarks>Cannot be combined with WS_EX_CONTEXTHELP </remarks>
        /// </summary>
        MaximizeBox = 0x00010000L,

        /// <summary>
        /// Window is initially minimized
        /// </summary>
        Minimize = 0x20000000L,

        /// <summary>
        /// Window has a minimize button
        /// <remarks><see cref="SysMenu"/> also needs to be specified</remarks>
        /// <remarks>Cannot be combined with WS_EX_CONTEXTHELP </remarks>
        /// </summary>
        MinimizeBox = 0x00020000L,

        /// <summary>
        /// Window is an overlaped window with a title bar and border
        /// </summary>
        Overlapped = 0x00000000L,

        /// <summary>
        /// Window is an overlapped window with a title bar and border
        /// </summary>
        OverlappedWindow = Overlapped | Caption | SysMenu | SizeBox | MinimizeBox | MaximizeBox,

        /// <summary>
        /// Window is a popup window
        /// <remarks>Cannot be used with <see cref="ChildWindow"/></remarks>
        /// </summary>
        Popup = 0x80000000L,

        /// <summary>
        /// Window is a popup window
        /// <remarks><see cref="Caption"/> need to be set for the window menu to be visible</remarks>
        /// </summary>
        PopupWindow = Popup | Border | SysMenu,

        /// <summary>
        /// Window has a sizing border
        /// </summary>
        SizeBox = 0x00040000L,

        /// <summary>
        /// Window has a window menu in it's title bar
        /// <remarks><see cref="Caption"/> need to be set for the window menu to be visible</remarks>
        /// </summary>
        SysMenu = 0x00080000L,

        /// <summary>
        /// Window can receive keyboard focus when the user presses the TAB key
        /// </summary>
        TabStop = 0x00010000L,

        /// <summary>
        /// Window is initially visible
        /// </summary>
        Visible = 0x10000000L,

        /// <summary>
        /// Window has a vertical scrollbar
        /// </summary>
        VScroll = 0x00200000L
    }
}
