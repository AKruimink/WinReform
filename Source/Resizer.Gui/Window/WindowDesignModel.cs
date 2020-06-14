namespace Resizer.Gui.Window
{
    /// <summary>
    /// Defines a class that provides design time application information
    /// </summary>
    public class WindowDesignModel
    {
        /// <summary>
        /// Gets and Sets the version of the application
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets and Sets the state of the menu
        /// </summary>
        public bool MenuIsOpen { get; set; }

        /// <summary>
        /// Create a new instance of the <see cref="WindowDesignModel"/>
        /// </summary>
        public WindowDesignModel()
        {
            Version = $"v0.0.0";
            MenuIsOpen = true;
        }
    }
}
