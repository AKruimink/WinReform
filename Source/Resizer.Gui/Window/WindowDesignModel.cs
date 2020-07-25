namespace Resizer.Gui.Window
{
    /// <summary>
    /// Defines a class that provides design time application information
    /// </summary>
    public class WindowDesignModel : IWindowViewModel
    {
        ///<inheritdoc/>
        public string Version { get; set; }

        ///<inheritdoc/>
        public bool MenuIsOpen { get; set; }

        ///<inheritdoc/>
        public bool MinimizeOnClose { get; set; }

        /// <summary>
        /// Create a new instance of the <see cref="WindowDesignModel"/>
        /// </summary>
        public WindowDesignModel()
        {
            Version = $"v0.0.0";
            MenuIsOpen = true;
            MinimizeOnClose = false;
        }
    }
}
