using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Gui.Locator
{
    public class LocationPreset
    {
        public HorizontalLocationType HorizontalLocation { get; set; }

        public VerticalLocationType VerticalLocation { get; set; }
    }

    public enum HorizontalLocationType
    {
        Left,
        Center,
        Right
    }

    public enum VerticalLocationType
    {
        Top,
        Center,
        Bottom
    }
}
