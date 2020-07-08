using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Resizer.Domain.Windows
{
    /// <summary>
    /// Represents a class that acts as a service for managing active windows
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Gets the ative windows running on the system
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{Window}"/> containing all active windows on the system</returns>
        IEnumerable<Window> GetActiveWindows();
    }
}
