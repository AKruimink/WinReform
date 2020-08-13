using WinReform.Gui.Infrastructure.Common.Command;
using System;
using Xunit;
using Moq;
using WinReform.Tests.Fixtures;

namespace WinReform.Gui.Tests.Infrastructure.Common.Command
{
    /// <summary>
    /// Tests for the <see cref="DelegateCommand"/>
    /// </summary>
    public class DelegateCommandTests
    {
        private delegate void DelegateFixture(ClassFixture classFixture);

        #region Constructor Tests

        [Fact]
        public void Construct_Nulls_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand(null!, null);
            });
        }

        [Fact]
        public void Construct_NullExecuteMethod_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand(null!);
            });
        }

        [Fact]
        public void ConstructWithGenericType_Object_ShouldIntializesValues()
        {
            // Act
            var actual = new DelegateCommand<object>(param => { });

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void ConstructWithGenericType_Nullable_ShouldIntializesValues()
        {
            // Act
            var actual = new DelegateCommand<int?>(param => { });

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void ConstructWithGenericType_NonNullable_ShouldThrowInvalidCastException()
        {
            // Assert
            Assert.Throws<InvalidCastException>(() =>
            {
                var actual = new DelegateCommand<int>(param => { });
            });
        }

        [Fact]
        public void ConstructWithGenericType_NullParameters_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand<object>(null!, null);
            });
        }

        [Fact]
        public void ConstructWithGenericType_NullExecuteMethod_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand<object>(null!);
            });
        }

        [Fact]
        public void ConstructWithGenericType_NullCanExecuteMethod_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new DelegateCommand<object>(param => { }, null);
            });
        }

        #endregion Constructor Tests

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
            var callBackActionMock = new Mock<Action<object>>();
            var command = new DelegateCommand<object>(callBackActionMock.Object);

            // Act
            command.Execute(new object());

            // Assert
            callBackActionMock.Verify(x => x, Times.Once);
        }

        [Fact]
        public void Execute_PassInstance_ShouldPassInstance()
        {
            // Prepare
            ClassFixture? passedFakeClass = default;
            var fakeClass = new ClassFixture();
            var callBackActionMock = new Mock<Action<ClassFixture>>();
            var command = new DelegateCommand<ClassFixture>(callBackActionMock.Object);

            callBackActionMock.Setup(x => x(It.IsAny<ClassFixture>()))
                .Callback<ClassFixture>((obj) => passedFakeClass = obj);

            // Act
            command.Execute(fakeClass);

            // Assert
            callBackActionMock.Verify(x => x, Times.Once);
            Assert.NotNull(passedFakeClass);
            Assert.Equal(fakeClass, passedFakeClass);
        }

        #endregion Execute Tests

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
            var parameter = new object();
            var fakeClass = new ClassFixture();
            var callBackActionMock = new Mock<Action<object>>();
            var command = new DelegateCommand<object>(callBackActionMock.Object, (obj) => 
            {
                // Assert
                Assert.Equal(parameter, obj);
                return true; 
            });
            
            // Act
            var returnValue = command.CanExecute(parameter);
        }

        [Fact]
        public void CanExecute_Unassigned_ShouldReturnTrue()
        {
            // Prepare
            var callBackActionMock = new Mock<Action<object>>();
            var command = new DelegateCommand<object>(callBackActionMock.Object);

            // Act
            var returnValue = command.CanExecute(null!);

            // Assert
            Assert.True(returnValue);
        }

        [Fact]
        public void CanExecute_RaiseCanExecuteChanged_ShouldReturnTrue()
        {
            // Prepare
            var callBackActionMock = new Mock<Action<object>>();
            var command = new DelegateCommand<object>(callBackActionMock.Object);
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
            var fakeClass = new ClassFixture();
            var command = new DelegateCommand<ClassFixture>((p) => { }, delegate (ClassFixture parameter)
            {
                // Assert
                Assert.Same(fakeClass, parameter);

                // Act
                executeCalled = true;
                return true;
            });

            // Act
            command.CanExecute(fakeClass);

            // Assert
            Assert.True(executeCalled);
        }

        #endregion CanExecute Tests
    }
}
