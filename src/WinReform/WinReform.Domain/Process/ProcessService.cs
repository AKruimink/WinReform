using System.Collections.Generic;
using System.Linq;

namespace WinReform.Domain.Process
{
    /// <summary>
    /// Defines a class that acts as a service for managing active processes
    /// </summary>
    public class ProcessService : IProcessService
    {
        /// <inheritdoc/>
        public List<System.Diagnostics.Process> GetActiveProcesses() => System.Diagnostics.Process.GetProcesses().ToList();
    }
}
