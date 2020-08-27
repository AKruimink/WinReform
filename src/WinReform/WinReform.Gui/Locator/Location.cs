using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Gui.Locator
{
    /// <summary>
    /// Defines a class that represents a preset location on a display
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets <see cref="Dictionary{string, Location}"/> containing all location presets
        /// </summary>
        public static Dictionary<string, Location> Locations { get; } = new Dictionary<string, Location>()
        {
            { "Left-Top", new Location(HorizontalLocationType.Left, VerticalLocationType.Top) },
            { "Left-Center", new Location(HorizontalLocationType.Left, VerticalLocationType.Center) },
            { "Left-Bottom", new Location(HorizontalLocationType.Left, VerticalLocationType.Bottom) },
            { "Center-Top", new Location(HorizontalLocationType.Center, VerticalLocationType.Top) },
            { "Center", new Location(HorizontalLocationType.Center, VerticalLocationType.Center) },
            { "Center-Bottom", new Location(HorizontalLocationType.Center, VerticalLocationType.Bottom) },
            { "Right-Top", new Location(HorizontalLocationType.Right, VerticalLocationType.Top) },
            { "Right-Center", new Location(HorizontalLocationType.Right, VerticalLocationType.Center) },
            { "Right-Bottom", new Location(HorizontalLocationType.Right, VerticalLocationType.Bottom) }
        };

        /// <summary>
        /// Gets the <see cref="HorizontalLocationType"/> of this location
        /// </summary>
        public HorizontalLocationType HorizontalLocation { get; }

        /// <summary>
        /// Gets the <see cref="VerticalLocationType"/> of this location
        /// </summary>
        public VerticalLocationType VerticalLocation { get; }

        /// <summary>
        /// Create a new instance of <see cref="Location"/>
        /// </summary>
        /// <param name="horizontalLocation"><see cref="HorizontalLocationType"/> of the horizontal location</param>
        /// <param name="verticalLocation"><see cref="VerticalLocationType"/> of the vertical location</param>
        public Location(HorizontalLocationType horizontalLocation, VerticalLocationType verticalLocation)
        {
            HorizontalLocation = horizontalLocation;
            VerticalLocation = verticalLocation;
        }
    }

    /// <summary>
    /// Defines a class that represents all possible window location on the horizontal axis of a display
    /// </summary>
    public enum HorizontalLocationType
    {
        /// <summary>
        /// Window will be located to the left side of the display
        /// </summary>
        Left,

        /// <summary>
        /// Window will be horizontally centered on the display
        /// </summary>
        Center,

        /// <summary>
        /// Window will be located to the right side of the display
        /// </summary>
        Right
    }

    /// <summary>
    /// Defines a class that represents all possible window location on the vertical axis of a display
    /// </summary>
    public enum VerticalLocationType
    {
        /// <summary>
        /// Window will be located to the top of the display
        /// </summary>
        Top,

        /// <summary>
        /// Window will be vertically centered on the display
        /// </summary>
        Center,

        /// <summary>
        /// Window will be located to the bottom of the display
        /// </summary>
        Bottom
    }
}
