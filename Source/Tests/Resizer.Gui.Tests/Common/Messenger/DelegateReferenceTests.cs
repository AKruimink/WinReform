using System;
using System.Threading.Tasks;
using Resizer.Gui.Helpers.Messenger;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.Messenger
{
    /// <summary>
    /// Tests for the <see cref="DelegateReference"/>
    /// </summary>
    public class DelegateReferenceTests
    {
        #region Test Fixtures

        private class DelegateHandler
        {
            public string? _actionArgs;

            public void DoEvent(string value)
            {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                var myValue = value;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            }

            public void SomeAction(string args)
            {
                _actionArgs = args;
            }

            public static void StaticMethod()
            {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                var someValue = 0;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            }
        }

        #endregion

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
            var @delegate = new DelegateHandler();
            var delegateReference = new DelegateReference((Action<string>)@delegate.DoEvent, true);

            // Act
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            @delegate = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.NotNull(delegateReference.Delegate);
        }

        [Fact]
        public async Task Construct_KeepReferenceAliveFalse_ShouldGetGarbageCollected()
        {
            // Prepare
            var @delegate = new DelegateHandler();
            var delegateReference = new DelegateReference((Action<string>)@delegate.DoEvent, true);

            // Act
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            @delegate = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            await Task.Delay(100);
            GC.Collect();

            // Assert
            Assert.NotNull(delegateReference.Delegate);
        }

        [Fact]
        public void Construct_WeakReferenceStaticDelegate_ShouldSet()
        {
            // Prepare
            var delegateReference = new DelegateReference((Action)DelegateHandler.StaticMethod, false);

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
            var @delegate = new DelegateHandler();
            var action = new Action<string>(@delegate.SomeAction);
            var delegateReference = new DelegateReference(action, false);

            // Act
            ((Action<string>)delegateReference.Delegate!)("payload");

            // Assert
            Assert.Equal("payload", @delegate._actionArgs);
        }

        [Fact]
        public async Task GetDelegate_DelegateNotAlive_ShouldReturnNull()
        {
            // Prepare
            var @delegate = new DelegateHandler();
            var weakReference = new WeakReference(@delegate);
            var delegateReference = new DelegateReference((Action<string>)@delegate.DoEvent, false);

            // Act
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            @delegate = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
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
            var @delegate = new DelegateHandler();
            var action = new Action<string>(@delegate.SomeAction);
            var delegateReference = new DelegateReference(action, false);

            // Act

            // Assert
            Assert.True(delegateReference.DelegateEquals(new Action<string>(@delegate.SomeAction)));
        }

        [Fact]
        public void DelegateEquals_DoesntEqualDelegate_ShouldReturnFalse()
        {
            // Prepare
            var @delegate = new DelegateHandler();
            var delegateReference = new DelegateReference(new Action<string>(@delegate.SomeAction), false);

            // Act

            // Assert
            Assert.False(delegateReference.DelegateEquals(new Action<string>(@delegate.DoEvent)));
        }

        [Fact]
        public void DelegateEquals_EqualsNullDelegate_ShouldReturnFalse()
        {
            // Prepare
            var @delegate = new DelegateHandler();
            var weakReference = new WeakReference(@delegate);
            var delegateReference = new DelegateReference((Action<string>)@delegate.DoEvent, false);

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
            var @delegate = new DelegateHandler();
            var weakReference = new WeakReference(@delegate);
            var delegateReference = new DelegateReference((Action<string>)@delegate.DoEvent, false);

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
            var delegateReference = new DelegateReference((Action)DelegateHandler.StaticMethod, false);

            // Act

            // Assert
            Assert.True(delegateReference.DelegateEquals((Action)DelegateHandler.StaticMethod));
        }

        #endregion
    }
}
