using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Domain.Process;

namespace WinReform.Domain.Tests.Process.Mocks
{
    /// <summary>
    /// Mock implementation of <see cref="IProcessService"/>
    /// </summary>
    public class ProcessServiceMock : IProcessService
    {
        /// <summary>
        /// <see cref="List{System.Diagnostics.Process}"/> to be used during the mock
        /// </summary>
        public List<System.Diagnostics.Process> ProcessesToReturn { get; set; } = new List<System.Diagnostics.Process>();

        ///<inheritdoc/>
        public List<System.Diagnostics.Process> GetActiveProcesses() => ProcessesToReturn;
    }
}
