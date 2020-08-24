using System.Collections.Generic;
using WinReform.Domain.WinApi;

namespace WinReform.Gui.Resizer
{
    /// <summary>
    /// Represents a class that provides data and functionality for resizing of windows
    /// </summary>
    public interface IResizerViewModel
    {
        /// <summary>
        /// Gets all preset resolutions
        /// </summary>
        Dictionary<string, Rect> Resolutions { get; }

        /// <summary>
        /// Gets or Sets the new width to be applied to all selected windows
        /// NOTE: if left empty the original width of the window(s) will be used
        /// </summary>
        string NewWidth { get; set; }

        /// <summary>
        /// Gets or Sets The new height to be applied to all selected windows
        /// NOTE: if left empty the original height of the window(s) will be used
        /// </summary>
        string NewHeight { get; set; }

        /// <summary>
        /// <see cref="List{Domain.Windows.Window}"/> containing all currently selected windows
        /// </summary>
        List<Domain.Windows.Window> SelectedWindows { get; set; }
    }
}
