using System;
using Resizer.Gui.Helpers.Command;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.Command
{
    /// <summary>
    /// Tests for the <see cref="DelegateCommand"/>
    /// </summary>
    public class DelegateCommandTests
    {
        private class DelegateHandler
        {
            public bool CanExecuteReturnValue { get; set; } = true;

            public object? CanExecuteParameter { get; set; }

            public object? ExecuteParameter { get; set; }

            public bool CanExecute(object parameter)
            {
                CanExecuteParameter = parameter;
                return CanExecuteReturnValue;
            }

            public void Execute(object parameter)
            {
                ExecuteParameter = parameter;
            }
        }

        private class TestClass
        {

        }

        #region Constructor Tests

        [Fact]
        public void Construct_Nulls_ShouldThrowArgumentNullException()
        {
            // Prepare

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand(null, null);
            });
        }

        [Fact]
        public void Construct_NullExecuteMethod_ShouldThrowArgumentNullException()
        {
            // Prepare

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand(null);
            });
        }

        [Fact]
        public void ConstructWithGenericType_Object_ShouldIntializesValues()
        {
            // Prepare

            // Act
            var actual = new DelegateCommand<object>(param => { });

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void ConstructWithGenericType_Nullable_ShouldIntializesValues()
        {
            // Prepare

            // Act
            var actual = new DelegateCommand<int?>(param => { });

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void ConstructWithGenericType_NonNullable_ShouldThrowInvalidCastException()
        {
            // Prepare

            // Act

            // Assert
            Assert.Throws<InvalidCastException>(() =>
            {
                var actual = new DelegateCommand<int>(param => { });
            });
        }

        [Fact]
        public void ConstructWithGenericType_Nulls_ShouldThrowArgumentNullException()
        {
            // Prepare

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand<object>(null, null);
            });
        }

        [Fact]
        public void ConstructWithGenericType_NullExecuteMethod_ShouldThrowArgumentNullException()
        {
            // Prepare

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand<object>(null);
            });
        }

        [Fact]
        public void ConstructWithGenericType_NullCanExecuteMethod_ShouldThrowArgumentNullException()
        {
            // Prepare

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand<object>(param => { }, null);
            });
        }

        #endregion

        #region Execute Tests

        [Fact]
        public void Execute_NonGenericDelegate_ShouldInvoke()
        {
            // Prepare
            var executed = false;
            var command = new DelegateCommand(() => { executed = true; });

            // Act
            command.Execute();

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public void Execute_PassObject_ShouldPassObject()
        {
            // Prepare
            var handler = new DelegateHandler();
            var command = new DelegateCommand<object>(handler.Execute);
            var parameter = new object();

            // Act
            command.Execute(parameter);

            // Assert
            Assert.Same(parameter, handler.ExecuteParameter);
        }

        [Fact]
        public void Execute_PassInstance_ShouldPassInstance()
        {
            // Prepare
            var executeCalled = false;
            var testClass = new TestClass();
            var command = new DelegateCommand<TestClass>(delegate (TestClass parameter)
            {
                // Act
                executeCalled = true;

                // Assert
                Assert.Same(testClass, parameter);
            });

            // Act
            command.Execute(testClass);

            // Assert
            Assert.True(executeCalled);
        }

        #endregion

        #region CanExecute Tests

        [Fact]
        public void CanExecute_NonGenericDelegate_ShouldInvokeCanExecute()
        {
            // Prepare
            var executed = false;
            var canExecute = false;
            var command = new DelegateCommand(() => { }, () => { executed = true; return true; });

            // Act
            canExecute = command.CanExecute();

            // Assert
            Assert.True(executed);
            Assert.True(canExecute);
        }

        [Fact]
        public void CanExecute_PassObject_ShouldPassObject()
        {
            // Prepare
            var handler = new DelegateHandler();
            var command = new DelegateCommand<object>(handler.Execute, handler.CanExecute);
            var parameter = new object();
            handler.CanExecuteReturnValue = true;

            // Act
            var returnValue = command.CanExecute(parameter);

            // Assert
            Assert.Same(parameter, handler.CanExecuteParameter);
            Assert.Equal(handler.CanExecuteReturnValue, returnValue);
        }

        [Fact]
        public void CanExecute_Unassigned_ShouldReturnTrue()
        {
            // Prepare
            var handler = new DelegateHandler();
            var command = new DelegateCommand<object>(handler.Execute);

            // Act
            var returnValue = command.CanExecute(null);

            // Assert
            Assert.True(returnValue);
        }

        [Fact]
        public void CanExecute_RaiseCanExecuteChanged_ShouldReturnTrue()
        {
            // Prepare
            var handler = new DelegateHandler();
            var command = new DelegateCommand<object>(handler.Execute);
            var canExecuteChangedRaised = false;

            // Act
            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };
            command.RaiseCanExecuteChanged();

            // Assert
            Assert.True(canExecuteChangedRaised);
        }

        [Fact]
        public void CanExecute_RemoveRaiseCanExecuteChangeddHandler_ShouldNotFire()
        {
            // Prepare
            var command = new DelegateCommand<object>((o) => { });
            var canExecuteChangedRaised = false;
            void Handler(object? s, EventArgs e) => canExecuteChangedRaised = true;

            // Act
            command.CanExecuteChanged += Handler;
            command.CanExecuteChanged -= Handler;
            command.RaiseCanExecuteChanged();

            // Assert
            Assert.False(canExecuteChangedRaised);
        }

        [Fact]
        public void CanExecute_PassInstance_ShouldPassInstance()
        {
            // Prepare
            var executeCalled = false;
            var testClass = new TestClass();
            var command = new DelegateCommand<TestClass>((p) => { }, delegate (TestClass parameter)
            {
                // Assert
                Assert.Same(testClass, parameter);

                // Act
                executeCalled = true;
                return true;
            });

            // Act
            command.CanExecute(testClass);

            // Assert
            Assert.True(executeCalled);
        }

        #endregion
    }
}
