using Resizer.Domain.Windows;
using System.Collections.Generic;

namespace Resizer.Domain.Tests.Windows.Mocks
{
    /// <summary>
    /// Mock implementation of <see cref="IWindowService"/>
    /// </summary>
    public class WindowServiceMock : IWindowService
    {
        /// <summary>
        /// Indicates if the <see cref="GetActiveWindows"/> has been called
        /// </summary>
        public bool GetActiveWindowsCalled { get; set; } = false;

        /// <summary>
        /// <see cref="List{Window}"/> to be returned by <see cref="GetActiveWindows"/>
        /// </summary>
        public List<Window> WindowsToReturn { get; set; } = new List<Window>();

        ///<inheritdoc/>
        public IEnumerable<Window> GetActiveWindows()
        {
            GetActiveWindowsCalled = true;
            return WindowsToReturn;
        }
    }
}