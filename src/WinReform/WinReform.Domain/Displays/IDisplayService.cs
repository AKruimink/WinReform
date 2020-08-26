using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Domain.Displays
{
    /// <summary>
    /// Represents a class that acts as a service for managing displays
    /// </summary>
    public interface IDisplayService
    {
        /// <summary>
        /// Gets all available displays
        /// </summary>
        /// <returns>Returns <see cref="List{Display}"/> containing all available displays</returns>
        List<Display> GetDisplays();
    }
}
