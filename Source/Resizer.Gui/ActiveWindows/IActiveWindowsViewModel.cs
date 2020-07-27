using System.Collections.ObjectModel;
using System.ComponentModel;

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

        /// <summary>
        /// Gets or Sets the filter applied to the <see cref="ActiveWindows"/> collection used by <see cref="FilteredActiveWindows"/>
        /// </summary>
        string WindowFilter { get; set; }

        /// <summary>
        /// Gets or Sets a filtered list of all <see cref="ActiveWindows"/>
        /// </summary>
        ICollectionView FilteredActiveWindows { get; set; }
    }
}
