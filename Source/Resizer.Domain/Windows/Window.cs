using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;

namespace Resizer.Domain.Windows
{
    /// <summary>
    /// Defines a class that acts as model for active windows running on the system
    /// </summary>
    public class Window : IComparable<Window>
    {
        /// <summary>
        /// Gets or Sets the id of the window used as identification
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the window handle used to manipulate window information through the WinApi
        /// </summary>
        public IntPtr WindowHandle { get; set; }

        /// <summary>
        /// Gets or Sets the description of the application that owns the window
        /// <remarks>Defaults to an empty string</remarks>
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Gets or Sets the icon of the application that owns the window
        /// </summary>
        public Bitmap? Icon { get; set; }

        /// <summary>
        /// Gets or Sets the dimensions of the window
        /// </summary>
        public Dimensions Dimensions { get; set; }

        /// <summary>
        /// Gets the resolution of the application
        /// </summary>
        public string Resolution => $"{Dimensions.Width} x {Dimensions.Height}";

        /// <summary>
        /// Compares a object with the current object
        /// </summary>
        /// <param name="other"><see cref="Window"/> to compare to the current instance</param>
        /// <returns>Returns -1 if the object's arent equal, 0 if the objects are identical and 1 if the object are equal but have some differences in the assigned values</returns>
        public int CompareTo([AllowNull] Window other)
        {
            if (Id != other?.Id)
            {
                // Objects don't represent the same Window
                return -1;
            }

            foreach (var property in GetType().GetProperties())
            {
                var currentValue = property.GetValue(this);
                var otherValue = property.GetValue(other);

                if (currentValue == otherValue)
                {
                    // Object contains atleast 1 changed item
                    return 1;
                }
            }

            return 0;
        }
    }
}
