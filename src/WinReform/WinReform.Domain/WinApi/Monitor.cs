using System;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a struct that represents a monitor
    /// </summary>
    public struct Monitor : IEquatable<Monitor>
    {
        /// <summary>
        /// Gets or Sets tje size of <see cref="Monitor"/> in bytes
        /// NOTE: Needs to be set as GetMonitorInfo uses it to determine the type of <see cref="struct"/> passed
        /// </summary>
        public uint Size { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="Rect"/> containing the full resolution of the monitor
        /// </summary>
        public Rect MonitorSize { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="Rect"/> containing the full working area
        /// NOTE: working area is <see cref="MonitorSize"/> minus the taskbar and additional desktop toolbars
        /// </summary>
        public Rect WorkArea { get; set; }

        /// <summary>
        /// Gets or Sets the flags that represent the attributes of the monitor
        /// NOTE: only the MONITORINFOF_PRIMARY exists, so when set the monitor is set as the primary monitor
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// Gets or Sets the window handle that represents the <see cref="Monitor"/>
        /// </summary>
        public IntPtr MonitorHandle { get; set; }

        /// <summary>
        /// Comapres the current <see cref="Monitor"/> to a given <see cref="Monitor"/>
        /// </summary>
        /// <param name="other"><see cref="Monitor"/> to compare against the current instance to</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="Monitor"/>, otherwise returns <see langword="false"/></returns>
        public bool Equals(Monitor other) =>
            other.Size == Size
            && other.MonitorSize == MonitorSize
            && other.WorkArea == WorkArea
            && other.Flags == Flags
            && other.MonitorHandle == MonitorHandle;

        /// <summary>
        /// Compares the current <see cref="Monitor"/> to a given <see cref="object"/>
        /// </summary>
        /// <param name="obj"><see cref="object"/> to compare against the current instance to</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="object"/>, otherwise returns <see langword="false"/></returns>
        public override bool Equals(object? obj)
            => obj is Monitor monitor
            && Equals(monitor);

        /// <summary>
        /// Gets the hashCode of the the current <see cref="Monitor"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="Monitor"/></returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Compare two instances of <see cref="Monitor"/> for equality
        /// </summary>
        /// <param name="rect1">First <see cref="Monitor"/> to compare</param>
        /// <param name="rect2">Second <see cref="Monitor"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the two instances are equal, otherwise returns <see langword="false"/></returns>
        public static bool operator ==(Monitor monitor1, Monitor monitor2)
        {
            return monitor1.Equals(monitor2);
        }

        /// <summary>
        /// Compare two instances of <see cref="Monitor"/> for unequality
        /// </summary>
        /// <param name="rect1">First <see cref="Monitor"/> to compare</param>
        /// <param name="rect2">Second <see cref="Monitor"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the two instances aren't equal, otherwise returns <see langword="false"/></returns>
        public static bool operator !=(Monitor monitor1, Monitor monitor2)
        {
            return !monitor1.Equals(monitor2);
        }
    }
}
