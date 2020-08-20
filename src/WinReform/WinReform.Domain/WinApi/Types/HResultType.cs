namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a class that represents all the possible HResult values
    /// <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/705fb797-2175-4a90-b5a3-3918024b10b8">HResult values</a>
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/seccrypto/common-hresult-values">Common HResult values</a>
    /// TODO: possibly clean this enum up of the HResult values that cannot occure by the available native calls in this application
    /// </summary>
    internal enum HResultType : uint
    {
        /// <summary>
        /// Operation successful
        /// </summary>
        Ok = 0x00000000,

        /// <summary>
        /// Data necessary to complete the oparation is not yet available
        /// </summary>
        Pending = 0x8000000A,

        /// <summary>
        /// Not implemented
        /// </summary>
        NotImplemented = 0x80004001,

        /// <summary>
        /// No such interface supported
        /// </summary>
        NoInterface = 0x80004002,

        /// <summary>
        /// Invalid pointer
        /// </summary>
        Pointer = 0x80004003,

        /// <summary>
        /// Operation aborted
        /// </summary>
        Abort = 0x80004004,

        /// <summary>
        /// Unspecified error
        /// </summary>
        Fail = 0x80004005,

        /// <summary>
        /// Catastrophic failure
        /// </summary>
        Unexpected = 0x8000FFFF,

        /// <summary>
        /// Unable to perform requested operation
        /// </summary>
        InvalidFunction = 0x80030001,

        /// <summary>
        /// This implementation does not take advises
        /// </summary>
        AdviceNotSupported = 0x80040003,

        /// <summary>
        /// Invalid FORMATETC structure
        /// </summary>
        Formatetc = 0x80040064,

        /// <summary>
        /// Invalid TYMED structure
        /// </summary>
        Tymed = 0x80040069,

        /// <summary>
        /// Invalid clipboard format
        /// </summary>
        ClipFormat = 0x8004006A,

        /// <summary>
        /// Invalid aspects
        /// </summary>
        DvAspect = 0x8004006B,

        /// <summary>
        /// Class not registered
        /// </summary>
        ClassNotRegistered = 0x80040154,

        /// <summary>
        /// General access denied error
        /// </summary>
        AccessDenied = 0x80070005,

        /// <summary>
        /// The server does not have enough memory for the new channel
        /// </summary>
        OutOfMemory = 0x8007000E,

        /// <summary>
        /// One or more arguments are invalid
        /// </summary>
        InvalidArguments = 0x80070057,
    }
}
