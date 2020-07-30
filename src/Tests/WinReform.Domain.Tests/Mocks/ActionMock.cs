using System;

namespace WinReform.Domain.Tests.Mocks
{
    /// <summary>
    /// Defines a mock implementation of a <see cref="Action"/>
    /// </summary>
    public class ActionMock
    {
        /// <summary>
        /// Gets or Sets the value passed on execution of the <see cref="Action"/>
        /// </summary>
        public object? PassedArgument { get; set; }

        /// <summary>
        /// Gets or Sets the indication if the <see cref="Action"/> has been executed
        /// </summary>
        public bool Executed { get; set; } = false;

        /// <summary>
        /// Gets or Sets the <see cref="Action"/> to be executed
        /// </summary>
        public Action? ActionToExecute { get; set; }

        /// <summary>
        /// Action that doesnt take a argument
        /// </summary>
        public void Action()
        {
            Executed = true;

            if(ActionToExecute != null)
            {
                ActionToExecute.Invoke();
            }
        }

        /// <summary>
        /// Action that takes an argument
        /// </summary>
        /// <param name="argument">The argument</param>
        public void Action(object argument)
        {
            PassedArgument = argument;
            Executed = true;

            if(ActionToExecute != null)
            {
                ActionToExecute.Invoke();
            }
        }
    }
}