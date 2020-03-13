using System;
using Resizer.Gui.Helpers.EventManager;

namespace Resizer.Gui.Tests.Helpers.EventManager
{
    /// <summary>
    /// Defines a base class for the Weak Event Manager tests that provides methods, propeties and variables
    /// </summary>
    public abstract class WeakEventManagerTestsBase
    {
        public readonly IWeakEventManager WeakEventManager = new WeakEventManager();

        public readonly IWeakEventManager<string> WeakEventManagerType = new WeakEventManager<string>();

        public event EventHandler InvalidEvent
        {
            add => WeakEventManager.AddEventHandler(value);
            remove => WeakEventManager.RemoveEventHandler(value);
        }
    }
}
