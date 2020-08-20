using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace WinReform.Domain.WinApi
{
    /// <summary>
    /// Defines a struct that represents common Win32 status codes
    /// <remarks>Credit: ControlzEx </remarks>
    /// Link: https://github.com/ControlzEx/ControlzEx/blob/develop/src/ControlzEx/Microsoft.Windows.Shell/Standard/ErrorCodes.cs
    /// </summary>
    internal struct WinApiError
    {
        /// <summary>
        /// Gets the <see cref="WinApiErrorType"/>
        /// </summary>
        public WinApiErrorType Error { get; }

        /// <summary>
        /// Create a new instance of <see cref="WinApiError"/>
        /// </summary>
        /// <param name="errorCode"><see cref="int"/> of the error code thrown during the WinApi call</param>
        public WinApiError(int errorCode)
        {
            if (!Enum.IsDefined(typeof(WinApiErrorType), errorCode))
            {
                throw new InvalidOperationException($"{errorCode} is not an underlying value of {typeof(WinApiErrorType)}");
            }
            Error = (WinApiErrorType)errorCode;
        }

        /// <summary>
        /// Create a new instance of <see cref="WinApiError"/>
        /// </summary>
        /// <param name="errorType"><see cref="WinApiErrorType"/> of the thrown error during the WinApi call</param>
        public WinApiError(WinApiErrorType errorType)
        {
            Error = errorType;
        }

        /// <summary>
        /// Gets the last error thrown by the unmanaged functions
        /// </summary>
        /// <returns>Returns <see cref="WinApiError"/> of the last thrown error</returns>
        public static WinApiError GetLastError() => new WinApiError(Marshal.GetLastWin32Error());

        /// <summary>
        /// Compares the current <see cref="WinApiError"/> to a given <see cref="object"/>
        /// </summary>
        /// <param name="obj"><see cref="object"/> to compare against the current instance to</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="object"/>, otherwise returns <see langword="false"/></returns>
        public override bool Equals(object? obj) => obj is WinApiError winApiError && Error == winApiError.Error;

        /// <summary>
        /// Gets the hashCode of the the current <see cref="WinApiError"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="WinApiError"/></returns>
        public override int GetHashCode() => Error.GetHashCode();

        /// <summary>
        /// Compare two instances of <see cref="WinApiError"/> for equality
        /// </summary>
        /// <param name="winApiError1">First <see cref="WinApiError"/> to compare</param>
        /// <param name="winApiError2">Second <see cref="WinApiError"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the two instances are equal, otherwise returns <see langword="false"/></returns>
        public static bool operator ==(WinApiError winApiError1, WinApiError winApiError2)
        {
            return winApiError1.Equals(winApiError2);
        }

        /// <summary>
        /// Compare two instances of <see cref="WinApiError"/> for unequality
        /// </summary>
        /// <param name="winApiError1">First <see cref="WinApiError"/> to compare</param>
        /// <param name="winApiError2">Second <see cref="WinApiError"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the two instances aren't equal, otherwise returns <see langword="false"/></returns>
        public static bool operator !=(WinApiError winApiError1, WinApiError winApiError2)
        {
            return !winApiError1.Equals(winApiError2);
        }

        /// <summary>
        /// Convert a given instance of <see cref="WinApiError"/> to an instance of <see cref="HResult"/>
        /// </summary>
        /// <param name="winApiError"><see cref="WinApiError"/> to be converted to <see cref="HResult"/></param>
        public static explicit operator HResult(WinApiError winApiError)
        {
            if (winApiError.Error <= 0)
            {
                return new HResult((uint)winApiError.Error);
            }
            return HResult.Create(true, FacilityType.Win32, (int)winApiError.Error & 0x0000FFFF);
        }
    }

    /// <summary>
    /// Defines a struct that represents the HResult status codes
    /// <remarks>Credit: ControlzEx </remarks>
    /// Link: https://github.com/ControlzEx/ControlzEx/blob/develop/src/ControlzEx/Microsoft.Windows.Shell/Standard/ErrorCodes.cs
    /// </summary>
    internal struct HResult
    {
        /// <summary>
        /// Gets the <see cref="HResultType"/> of this HResult
        /// </summary>
        private HResultType Result { get; }

        /// <summary>
        /// Gets the <see cref="FacilityType"/> of this HResult
        /// </summary>
        private FacilityType Facility => (FacilityType)(((int)Result >> 16) & 0x1fff);

        /// <summary>
        /// Gets the Hresult code
        /// </summary>
        private int Code => ((int)Result & 0xfff);

        /// <summary>
        /// Create a new instance of <see cref="HResult"/>
        /// </summary>
        /// <param name="errorCode"></param>
        public HResult(uint result)
        {
            if (!Enum.IsDefined(typeof(HResultType), result))
            {
                throw new InvalidOperationException($"{result} is not an underlying value of {typeof(HResultType)}");
            }
            Result = (HResultType)result;
        }

        /// <summary>
        /// Create a new instance of <see cref="HResult"/>
        /// </summary>
        /// <param name="errorCode"></param>
        public HResult(HResultType result)
        {
            Result = result;
        }

        /// <summary>
        /// Indicates if the current execution was sucsessfull
        /// </summary>
        public bool Succeeded => (int)Result >= 0;

        /// <summary>
        /// Indicates if the current execution failed
        /// </summary>
        public bool Failed => (int)Result < 0;

        /// <summary>
        /// Throws an exception if the unmanaged method failed to execute
        /// </summary>
        /// <param name="message">Optional message to about the exception</param>
        public void ThrowIfFailed(string? message = null)
        {
            if (Failed)
            {
                if (string.IsNullOrEmpty(message))
                {
                    message = ToString();
                }
#if DEBUG
                else
                {
                    message += $"({ToString()})";
                }
#endif
                // We want to convert HResult to a more appropriate exeption type than ComException.
                // Marshal.ThrowExceptionForHr does this for us, but the general call users GetErrorInfo
                // If sets it ignores the HResult that we provided, this works for the first time but fails on the second.
                // To avoid this we explicitly use the overload that ignores the IErrorInfo
                var ex = Marshal.GetExceptionForHR((int)Result, new IntPtr(-1));

                // If we get nothing better then ComExceptrion from the Marshal
                // we try and attempt to do it better ourself
                if (ex?.GetType() == typeof(COMException))
                {
                    ex = Facility switch
                    {
                        FacilityType.Win32 => new Win32Exception(Code, message),
                        _ => new COMException(message, (int)Result),
                    };
                }
                else
                {
                    // We use reflection in a throw call, this might be a bad idea in the long run.
                    // If we are throwing an exception i assume that its ok to take some time to give it back.
                    var cons = ex?.GetType().GetConstructor(new[] { typeof(string) });
                    if (cons != null)
                    {
                        ex = cons.Invoke(new object[] { message ?? "" }) as Exception;
                    }
                }
                throw ex ?? new Exception(message ?? "");
            }
        }

        /// <summary>
        /// Checks if an error occured and throwns and <see cref="Exception"/> if it did
        /// </summary>
        public static void ThrowLastError()
        {
            ((HResult)WinApiError.GetLastError()).ThrowIfFailed();
        }

        /// <summary>
        /// Create a new <see cref="HResult"/>
        /// <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HResult code build</a>
        /// </summary>
        /// <param name="severe">Indicates if the <see cref="HResult"/> is a failure, otherwise indicates sucsess</param>
        /// <param name="facility"><see cref="FacilityType"/> of the source of the error</param>
        /// <param name="code">The error code thrown</param>
        /// <returns>Returns <see cref="HResult"/> of the result</returns>
        public static HResult Create(bool severe, FacilityType facility, int code)
        {
            return new HResult((uint)((severe ? (1 << 31) : 0) | ((int)facility << 16) | code));
        }

        /// <summary>
        /// Gets the string representation of <see cref="HResult"/>
        /// </summary>
        /// <returns>Returns <see cref="string"/> that represents the current <see cref="HResult"/></returns>
        public override string ToString()
        {
            // set the default return value to a hex representation of the HResult (0x########)
            var returnValue = string.Format(CultureInfo.InvariantCulture, "0x{0:X8}", (uint)Result);

            // Try to get the name of the current HResult
            var resultName = Enum.GetName(typeof(HResultType), Result);
            if (resultName != null)
            {
                returnValue = resultName;
            }

            return returnValue;
        }

        /// <summary>
        /// Compares the current <see cref="HResult"/> to a given <see cref="object"/>
        /// </summary>
        /// <param name="obj"><see cref="object"/> to compare against the current instance to</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="object"/>, otherwise returns <see langword="false"/></returns>
        public override bool Equals(object? obj) => obj is HResult hResult && Result == hResult.Result;

        /// <summary>
        /// Gets the hashCode of the the current <see cref="HResult"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="WinApiError"/></returns>
        public override int GetHashCode() => Result.GetHashCode();

        /// <summary>
        /// Compare two instances of <see cref="HResult"/> for equality
        /// </summary>
        /// <param name="hResult1">First <see cref="HResult"/> to compare</param>
        /// <param name="hResult2">Second <see cref="HResult"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the two instances are equal, otherwise returns <see langword="false"/></returns>
        public static bool operator ==(HResult hResult1, HResult hResult2)
        {
            return hResult1.Equals(hResult2);
        }

        /// <summary>
        /// Compare two instances of <see cref="HResult"/> for unequality
        /// </summary>
        /// <param name="hResult1">First <see cref="HResult"/> to compare</param>
        /// <param name="hResult2">Second <see cref="HResult"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the two instances aren't equal, otherwise returns <see langword="false"/></returns>
        public static bool operator !=(HResult hResult1, HResult hResult2)
        {
            return !hResult1.Equals(hResult2);
        }
    }
}
