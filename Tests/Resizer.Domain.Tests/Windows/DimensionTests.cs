using Resizer.Domain.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Resizer.Domain.Tests.Windows
{
    /// <summary>
    /// Tests for the <see cref="DimensionTests"/>
    /// </summary>
    public class DimensionTests
    {
        #region Equals Tests

        [Fact]
        public void Equals_EqualDimensions_ShouldReturnTrue()
        {
            // Prepare
            var dimension1 = new Dimension { Left = 1, Top = 2, Right = 3, Bottom = 4 };
            var dimension2 = new Dimension { Left = 1, Top = 2, Right = 3, Bottom = 4 };

            // Act

            // Assert
            Assert.True(dimension1.Equals(dimension2));
        }

        [Fact]
        public void Equals_UnEqualDimensions_ShouldReturnFalse()
        {
            // Prepare
            var dimension1 = new Dimension { Left = 1, Top = 2, Right = 3, Bottom = 4 };
            var dimension2 = new Dimension { Left = 4, Top = 3, Right = 2, Bottom = 1 };

            // Act

            // Assert
            Assert.False(dimension1.Equals(dimension2));
        }

        [Fact]
        public void Equals_InvalidType_ShouldReturnFalse()
        {
            // Prepare
            var dimension1 = new Dimension { Left = 1, Top = 2, Right = 3, Bottom = 4 };

            // Act

            // Assert
            Assert.False(dimension1.Equals(1));
        }

        #endregion

        #region GetHashCode Tests

        [Fact]
        public void GetHashCode_EqualInstances_ShouldReturnEqualHashCode()
        {
            // Prepare
            var dimension1 = new Dimension { Left = 1, Top = 2, Right = 3, Bottom = 4 };
            var dimension2 = new Dimension { Left = 1, Top = 2, Right = 3, Bottom = 4 };

            // Act
            var hashCode1 = dimension1.GetHashCode();
            var hashCode2 = dimension2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_UnEqualInstances_ShouldReturnUniqueHashCode()
        {
            // Prepare
            var dimension1 = new Dimension { Left = 1, Top = 2, Right = 3, Bottom = 4 };
            var dimension2 = new Dimension { Left = 4, Top = 3, Right = 2, Bottom = 1 };

            // Act
            var hashCode1 = dimension1.GetHashCode();
            var hashCode2 = dimension2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }

        #endregion
    }
}
