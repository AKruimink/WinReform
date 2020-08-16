using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a struct that represents a rectangle
    /// </summary>
    public struct Rect : IEquatable<Rect>
    {
        /// <summary>
        /// Gets a new empty <see cref="Rect"/>
        /// </summary>
        public static readonly Rect Empty = new Rect();

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
        /// Create a new instance of <see cref="Rect"/>
        /// </summary>
        /// <param name="left">The x coordinate of the top left corner</param>
        /// <param name="top">The y coordinate of the top left corner</param>
        /// <param name="right">The x coordinate of the bottom right corner</param>
        /// <param name="bottom">The y coordinate of the bottom right corner</param>
        public Rect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Create a new instance of <see cref="Rect"/>
        /// </summary>
        /// <param name="src"><see cref="Rect"/> containing the coordinates to copy</param>
        public Rect(Rect src)
        {
            Left = src.Left;
            Top = src.Top;
            Right = src.Right;
            Bottom = src.Bottom;
        }

        /// <summary>
        /// Checks if the current <see cref="Rect"/> is empty
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the current istance is empty, otherwise returns <see langword="false"/></returns>
        public bool IsEmpty => Left >= Right || Top >= Bottom;

        /// <summary>
        /// Gets a new <see cref="Rect"/>  that is large enough to contain both given rectangles
        /// </summary>
        /// <param name="rect1">First <see cref="Rect"/> to use for calculating a new rectangle size</param>
        /// <param name="rect2">Second <see cref="Rect"/> to use for calculating the new rectangle size</param>
        /// <returns>Returns a new <see cref="Rect"/> containing that size that fits both given rectangles</returns>
        public static Rect Union(Rect rect1, Rect rect2) => new Rect
        {
            Left = Math.Min(rect1.Left, rect2.Left),
            Top = Math.Min(rect1.Top, rect2.Top),
            Right = Math.Max(rect1.Right, rect2.Right),
            Bottom = Math.Max(rect1.Bottom, rect2.Bottom)
        };

        /// <summary>
        /// Comapres the current <see cref="Rect"/> to a given <see cref="Rect"/>
        /// </summary>
        /// <param name="other"><see cref="Rect"/> to compare against the current instance to</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="Rect"/>, otherwise returns <see langword="false"/></returns>
        public bool Equals(Rect other) =>
            other.Left == Left
            && other.Top == Top
            && other.Right == Right
            && other.Bottom == Bottom;

        /// <summary>
        /// Compares the current <see cref="Rect"/> to a given <see cref="object"/>
        /// </summary>
        /// <param name="obj"><see cref="object"/> to compare against the current instance to</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="object"/>, otherwise returns <see langword="false"/></returns>
        public override bool Equals(object? obj) 
            => obj is Rect rect 
            && Equals(rect);

        /// <summary>
        /// Gets the hashCode of the the current <see cref="Rect"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="Rect"/></returns>
        public override int GetHashCode() 
            => (Left, Top, Right, Bottom).GetHashCode();

        /// <summary>
        /// Get the string representation of the current <see cref="Rect"/>
        /// </summary>
        /// <returns>Returns a <see cref="string"/> containing the current rectangle dimensions</returns>
        public override string ToString() 
            => "RECT { left : " + Left + " / top : " + Top + " / right : " + Right + " / bottom : " + Bottom + " }";

        /// <summary>
        /// Compare two instances of <see cref="Rect"/> for equality
        /// </summary>
        /// <param name="rect1">First <see cref="Rect"/> to compare</param>
        /// <param name="rect2">Second <see cref="Rect"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the two instances are equal, otherwise returns <see langword="false"/></returns>
        public static bool operator ==(Rect rect1, Rect rect2)
        {
            return rect1.Equals(rect2);
        }

        /// <summary>
        /// Compare two instances of <see cref="Rect"/> for unequality
        /// </summary>
        /// <param name="rect1">First <see cref="Rect"/> to compare</param>
        /// <param name="rect2">Second <see cref="Rect"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the two instances aren't equal, otherwise returns <see langword="false"/></returns>
        public static bool operator !=(Rect rect1, Rect rect2)
        {
            return !rect1.Equals(rect2);
        }
    }
}
