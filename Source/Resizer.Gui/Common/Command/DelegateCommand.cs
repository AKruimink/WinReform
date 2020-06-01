using System;
using System.Reflection;
using System.Windows.Input;

namespace Resizer.Gui.Helpers.Command
{
    /// <summary>
    /// An <see cref="ICommand"/> whose delegate doesnt take any parameters
    /// </summary>
    public class DelegateCommand : DelegateCommandBase
    {
        /// <summary>
        /// The <see cref="Action"/> to execute 
        /// </summary>
        private readonly Action _executeMethod;

        /// <summary>
        /// The <see cref="Func{TResult}"/> that determines if the command can be executed
        /// </summary>
        private readonly Func<bool> _canExecuteMethod;

        /// <summary>
        /// A new delegate command that that invokes an <see cref="Action"/> on execution
        /// </summary>
        /// <param name="executeMethod"><see cref="Action"/> to invoke on execution</param>
        public DelegateCommand(Action executeMethod) : this(executeMethod, () => true) { }

        /// <summary>
        /// A new delegate command that invokes an <see cref="Action"/> on execution when <see langword="Func" /> determines if it's able to execute
        /// </summary>
        /// <param name="executeMethod"></param>
        /// <param name="canExecuteMethod"></param>
        public DelegateCommand(Action executeMethod, Func<bool>? canExecuteMethod) : base()
        {
            if(executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod), "The Execute Method or Can Execute Method cannot be null ");
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        public void Execute()
        {
            _executeMethod();
        }

        /// <summary>
        /// Determine if the command can be executed
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the command can execute, otherwise <see langword="false"/></returns>
        public bool CanExecute()
        {
            return _canExecuteMethod();
        }
        
        ///<inheritdoc/>
        protected override void Execute(object parameter)
        {
            Execute();
        }

        ///<inheritdoc/>
        protected override bool CanExecute(object parameter)
        {
            return CanExecute();
        }
    }

    /// <summary>
    /// An <see cref="ICommand"/> whose delegate takes a parameter
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    public class DelegateCommand<T> : DelegateCommandBase
    {
        /// <summary>
        /// The <see cref="Action"/> to execute 
        /// </summary>
        private readonly Action<T> _executeMethod;

        /// <summary>
        /// The <see cref="Func{TResult}"/> that determines if the command can be executed
        /// </summary>
        private readonly Func<T, bool> _canExecuteMethod;

        /// <summary>
        /// A new delegate command that that invokes an <see cref="Action"/> on execution
        /// </summary>
        /// <param name="executeMethod"><see cref="Action"/> to invoke on execution</param>
        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, (o) => true) { }

        /// <summary>
        /// A new delegate command that invokes an <see cref="Action"/> on execution when <see langword="Func" /> determines if it's able to execute
        /// </summary>
        /// <param name="executeMethod"></param>
        /// <param name="canExecuteMethod"></param>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool>? canExecuteMethod) : base()
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod), "The Execute Method or Can Execute Method cannot be null ");
            }

            var typeInfo = typeof(T).GetTypeInfo();
            if(typeInfo.IsValueType)
            {
                if(!typeInfo.IsGenericType || !typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(typeInfo.GetGenericTypeDefinition().GetTypeInfo()))
                {
                    throw new InvalidCastException("Parameter type isnt a valid generic type");
                }
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public void Execute(T parameter)
        {
            _executeMethod(parameter);
        }

        /// <summary>
        /// Determine if the command can be executed
        /// </summary>
        /// <param name="parameter">Data used to determine if the command can be executed</param>
        /// <returns>Returns <see langword="true"/> if the command can execute, otherwise <see langword="false"/></returns>
        public bool CanExecute(T parameter)
        {
            return _canExecuteMethod(parameter);
        }

        ///<inheritdoc/>
        protected override void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        ///<inheritdoc/>
        protected override bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }
    }
}
