using WinReform.Domain.Windows;
using System.Collections.Generic;

namespace WinReform.Domain.Tests.Windows.Mocks
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
        /// Indicates if the <see cref="SetResizableBorder(Window)"/> has been called
        /// </summary>
        public bool SetResizableBorderCalled { get; set; } = false;

        /// <summary>
        /// Value to be returned when <see cref="SetResizableBorder(Window)"/> is called
        /// <remarks>Defaults to <see langword="false"/></remarks>
        /// </summary>
        public bool SetResizableBorderReturnValue { get; set; } = false;

        /// <summary>
        /// Indicates if the <see cref="RedrawWindow(Window)"/> has been called
        /// </summary>
        public bool RedrawWindowCalled { get; set; } = false;

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

        ///<inheritdoc/>
        public bool SetResizableBorder(Window window)
        {
            SetResizableBorderCalled = true;
            return SetResizableBorderReturnValue;
        }

        ///<inheritdoc/>
        public void RedrawWindow(Window window)
        {

        }
    }
}
