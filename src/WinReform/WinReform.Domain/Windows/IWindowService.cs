using System.Drawing.Drawing2D;
using WinReform.Domain.WinApi;

namespace WinReform.Domain.Windows
{
    /// <summary>
    /// Represents a class that acts as a service for managing active windows
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Gets the active windows running on the system, with optional filtering for processes without windows and zero-size windows.
        /// </summary>
        /// <param name="showZeroSizeWindows">If <see langword="true"/>, includes windows with a size of 0x0. Defaults to <see langword="false"/>.</param>
        /// <returns>Returns an <see cref="IEnumerable{Window}"/> containing all active windows on the system, based on the applied filters.</returns>
        IEnumerable<Window> GetActiveWindows(bool showZeroSizeWindows = false);

        /// <summary>
        /// Updates a given window with the latest dimensions
        /// </summary>
        /// <param name="window">The <see cref="Window"/> to fetch updated dimensions for.</param>
        /// <returns>A new <see cref="Window"/> instance with updated dimensions.</returns>
        Window UpdateWindow(Window window);

        /// <summary>
        /// Resize a window
        /// </summary>
        /// <param name="window"><see cref="Window"/> to be resized</param>
        /// <param name="resolution"><see cref="Rect"/> containing the new size</param>
        void ResizeWindow(Window window, Rect resolution);

        /// <summary>
        /// Relocates a window
        /// </summary>
        /// <param name="window"><see cref="Window"/> to be relocated</param>
        /// <param name="location"><see cref="Rect"/> containing the new location</param>
        void RelocateWindow(Window window, Rect location);

        /// <summary>
        /// Sets the border style of a window to a resizable one
        /// </summary>
        /// <param name="window"><see cref="Window"/> to change the border style of</param>
        /// <returns>Returns <see langword="true"/> if the style was successfully set, otherwise returns <see langword="false"/></returns>
        bool SetResizableBorder(Window window);

        /// <summary>
        /// Redraws an existing window
        /// </summary>
        /// <param name="window"><see cref="Window"/> to be redrawn</param>
        void RedrawWindow(Window window);

        /// <summary>
        /// Hooks into the Windows event system to track when a specific window moves.
        /// </summary>
        /// <param name="targetWindow">The <see cref="Window"/> whose movement should be tracked.</param>
        /// <param name="callback">The action to invoke when the window moves.</param>
        void HookWindowMoveEvent(Window targetWindow, Action callback);

        /// <summary>
        /// Unhooks a previously registered window move event, stopping movement tracking for the specified window.
        /// </summary>
        /// <param name="targetWindow">The <see cref="Window"/> whose movement tracking should be removed.</param>
        void UnhookWindowMoveEvent(Window targetWindow);

        /// <summary>
        /// Enables click-through behavior for a specified window, making it non-interactable.
        /// </summary>
        /// <param name="window">The <see cref="Window"/> to apply click-through behavior to.</param>
        void EnableClickThrough(Window window);

        /// <summary>
        /// Updates the position and size of an overlay window to match the target window,
        /// using pre-transformed coordinates provided by the caller.
        /// </summary>
        /// <param name="overlayWindow">The <see cref="Window"/> overlay that should be repositioned.</param>
        /// <param name="transformedRect">The transformed <see cref="Rect"/> representing the new position and size.</param>
        void UpdateOverlayPosition(Window overlayWindow, Rect transformedRect);
    }
}
