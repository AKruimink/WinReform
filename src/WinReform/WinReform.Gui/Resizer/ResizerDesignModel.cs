using System.Collections.Generic;
using WinReform.Domain.WinApi;

namespace WinReform.Gui.Resizer
{
    /// <summary>
    /// Defines a class that provides data and functionality for resizing of windows during design time
    /// </summary>
    public class ResizerDesignModel : IResizerViewModel
    {
        ///<inheritdoc/>
        public Dictionary<string, Rect> Resolutions { get; }

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
            Resolutions = new Dictionary<string, Rect>()
            {
                { "640x480 (4/3)", new Rect() {Right = 640, Bottom = 480 } },
                { "720x480 (4/3)", new Rect() {Right = 720, Bottom = 480 } },
            };
            NewWidth = "1234";
            NewHeight = "Ïnvalid Input";
        }
    }
}
