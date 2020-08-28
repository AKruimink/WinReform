using System.Collections.Generic;

namespace WinReform.Gui.Resizer
{
    /// <summary>
    /// Defines a class that provides data and functionality for resizing of windows during design time
    /// </summary>
    public class ResizerDesignModel : IResizerViewModel
    {
        ///<inheritdoc/>
        public string NewWidth { get; set; }

        ///<inheritdoc/>
        public string NewHeight { get; set; }

        ///<inheritdoc/>
        public List<Domain.Windows.Window> SelectedWindows { get; set; } = new List<Domain.Windows.Window>();

        /// <summary>
        /// Create a new instance of <see cref="ResizerDesignModel"/>
        /// </summary>
        public ResizerDesignModel()
        {
            NewWidth = "1234";
            NewHeight = "Ïnvalid Input";
        }
    }
}
