using WinReform.Gui.Infrastructure.Common.ViewModel;
using WinReform.Tests.Fixtures;
using Xunit;

namespace WinReform.Gui.Tests.Infrastructure.Common.ViewModel
{
    /// <summary>
    /// Tests for the <see cref="ViewModelBase"/>
    /// </summary>
    public class ViewModelBaseTests : ViewModelBase
    {
        #region SetProperty Tests

        [Fact]
        public void SetProperty_NewValue_ShouldSetValue()
        {
            // Prepare
            var value = 10;
            var viewModelFixture = new ViewModelFixture
            {
                // Act
                Number = value
            };

            // Assert
            Assert.Equal(value, viewModelFixture.Number);
        }

        [Fact]
        public void SetProperty_UnchangedValue_ShouldNotSet()
        {
            // Prepare
            var oldValue = 10;
            var newValue = 10;
            var viewModelFixture = new ViewModelFixture { Number = oldValue };
            var invoked = false;
            viewModelFixture.PropertyChanged += (o, e) =>
            {
                // Act
                if(e.PropertyName.Equals(nameof(viewModelFixture.Number)))
                {
                    invoked = true;
                }
            };

            // Act
            viewModelFixture.Number = newValue;

            // Assert
            Assert.False(invoked);
            Assert.Equal(oldValue, viewModelFixture.Number);
        }

        [Fact]
        public void SetProperty_NewValue_ShouldRaisePropertyChanged()
        {
            // Prepare
            var value = 10;
            var viewModelFixture = new ViewModelFixture();
            var invoked = false;
            viewModelFixture.PropertyChanged += (o, e) =>
            {
                // Act
                if(e.PropertyName.Equals(nameof(viewModelFixture.Number)))
                {
                    invoked = true;
                }
            };

            // Act
            viewModelFixture.Number = value;

            // Assert
            Assert.True(invoked);
        }

        #endregion SetProperty Tests

        #region OnPropertyChanged Tests

        [Fact]
        public void OnPropertyChanged_Raised_ShouldExtractPropertyName()
        {
            // Prepare
            var invoked = false;
            var viewModelFixture = new ViewModelFixture();
            viewModelFixture.PropertyChanged += (o, e) =>
            {
                // Act
                if(e.PropertyName.Equals(nameof(viewModelFixture.Number)))
                {
                    invoked = true;
                }
            };

            // Act
            RaisePropertyChanged(nameof(viewModelFixture.Number));

            //Assert
            Assert.True(invoked);
        }

        #endregion OnPropertyChanged Tests
    }
}
