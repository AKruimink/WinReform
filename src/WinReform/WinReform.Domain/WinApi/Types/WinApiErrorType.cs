using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a class that represents all the posible error codes thrown by <see cref="FacilityType.Win32"/>
    /// <a href="https://docs.microsoft.com/en-gb/windows/win32/debug/system-error-codes?redirectedfrom=MSDN">List of <see cref="FacilityType.Win32"/> error codes</a>
    /// TODO: possibly clean this enum up from the error codes that cannot be thrown by the available native calls in this application
    /// </summary>
    internal enum  WinApiErrorType
    {
        /// <summary>
        /// Operation completed sucessfully
        /// </summary>
        Sucsess = 0,

        /// <summary>
        /// Incorrect function
        /// </summary>
        InvalidFunction = 1,

        /// <summary>
        /// System cannot find the file specified
        /// </summary>
        FileNotFound = 2,

        /// <summary>
        /// System cannot find the path specified
        /// </summary>
        PathNotFound = 3,

        /// <summary>
        /// System is unable to open the file
        /// </summary>
        TooManyOpenFiles = 4,

        /// <summary>
        /// Access is denied
        /// </summary>
        AccessDenied = 5,

        /// <summary>
        /// Handle is invalid
        /// </summary>
        InvalidHandle = 6,

        /// <summary>
        /// Not enough storage available to complete the operation
        /// </summary>
        OutOfMemory = 14,

        /// <summary>
        /// There are no more files
        /// </summary>
        NoMoreFiles = 18,

        /// <summary>
        /// Process cannot acces the file beccause it is being used by another process
        /// </summary>
        SharingViolation = 32,

        /// <summary>
        /// Parameter is incorrect
        /// </summary>
        InvalidParameter = 87,

        /// <summary>
        /// Data area passed to a system call is to small
        /// </summary>
        InsufficientBuffer = 122,

        /// <summary>
        /// Cannot nest calls to LoadModule
        /// </summary>
        NestingNotAllowed= 215,

        /// <summary>
        /// Illegal operation attempted on a registery key that has been marked for deletion
        /// </summary>
        KeyDeleted = 1018,

        /// <summary>
        /// Element not found
        /// </summary>
        NotFound = 1168,

        /// <summary>
        /// Found no match for the specified key in the index
        /// </summary>
        NoMatch = 1169,

        /// <summary>
        /// Invalid device was specified
        /// </summary>
        BadDevice = 1200,

        /// <summary>
        /// Operation was canceled by the user
        /// </summary>
        Cancelled = 1223,

        /// <summary>
        /// Cannot find window class
        /// </summary>
        CannotFindWndClass = 1407,

        /// <summary>
        /// Window class was already registered
        /// </summary>
        ClassAlreadyExists = 1410,

        /// <summary>
        /// Specified datatype is invalid
        /// </summary>
        InvalidDataType = 1804
    }
}
