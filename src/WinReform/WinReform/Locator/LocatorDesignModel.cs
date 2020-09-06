using System.Collections.Generic;
using WinReform.Domain.Displays;

namespace WinReform.Locator
{
    /// <summary>
    /// Defines a class that provides data and functionality for the relocation of windows during design time
    /// </summary>
    public class LocatorDesignModel : ILocatorViewModel
    {
        ///<inheritdoc/>
        public string NewHorizontalLocation { get; set; }

        ///<inheritdoc/>
        public string NewVerticalLocation { get; set; }

        ///<inheritdoc/>
        public List<Domain.Windows.Window> SelectedWindows { get; set; } = new List<Domain.Windows.Window>();

        ///<inheritdoc/>
        public List<Display> AvailableDisplays { get; set; } = new List<Display>();

        ///<inheritdoc/>
        public Display SelectedDisplay { get; set; } = new Display();

        /// <summary>
        /// Create a new instance of the <see cref="LocatorDesignModel"/>
        /// </summary>
        public LocatorDesignModel()
        {
            NewHorizontalLocation = 10.ToString();
            NewVerticalLocation = "Invalid Type";
        }
    }
}
