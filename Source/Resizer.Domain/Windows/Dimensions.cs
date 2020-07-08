using System;
using System.Collections.Generic;
using System.Text;

namespace Resizer.Domain.Windows
{
    /// <summary>
    /// Defines a struct that represents the dimensions of a window
    /// </summary>
    public struct Dimensions
    {
        /// <summary>
        /// Gets or Sets the x coordinate of the top left corner
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Gets or Sets the y coordinate of the top left corner
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Gets or Sets the x coordinate of the bottom right corner
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Gets or Sets the y coordinate of the bottom right corner
        /// </summary>
        public int Bottom { get; set; }

        /// <summary>
        /// Gets the full width of the dimension
        /// </summary>
        public int Width => Right - Left;

        /// <summary>
        /// Gets the full height of the dimension
        /// </summary>
        public int Height => Bottom - Top;
    }
}
