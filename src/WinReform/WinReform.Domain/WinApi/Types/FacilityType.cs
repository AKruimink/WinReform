namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a class that represents all possible facilities of the thrown errors
    /// <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">List of facilities</a>
    /// TODO: possibly clean this enum up from the facilities that cannot be thrown by the available native calls in this application
    /// </summary>
    internal enum FacilityType
    {
        /// <summary>
        /// The default facility code
        /// </summary>
        Null = 0,

        /// <summary>
        /// Source of the error code is an RPC subsystem
        /// </summary>
        Rpc = 1,

        /// <summary>
        /// Source of the error code is a COM dispatch
        /// </summary>
        Dispatch = 2,

        /// <summary>
        /// Source of the error code is OLE storage
        /// </summary>
        Storage = 3,

        /// <summary>
        /// Source of the error code is Com/OLE interface management
        /// </summary>
        Itf = 4,

        /// <summary>
        /// Region is reserved to map undecorated error codes into HRESULT
        /// </summary>
        Win32 = 7,

        /// <summary>
        /// Source of the error code is the windows subsystem
        /// </summary>
        Windows = 8,

        /// <summary>
        /// Source of the error code is the control mechanism
        /// </summary>
        Control = 10,
    }
}
