using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Domain.Infrastructure.Model;
using WinReform.Tests.Fixtures;
using Xunit;

namespace WinReform.Domain.Tests.Infrastructure.Model
{
    /// <summary>
    /// Tests for the <see cref="ViewModelBase"/>
    /// </summary>
    public class ModelBaseTests : ModelBase
    {
        #region SetProperty Tests

        [Fact]
        public void SetProperty_NewValue_ShouldSetValue()
        {
            // Prepare
            var value = 10;
            var modelFixture = new ModelFixture
            {
                // Act
                Number = value
            };

            // Assert
            Assert.Equal(value, modelFixture.Number);
        }

        [Fact]
        public void SetProperty_UnchangedValue_ShouldNotSet()
        {
            // Prepare
            var oldValue = 10;
            var newValue = 10;
            var modelFixture = new ModelFixture { Number = oldValue };
            var invoked = false;
            modelFixture.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(modelFixture.Number)))
                {
                    invoked = true;
                }
            };

            // Act
            modelFixture.Number = newValue;

            // Assert
            Assert.False(invoked);
            Assert.Equal(oldValue, modelFixture.Number);
        }

        [Fact]
        public void SetProperty_NewValue_ShouldRaisePropertyChanged()
        {
            // Prepare
            var value = 10;
            var modelFixture = new ModelFixture();
            var invoked = false;
            modelFixture.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(modelFixture.Number)))
                {
                    invoked = true;
                }
            };

            // Act
            modelFixture.Number = value;

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
            var modelFixture = new ModelFixture();
            modelFixture.PropertyChanged += (o, e) =>
            {
                // Act
                if (e.PropertyName.Equals(nameof(modelFixture.Number)))
                {
                    invoked = true;
                }
            };

            // Act
            modelFixture.InvokePropertyChanged(nameof(modelFixture.Number));

            //Assert
            Assert.True(invoked);
        }

        #endregion OnPropertyChanged Tests
    }
}
