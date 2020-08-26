using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Gui.Locator
{
    /// <summary>
    /// Defines a class that provides data and functionality for the relocation of windows during design time
    /// </summary>
    public class LocatorDesignModel : ILocatorViewModel
    {
        ///<inheritdoc/>
        public string NewXAxis { get; set; }

        ///<inheritdoc/>
        public string NewYAxis { get; set; }

        ///<inheritdoc/>
        public List<Domain.Windows.Window> SelectedWindows { get; set; } = new List<Domain.Windows.Window>();

        /// <summary>
        /// Create a new instance of the <see cref="LocatorDesignModel"/>
        /// </summary>
        public LocatorDesignModel()
        {
            NewXAxis = 10.ToString();
            NewYAxis = "Invalid Type";
        }
    }
}
