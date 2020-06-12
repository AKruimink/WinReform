using Resizer.Gui.Common.ViewModel;
using Resizer.Gui.Tests.Common.ViewModel.Mocks;
using Resizer.Gui.Tests.Mocks;
using Xunit;

namespace Resizer.Gui.Tests.Common.ViewModel
{
    /// <summary>
    /// Tests for the <see cref="ViewModelBase"/>
    /// </summary>
    public class ViewModelBaseTests
    {
        #region SetProperty Tests

        [Fact]
        public void SetProperty_NewValue_ShouldSetValue()
        {
            // Prepare
            int value = 10;
            var mockViewModel = new ViewModelMock();

            // Assert
            Assert.Equal(0, mockViewModel.IntProperty);

            // Act
            mockViewModel.IntProperty = value;

            // Assert
            Assert.Equal(value, mockViewModel.IntProperty);
        }

        [Fact]
        public void SetProperty_UnchangedValue_ShouldNotSet()
        {
            // Prepare
            var oldValue = 10;
            var newValue = 10;
            var mockViewModel = new ViewModelMock { IntProperty = oldValue};
            var invoked = false;
            mockViewModel.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(mockViewModel.IntProperty)))
                {
                    invoked = true;
                }
            };

            // Act
            mockViewModel.IntProperty = newValue;

            // Assert
            Assert.False(invoked);
            Assert.Equal(oldValue, mockViewModel.IntProperty);
        }

        [Fact]
        public void SetProperty_NewValue_ShouldRaisePropertyChanged()
        {
            // Prepare
            var value = 10;
            var mockViewModel = new ViewModelMock();
            var invoked = false;
            mockViewModel.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(mockViewModel.IntProperty)))
                {
                    invoked = true;
                }
            };

            // Act
            mockViewModel.IntProperty = value;

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
            var mockViewModel = new ViewModelMock();
            mockViewModel.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(mockViewModel.IntProperty)))
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
