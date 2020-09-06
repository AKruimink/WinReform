using System.Collections.Generic;
using WinReform.Domain.Displays;

namespace WinReform.Locator
{
    /// <summary>
    /// Represents a class that provides data and functionality for the relocation of windows
    /// </summary>
    public interface ILocatorViewModel
    {
        /// <summary>
        /// Gets or Sets the new horizontal location to be applied to all selected windows
        /// NOTE: if left empty the original horizontal location of the window(s) will be used
        /// </summary>
        string NewHorizontalLocation { get; set; }

        /// <summary>
        /// Gets or Sets The new vertical location to be applied to all selected windows
        /// NOTE: if left empty the original vertical location of the window(s) will be used
        /// </summary>
        string NewVerticalLocation { get; set; }

        /// <summary>
        /// Gets or Sets all available displays that the preset can be applied to
        /// </summary>
        List<Display> AvailableDisplays { get; set; }

        /// <summary>
        /// Gets or Sets the selected displat to apply the preset to
        /// </summary>
        Display SelectedDisplay { get; set; }

        /// <summary>
        /// <see cref="List{Domain.Windows.Window}"/> containing all currently selected windows
        /// </summary>
        List<Domain.Windows.Window> SelectedWindows { get; set; }
    }
}
