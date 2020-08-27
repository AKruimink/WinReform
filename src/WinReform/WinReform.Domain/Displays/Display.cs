using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using WinReform.Domain.WinApi;

namespace WinReform.Domain.Displays
{
    /// <summary>
    /// Defines a class that acts as model for a display
    /// </summary>
    public class Display : IEquatable<Display>, IComparable<Display>
    {
        /// <summary>
        /// Gets or Sets the id of the display used as identification
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the display handle used to manipulate monitor information through the WinApi
        /// </summary>
        public IntPtr DisplayHandle { get; set; }

        /// <summary>
        /// Gets or Sets an indication if the display is the primary display
        /// </summary>
        public bool Primary { get; set; }

        /// <summary>
        /// Gets or Sets the available work area of the display
        /// </summary>
        public Rect WorkArea { get; set; }

        /// <summary>
        /// Gets the display number as readable string
        /// </summary>
        public string DisplayNumber => $"Display {Id}";

        /// <summary>
        /// Comapares the current <see cref="Display"/> to a given <see cref="Display"/>
        /// </summary>
        /// <param name="other"><see cref="Display"/> to compare to the current instance</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="Display"/>, otherwise returns <see langword="false"/></returns>
        public bool Equals([AllowNull] Display other)
            => other?.Id == Id
            && other?.DisplayHandle == DisplayHandle
            && other?.Primary == Primary
            && other?.WorkArea == WorkArea;

        /// <summary>
        /// Compare if the current <see cref="Display"/> represents the same item as a given <see cref="Display"/>
        /// </summary>
        /// <param name="other"><see cref="Display"/> to compare against</param>
        /// <returns>Returns <see langword="true"/> if the <see cref="Display"/> represent the same <see cref="Display"/>, otherwise returns <see langword="false"/></returns>
        public int CompareTo([AllowNull] Display other)
        {
            if (other?.Id == Id)
            {
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// Comapares the current <see cref="Display"/> to a given <see cref="object"/>
        /// </summary>
        /// <param name="obj"><see cref="object"/> to compare to the current instance</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="object"/>, otherwise returns <see langword="false"/></returns>
        public override bool Equals(object? obj)
            => obj is Display display
            && Equals(display);

        /// <summary>
        /// Gets the hashCode of the <see cref="Display"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="Display"/></returns>
        public override int GetHashCode()
            => (Id, DisplayHandle, Primary, WorkArea).GetHashCode();
    }
}
