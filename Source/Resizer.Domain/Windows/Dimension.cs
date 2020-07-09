using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Resizer.Domain.Windows
{
    /// <summary>
    /// Defines a struct that represents the dimension of a window
    /// </summary>
    public struct Dimension : IEquatable<Dimension>
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

        /// <summary>
        /// Comapares the current <see cref="Dimension"/> to a given <see cref="Dimension"/>
        /// </summary>
        /// <param name="other"><see cref="Dimension"/> to compare to the current instance</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="Dimension"/>, otherwise returns <see langword="false"/></returns>
        public bool Equals([AllowNull] Dimension other) 
            => other.Left == Left
            && other.Top == Top
            && other.Right == Right
            && other.Bottom == Bottom;

        /// <summary>
        /// Comapares the current <see cref="Dimension"/> to a given <see cref="object"/>
        /// </summary>
        /// <param name="obj"><see cref="object"/> to compare to the current instance</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="object"/>, otherwise returns <see langword="false"/></returns>
        public override bool Equals(object? obj) 
            => obj is Dimension dimensions && Equals(dimensions);

        /// <summary>
        /// Gets the hashCode of the <see cref="Dimension"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="Dimension"/></returns>
        public override int GetHashCode() => (Left, Top, Right, Bottom).GetHashCode();

        public static bool operator == (Dimension d1, Dimension d2)
        {
            return d1.Equals(d2);
        }

        public static bool operator != (Dimension d1, Dimension d2)
        {
            return !(d1 == d2);
        }
    }
}
