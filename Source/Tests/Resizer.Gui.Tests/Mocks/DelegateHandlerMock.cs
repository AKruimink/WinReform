namespace Resizer.Gui.Tests.Mocks
{    
    /// <summary>
    /// Defines a mock implementation of a delegate handler 
    /// </summary>
    public class DelegateHandlerMock
    {
        /// <summary>
        /// Indicates if the command can be executed at this time
        /// </summary>
        public bool CanExecuteReturnValue { get; set; } = true;

        /// <summary>
        /// Argument passed that is normaly used to determine if the command can be executed
        /// </summary>
        public object? CanExecuteParameter { get; set; }

        /// <summary>
        /// The argument passed to the command during execution
        /// </summary>
        public object? ExecuteParameter { get; set; }

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
    }
}
