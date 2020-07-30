using System;
using System.Reflection;

namespace WinReform.Domain.Infrastructure.Messenger
{
    /// <summary>
    /// Defines a class that holds a references to a delegate
    /// </summary>
    public class DelegateReference : IDelegateReference
    {
        /// <summary>
        /// Strong reference to the delegate
        /// NOTE: Only used when delegate should be kept alive
        /// </summary>
        private readonly Delegate? _delegate;

        /// <summary>
        /// Weak reference to the delegate
        /// </summary>
        private readonly WeakReference? _weakReference;

        /// <summary>
        /// Information about the delegate
        /// </summary>
        private readonly MethodInfo? _methodInfo;

        /// <summary>
        /// Type of the delegate
        /// </summary>
        private readonly Type? _delegateType;

        /// <summary>
        /// Create a new <see cref="DelegateReference"/> that holds a weak or strong reference to a <see cref="Delegate"/>
        /// </summary>
        /// <param name="delegate">The <see cref="Delegate"/> to hold</param>
        /// <param name="keepReferenceAlive">If <see langword="true"/> the reference wil be kept alive using a string reference, otherwise a <see cref="WeakReference"/> is used</param>
        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (@delegate == null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            if (keepReferenceAlive)
            {
                _delegate = @delegate;
            }
            else
            {
                _weakReference = new WeakReference(@delegate?.Target);
                _methodInfo = @delegate?.GetMethodInfo();
                _delegateType = @delegate?.GetType();
            }
        }

        ///<inheritdoc/>
        public Delegate? Delegate
        {
            get
            {
                if (_delegate != null)
                {
                    return _delegate;
                }
                else
                {
                    return TryGetDelegate();
                }
            }
        }

        /// <summary>
        /// Check if the target <see cref="Delegate"/> is equal to another
        /// </summary>
        /// <param name="">The <see cref="Delegate"/> to compare against</param>
        /// <returns>Returns <see langword="true"/> if equal, otherwise <see langword="false"/></returns>
        public bool DelegateEquals(Delegate @delegate)
        {
            if (_delegate != null)
            {
                return _delegate == @delegate;
            }

            // Strong reference was null, so weak reference needs to exist to continue
            if (_weakReference == null || _methodInfo == null || _delegateType == null)
            {
                return false;
            }

            if (@delegate == null)
            {
                return !_methodInfo.IsStatic && !_weakReference.IsAlive;
            }

            return _weakReference.Target == @delegate.Target && Equals(_methodInfo, @delegate.GetMethodInfo());
        }

        /// <summary>
        /// Try to get the <see cref="Delegate"/> from the <see cref="WeakReference"/>
        /// </summary>
        /// <returns>Returns the created <see cref="Delegate"/> if no <see cref="Delegate"/> could be create it returns <see cref="null"/></returns>
        private Delegate? TryGetDelegate()
        {
            if (_methodInfo?.IsStatic ?? false)
            {
                return _methodInfo.CreateDelegate(_delegateType!, null);
            }

            var target = _weakReference?.Target;
            if (target != null)
            {
                return _methodInfo?.CreateDelegate(_delegateType!, target);
            }

            return null;
        }
    }
}
