namespace WinReform.Domain.Tests.Mocks
{
    /// <summary>
    /// Defines a mock implementation of a delegate handler
    /// </summary>
    public class DelegateHandlerMock
    {
        /// <summary>
        /// Indicates if a command can be executed at this time
        /// </summary>
        public bool CanExecuteReturnValue { get; set; } = true;

        /// <summary>
        /// Argument passed that is normaly used to determine if a command can be executed
        /// </summary>
        public object? CanExecuteParameter { get; set; }

        /// <summary>
        /// The argument passed to the delegate during command execution
        /// </summary>
        public object? ExecuteParameter { get; set; }

        /// <summary>
        /// The argument passed to any of the methods
        /// </summary>
        public string? MethodParameter { get; set; }

        /// <summary>
        /// Checks if the current command can be executed
        /// </summary>
        /// <param name="parameter">The paramater expected to be passed</param>
        /// <returns>Returns the value set in <see cref="CanExecuteReturnValue"/></returns>
        public bool CanExecute(object parameter)
        {
            CanExecuteParameter = parameter;
            return CanExecuteReturnValue;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">The parameter expected to be passed</param>
        public void Execute(object parameter)
        {
            ExecuteParameter = parameter;
        }

        /// <summary>
        /// Event method that does not take a parameter
        /// </summary>
        public void EventMethod()
        {
        }

        /// <summary>
        /// Action method that takes a parameter
        /// </summary>
        /// <param name="value"><see cref="string"/> that was passed</param>
        public void ActionMethod(string value)
        {
            MethodParameter = value;
        }

        /// <summary>
        /// Static method that does not take a parameter
        /// </summary>
        public static void StaticMethod()
        {
        }
    }
}