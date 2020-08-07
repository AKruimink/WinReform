using System.Collections.Generic;

namespace WinReform.Domain.Windows
{
    /// <summary>
    /// Represents a class that acts as a service for managing active windows
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Gets the ative windows running on the system
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{Window}"/> containing all active windows on the system</returns>
        IEnumerable<Window> GetActiveWindows();

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
    }
}
