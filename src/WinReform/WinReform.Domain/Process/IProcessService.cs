using System.Collections.Generic;

namespace WinReform.Domain.Process
{
    /// <summary>
    /// Represents a class that acts as a service for managing active processes
    /// </summary>
    public interface IProcessService
    {
        /// <summary>
        /// Gets a list of all active processes currently running on the system
        /// </summary>
        /// <returns>Returns <see cref="List{System.Diagnostics.Process}"/> of all active processes</returns>
        List<System.Diagnostics.Process> GetActiveProcesses();
    }
}
