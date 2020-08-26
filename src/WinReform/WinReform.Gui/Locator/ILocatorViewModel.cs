using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Gui.Locator
{
    /// <summary>
    /// Represents a class that provides data and functionality for the relocation of windows
    /// </summary>
    public interface ILocatorViewModel
    {

        /// <summary>
        /// Gets or Sets the new X axis to be applied to all selected windows
        /// NOTE: if left empty the original X axis of the window(s) will be used
        /// </summary>
        string NewXAxis { get; set; }

        /// <summary>
        /// Gets or Sets The new Y axis to be applied to all selected windows
        /// NOTE: if left empty the original Y axis of the window(s) will be used
        /// </summary>
        string NewYAxis { get; set; }

        /// <summary>
        /// <see cref="List{Domain.Windows.Window}"/> containing all currently selected windows
        /// </summary>
        List<Domain.Windows.Window> SelectedWindows { get; set; }
    }
}
