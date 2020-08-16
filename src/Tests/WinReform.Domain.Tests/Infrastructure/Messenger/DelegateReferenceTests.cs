using WinReform.Domain.Infrastructure.Messenger;
using System;
using System.Threading.Tasks;
using Xunit;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace WinReform.Domain.Tests.Infrastructure.Messenger
{
    /// <summary>
    /// Tests for the <see cref="DelegateReference"/>
    /// </summary>
    public class DelegateReferenceTests
    {
        /// <summary>
        /// Empty action used for testing <see cref="DelegateReference"/> with static delegates
        /// </summary>
        private static void StaticActionReference() { }

        #region Constructor Tests

        [Fact]
        public void Construct_NullDelegate_ShouldThrowNullArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var delegateReference = new DelegateReference(null!, true);
            });
        }

        [Fact]
        public async Task Construct_KeepReferenceAliveTrue_ShouldPreventGarbageCollection()
        {
            // Prepare
            Action referenceAction = () => { };
            var delegateReference = new DelegateReference(referenceAction, true);

            // Act
            referenceAction = null!;
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.NotNull(delegateReference.Delegate);
        }

        [Fact]
        public async Task Construct_KeepReferenceAliveFalse_ShouldGetGarbageCollected()
        {
            // Prepare
            Action referenceAction = () => { };
            var delegateReference = new DelegateReference(referenceAction, false);

            // Act
            referenceAction = null!;
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.NotNull(delegateReference.Delegate);
        }

        [Fact]
        public void Construct_WeakReferenceStaticDelegate_ShouldSet()
        {
            // Prepare
            var delegateReference = new DelegateReference((Action)StaticActionReference, false);

            // Assert
            Assert.NotNull(delegateReference.Delegate);
        }

        #endregion Constructor Tests

        #region GetDelegate Tests

        [Fact]
        public void GetDelegate_ActionDelegate_ShouldReturnAction()
        {
            // Prepare
            var passedArgument = "";
            void ReferenceAction(string arg) { passedArgument = arg; }
            var action = new Action<string>(ReferenceAction);
            var delegateReference = new DelegateReference(action, false);

            // Act
            ((Action<string>)delegateReference.Delegate!)("payload");

            // Assert
            Assert.Equal("payload", passedArgument);
        }

        [Fact]
        public async Task GetDelegate_DelegateNotAlive_ShouldReturnNull()
        {
            // Prepare
            Action referenceAction = () => { };
            var weakReference = new WeakReference(referenceAction);
            var delegateReference = new DelegateReference(referenceAction, false);

            // Act
            referenceAction = null!;
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.Null(delegateReference.Delegate);
        }

        #endregion GetDelegate Tests

        #region DelegateEquals Tests

        [Fact]
        public void DelegateEquals_EqualDelegate_ShouldReturnTrue()
        {
            // Prepare
            static void ReferenceAction(string arg) { }
            var action = new Action<string>(ReferenceAction);
            var delegateReference = new DelegateReference(action, false);

            // Assert
            Assert.True(delegateReference.DelegateEquals(new Action<string>(ReferenceAction)));
        }

        [Fact]
        public void DelegateEquals_DoesntEqualDelegate_ShouldReturnFalse()
        {
            // Prepare
            static void ReferenceAction(string arg) { }
            Action eventAction = () => { };
            var delegateReference = new DelegateReference(new Action<string>(ReferenceAction), false);

            // Act

            // Assert
            Assert.False(delegateReference.DelegateEquals(eventAction));
        }

        [Fact]
        public void DelegateEquals_EqualsNullDelegate_ShouldReturnFalse()
        {
            // Prepare
            Action referenceAction = () => { };
            var weakReference = new WeakReference(referenceAction);
            var delegateReference = new DelegateReference(referenceAction, false);

            // Act
            GC.KeepAlive(weakReference);

            // Assert
            Assert.False(delegateReference.DelegateEquals(null!));
            Assert.True(weakReference.IsAlive);
        }

        [Fact]
        public async Task DelegateEquals_EqualNullDelegateReferenceNotAlive_ShouldReturnTrue()
        {
            // Prepare
            Action referenceAction = () => { };
            var weakReference = new WeakReference(referenceAction);
            var delegateReference = new DelegateReference(referenceAction, false);

            // Act
            referenceAction = null!;
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.False(weakReference.IsAlive);
            Assert.True(delegateReference.DelegateEquals(null!));
        }

        [Fact]
        public void DelegateEquals_EqualStaticDelegate_ShouldReturnTrue()
        {
            // Prepare
            var delegateReference = new DelegateReference((Action)StaticActionReference, false);

            // Act

            // Assert
            Assert.True(delegateReference.DelegateEquals((Action)StaticActionReference));
        }

        #endregion DelegateEquals Tests
    }
}
