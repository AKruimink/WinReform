namespace Resizer.Gui.Window
{
    /// <summary>
    /// Represents a class that provides general application information
    /// </summary>
    public interface IWindowViewModel
    {
        /// <summary>
        /// Gets the current application version
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets or Sets the visibility state of the menu
        /// </summary>
        bool MenuIsOpen { get; set; }

        /// <summary>
        /// Gets or Sets the state that defines if the window should be minized when closed
        /// </summary>
        bool MinimizeOnClose { get; set; }
    }
}
