using System;

namespace Resizer.Gui.Helpers.Messenger
{
    /// <summary>
    /// Represents a class that holds a reference to a delegate
    /// </summary>
    public interface IDelegateReference
    {
        /// <summary>
        /// Gets the referenced <see cref="Delegate"/> object, Returns <see cref="null"/> if non exist
        /// </summary>
        Delegate? GetDelegate { get; }
    }
}
