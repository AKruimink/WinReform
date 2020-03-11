using System;
using System.Reflection;

namespace Resizer.Gui.Helpers.EventManager
{
    /// <summary>
    /// Defines a invalid handle error that occures during <see cref="WeakEventManager.HandleEvent"/> execution
    /// </summary>
    public class InvalidHandleEventException : Exception
    {
        /// <summary>
        /// Intializes a new instance of <see cref="InvalidHandleEventException"/>
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="targetParameterCountException">The <see cref="TargetParameterCountException"/> thrown</param>
        public InvalidHandleEventException(string message, TargetParameterCountException targetParameterCountException)
            : base(message, targetParameterCountException)
        {
        }
    }
}
