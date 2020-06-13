using System;
using System.Threading.Tasks;
using Resizer.Gui.Common.Messenger;
using Resizer.Gui.Tests.Mocks;
using Xunit;

namespace Resizer.Gui.Tests.Infrastructure.Common.Messenger
{
    /// <summary>
    /// Tests for the <see cref="DelegateReference"/>
    /// </summary>
    public class DelegateReferenceTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_NullDelegate_ShouldThrowNullArgumentException()
        {
            // Prepare

            // Act

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
            var @delegate = new DelegateHandlerMock();
            var delegateReference = new DelegateReference((Action)@delegate.EventMethod, true);

            // Act
            @delegate = null!;
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.NotNull(delegateReference.Delegate);
        }

        [Fact]
        public async Task Construct_KeepReferenceAliveFalse_ShouldGetGarbageCollected()
        {
            // Prepare
            var @delegate = new DelegateHandlerMock();
            var delegateReference = new DelegateReference((Action)@delegate.EventMethod, true);

            // Act
            @delegate = null!;
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.NotNull(delegateReference.Delegate);
        }

        [Fact]
        public void Construct_WeakReferenceStaticDelegate_ShouldSet()
        {
            // Prepare
            var delegateReference = new DelegateReference((Action)DelegateHandlerMock.StaticMethod, false);

            // Act

            // Assert
            Assert.NotNull(delegateReference.Delegate);
        }


        #endregion

        #region GetDelegate Tests

        [Fact]
        public void GetDelegate_ActionDelegate_ShouldReturnAction()
        {
            // Prepare
            var @delegate = new DelegateHandlerMock();
            var action = new Action<string>(@delegate.ActionMethod);
            var delegateReference = new DelegateReference(action, false);

            // Act
            ((Action<string>)delegateReference.Delegate!)("payload");

            // Assert
            Assert.Equal("payload", @delegate.MethodParameter);
        }

        [Fact]
        public async Task GetDelegate_DelegateNotAlive_ShouldReturnNull()
        {
            // Prepare
            var @delegate = new DelegateHandlerMock();
            var weakReference = new WeakReference(@delegate);
            var delegateReference = new DelegateReference((Action)@delegate.EventMethod, false);

            // Act
            @delegate = null!;
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.Null(delegateReference.Delegate);
        }

        #endregion

        #region DelegateEquals Tests

        [Fact]
        public void DelegateEquals_EqualDelegate_ShouldReturnTrue()
        {
            // Prepare
            var @delegate = new DelegateHandlerMock();
            var action = new Action<string>(@delegate.ActionMethod);
            var delegateReference = new DelegateReference(action, false);

            // Act

            // Assert
            Assert.True(delegateReference.DelegateEquals(new Action<string>(@delegate.ActionMethod)));
        }

        [Fact]
        public void DelegateEquals_DoesntEqualDelegate_ShouldReturnFalse()
        {
            // Prepare
            var @delegate = new DelegateHandlerMock();
            var delegateReference = new DelegateReference(new Action<string>(@delegate.ActionMethod), false);

            // Act

            // Assert
            Assert.False(delegateReference.DelegateEquals(new Action(@delegate.EventMethod)));
        }

        [Fact]
        public void DelegateEquals_EqualsNullDelegate_ShouldReturnFalse()
        {
            // Prepare
            var @delegate = new DelegateHandlerMock();
            var weakReference = new WeakReference(@delegate);
            var delegateReference = new DelegateReference((Action)@delegate.EventMethod, false);

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
            var @delegate = new DelegateHandlerMock();
            var weakReference = new WeakReference(@delegate);
            var delegateReference = new DelegateReference((Action)@delegate.EventMethod, false);

            // Act
            @delegate = null!;
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
            var delegateReference = new DelegateReference((Action)DelegateHandlerMock.StaticMethod, false);

            // Act

            // Assert
            Assert.True(delegateReference.DelegateEquals((Action)DelegateHandlerMock.StaticMethod));
        }

        #endregion
    }
}
