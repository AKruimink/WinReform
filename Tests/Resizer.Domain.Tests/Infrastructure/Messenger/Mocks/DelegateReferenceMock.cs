using Resizer.Domain.Infrastructure.Messenger;
using System;

namespace Resizer.Domain.Tests.Infrastructure.Messenger.Mocks
{
    /// <summary>
    /// Defines a mock implementation of <see cref="IDelegateReference"/>
    /// </summary>
    public class DelegateReferenceMock : IDelegateReference
    {
        /// <summary>
        /// The delegate to be executed
        /// </summary>
        public Delegate? Delegate { get; set; }

        /// <summary>
        /// Create a new mock of the <see cref="DelegateReference"/>
        /// </summary>
        public DelegateReferenceMock()
        {

        }

        /// <summary>
        /// Create a new mock of the <see cref="DelegateReference"/>
        /// </summary>
        /// <param name="delegate"><see cref="Delegate"/> to be referenced</param>
        public DelegateReferenceMock(Delegate @delegate)
        {
            Delegate = @delegate;
        }
    }
}
