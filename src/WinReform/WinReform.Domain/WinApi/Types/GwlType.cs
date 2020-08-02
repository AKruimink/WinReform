using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a class that represents all possible GWl options for the Get and Set WindowLongPtr method
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongptra">List of Gwl values</a>
    /// </summary>
    public enum GwlType
    {
        /// <summary>
        /// Retrieve the extended window styles
        /// </summary>
        ExStyle = -20,

        /// <summary>
        /// Retrieve a handle to the application instance
        /// </summary>
        HInstance = -6,

        /// <summary>
        /// Retrieve a handle to the parent window (if there is one)
        /// </summary>
        HwndParanet = -8,

        /// <summary>
        /// Retrieve the identifier of the window
        /// </summary>
        Id = -12,

        /// <summary>
        /// Retrieves the window styles
        /// </summary>
        Style = -16,

        /// <summary>
        /// Retrieves the user data accociated with the window
        /// </summary>
        UserData = -21,

        /// <summary>
        /// Retrieves the pointer to the window procedure, or a handle representing the pointer to the window procedure
        /// </summary>
        WindProc = -4,
    }
}
