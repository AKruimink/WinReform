using Resizer.Gui.Common.ViewModel;
using Xunit;

namespace Resizer.Gui.Tests.Common.ViewModel
{
    /// <summary>
    /// Tests for the <see cref="ViewModelBase"/>
    /// </summary>
    public class ViewModelBaseTests
    {
        private class MockViewModel : ViewModelBase
        {
            private int _mockProperty;

            public int MockProperty
            {
                get => _mockProperty;
                set => SetProperty(ref _mockProperty, value);
            }

            internal void InvokeOnPropertyChanged()
            {
                RaisePropertyChanged(nameof(MockProperty));
            }
        }

        #region SetProperty Tests

        [Fact]
        public void SetProperty_NewValue_ShouldSetValue()
        {
            // Prepare
            int value = 10;
            var mockViewModel = new MockViewModel();

            // Assert
            Assert.Equal(0, mockViewModel.MockProperty);

            // Act
            mockViewModel.MockProperty = value;

            // Assert
            Assert.Equal(value, mockViewModel.MockProperty);
        }

        [Fact]
        public void SetProperty_UnchangedValue_ShouldNotSet()
        {
            // Prepare
            var oldValue = 10;
            var newValue = 10;
            var mockViewModel = new MockViewModel {MockProperty = oldValue};
            var invoked = false;
            mockViewModel.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(mockViewModel.MockProperty)))
                {
                    invoked = true;
                }
            };

            // Act
            mockViewModel.MockProperty = newValue;

            // Assert
            Assert.False(invoked);
            Assert.Equal(oldValue, mockViewModel.MockProperty);
        }

        [Fact]
        public void SetProperty_NewValue_ShouldRaisePropertyChanged()
        {
            // Prepare
            var value = 10;
            var mockViewModel = new MockViewModel();
            var invoked = false;
            mockViewModel.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(mockViewModel.MockProperty)))
                {
                    invoked = true;
                }
            };

            // Act
            mockViewModel.MockProperty = value;

            // Assert
            Assert.True(invoked);
        }

        #endregion

        #region OnPropertyChanged Tests

        [Fact]
        public void OnPropertyChanged_Raised_ShouldExtractPropertyName()
        {
            // Prepare
            var invoked = false;
            var mockViewModel = new MockViewModel();
            mockViewModel.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(mockViewModel.MockProperty)))
                {
                    invoked = true;
                }
            };

            // Act
            mockViewModel.InvokeOnPropertyChanged();

            //Assert
            Assert.True(invoked);
        }

        #endregion
    }
}
