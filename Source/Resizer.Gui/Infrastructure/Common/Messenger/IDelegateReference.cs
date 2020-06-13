using System;

namespace Resizer.Gui.Infrastructure.Common.Messenger
{
    /// <summary>
    /// Represents a class that holds a reference to a delegate
    /// </summary>
    public interface IDelegateReference
    {
        /// <summary>
        /// Gets the referenced <see cref="System.Delegate"/> object, Returns <see cref="null"/> if non exist
        /// </summary>
        Delegate? Delegate { get; }
    }
}
