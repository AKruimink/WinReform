namespace WinReform.Domain.WinApi.Types
{
    /// <summary>
    /// Defines a class that represents all possible extended window styles (ExStyle) for the Get and Set WindowLongPtr method.
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles">List of ExStyle values</a>
    /// </summary>
    [Flags]
    public enum ExWsStyleType : uint
    {
        /// <summary>
        /// The window accepts drag-and-drop files.
        /// </summary>
        AcceptFiles = 0x00000010,

        /// <summary>
        /// The window should be placed above all non-topmost windows.
        /// </summary>
        AppWindow = 0x00040000,

        /// <summary>
        /// The window has a border with a raised edge.
        /// </summary>
        ClientEdge = 0x00000200,

        /// <summary>
        /// The window contains child windows that should be clipped relative to the parent.
        /// </summary>
        Composited = 0x02000000,

        /// <summary>
        /// The window is a layered window.
        /// NOTE: Required for transparency effects.
        /// </summary>
        Layered = 0x00080000,

        /// <summary>
        /// The window does not pass its window region to a child window.
        /// </summary>
        NoInheritLayout = 0x00100000,

        /// <summary>
        /// The window does not repaint its contents when moved.
        /// </summary>
        NoActivate = 0x08000000,

        /// <summary>
        /// The window should be placed above all non-topmost windows.
        /// </summary>
        NoParentNotify = 0x00000004,

        /// <summary>
        /// The window has a static edge appearance.
        /// </summary>
        StaticEdge = 0x00020000,

        /// <summary>
        /// The window is a topmost window.
        /// </summary>
        TopMost = 0x00000008,

        /// <summary>
        /// The window is transparent to hit testing, allowing mouse events to pass through.
        /// </summary>
        Transparent = 0x00000020,

        /// <summary>
        /// The window has a sunken border.
        /// </summary>
        WindowEdge = 0x00000100,
    }
}
