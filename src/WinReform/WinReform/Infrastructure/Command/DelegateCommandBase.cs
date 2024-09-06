using System;
using System.Threading;
using System.Windows.Input;

namespace WinReform.Infrastructure.Common.Command
{
    /// <summary>
    /// Defines a base command
    /// </summary>
    public abstract class DelegateCommandBase : ICommand
    {
        /// <summary>
        /// Location where the application code is being executed
        /// </summary>
        private readonly SynchronizationContext? _synchronizationContext;

        /// <summary>
        /// Occures when the can execute state changes
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// A base command that provides base functionallity to a command
        /// </summary>
        protected DelegateCommandBase()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        void ICommand.Execute(object? parameter)
        {
            Execute(parameter);
        }

        /// <summary>
        /// Determines if the command can be exected
        /// </summary>
        /// <param name="parameter">Data used to determine if the command can be executed</param>
        /// <returns>Returns <see langword="true"/> if the command can execute, otherwise <see langword="false"/></returns>
        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute(parameter);
        }

        /// <summary>
        /// Raises <see cref="CanExecuteChanged"/> to let every command requery to check if it can execute
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        /// <summary>
        /// Determines if the command can be exected
        /// </summary>
        /// <param name="parameter">Data used to determine if the command can be executed</param>
        /// <returns>Returns <see langword="true"/> if the command can execute, otherwise <see langword="false"/></returns>
        protected abstract bool CanExecute(object? parameter);

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        protected abstract void Execute(object? parameter);

        /// <summary>
        /// Raises the <see cref="ICommand.CanExecuteChanged"/> allowing every command to requery <see cref="ICommand.CanExecute"/>
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
                {
                    _synchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
                }
                else
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
