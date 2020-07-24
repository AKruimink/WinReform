using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Resizer.Gui.ActiveWindows
{
    /// <summary>
    /// Represents a class that provides active windows running on the system and management of said windows
    /// </summary>
    public interface IActiveWindowsViewModel
    {
        /// <summary>
        /// Gets or Sets all active windows currently open on the system
        /// </summary>
        ObservableCollection<Domain.Windows.Window> ActiveWindows { get; set; }

        /// <summary>
        /// Gets or Sets all the selected active windows currently open on the system
        /// </summary>
        ObservableCollection<Domain.Windows.Window> SelectedActiveWindows { get; set; }
    }
}
