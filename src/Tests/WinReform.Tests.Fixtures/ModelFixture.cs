using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using WinReform.Domain.Infrastructure.Attributes;
using WinReform.Domain.Infrastructure.Model;

namespace WinReform.Tests.Fixtures
{
    /// <summary>
    /// Defines a class that represents a model fixture that provides a fake model properties for testing puprose
    /// </summary>
    public sealed class ModelFixture : ModelBase, IEquatable<ModelFixture>, IComparable<ModelFixture>, IEqualityComparer<ModelFixture>
    {
        /// <summary>
        /// Gets or Sets the identifier of the model
        /// </summary>
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _id;

        /// <summary>
        /// Gets or Sets a test number
        /// </summary>
        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        private int _number;

        /// <summary>
        /// Gets or Sets a test text
        /// </summary>
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _text = string.Empty;

        /// <summary>
        /// Gets a string that depens on the <see cref="INotifyPropertyChanged"/> of <see cref="Text"/>
        /// </summary>
        [DependsOnProperty(nameof(Text))]
        public string TextDependency => $"This string depends on {nameof(Text)}";

        /// <summary>
        /// Raises the RaisePropertyChanged of the <see cref="ModelFixture"/>
        /// </summary>
        /// <param name="propertyName">Name of the property that changed</param>
        public void InvokePropertyChanged(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Gets all dependencies properties of a given property name
        /// </summary>
        /// <returns>Returns <see cref="List{string}"/> containing all property names that rely on the given property names <see cref="INotifyPropertyChanged"/></returns>
        public List<string> GetPropertyDependencies(string propertyName) => PropertyDependencies[propertyName];

        /// <summary>
        /// Compares if a given <see cref="ModelFixture"/> represents the same <see cref="ModelFixture"/> as the current instance
        /// </summary>
        /// <param name="obj"><see cref="ModelFixture"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the given <see cref="ModelFixture"/> represents the same object, otherwise returns <see langword="false"/></returns>
        public int CompareTo([AllowNull] ModelFixture obj)
        {
            if (obj?.Id == Id)
            {
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// Compares a given <see cref="object"/> to the current <see cref="ModelFixture"/>
        /// </summary>
        /// <param name="obj"><see cref="object"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if the <see cref="object"/> is equal to the current <see cref="ModelFixture"/>, otherwise returns <see langword="false"/></returns>
        /// <returns></returns>
        public override bool Equals(object? obj)
            => obj is ModelFixture fixture
            && Equals(fixture);

        /// <summary>
        /// Compares a given <see cref="ModelFixture"/> to the current <see cref="ModelFixture"/>
        /// </summary>
        /// <param name="obj"><see cref="ModelFixture"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if both <see cref="ModelFixture"/> are equal, otherwise returns <see langword="false"/></returns>
        public bool Equals([AllowNull] ModelFixture obj)
            => obj?.Id == Id
            && obj.Text == Text
            && obj.Number == Number;

        /// <summary>
        /// Checks if two <see cref="ModelFixture"/> instances are equal.
        /// </summary>
        /// <param name="left">The left <see cref="ModelFixture"/> to compare.</param>
        /// <param name="right">The right <see cref="ModelFixture"/> to compare.</param>
        /// <returns>Returns <see langword="true"/> if both instances are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(ModelFixture left, ModelFixture right)
        {
            // Handle null comparisons
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right); // Use the instance Equals method
        }

        /// <summary>
        /// Checks if two <see cref="ModelFixture"/> instances are not equal.
        /// </summary>
        /// <param name="left">The left <see cref="ModelFixture"/> to compare.</param>
        /// <param name="right">The right <see cref="ModelFixture"/> to compare.</param>
        /// <returns>Returns <see langword="true"/> if the instances are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(ModelFixture left, ModelFixture right)
        {
            return !(left == right); // Use the == operator to simplify logic
        }

        /// <summary>
        /// Checks if the left <see cref="ModelFixture"/> is less than the right <see cref="ModelFixture"/>.
        /// </summary>
        /// <param name="left">The left <see cref="ModelFixture"/> to compare.</param>
        /// <param name="right">The right <see cref="ModelFixture"/> to compare.</param>
        /// <returns>Returns <see langword="true"/> if the left instance is less than the right; otherwise, <see langword="false"/>.</returns>
        public static bool operator <(ModelFixture left, ModelFixture right)
        {
            if (left is null && right is null) return false;
            if (left is null) return true;
            if (right is null) return false;

            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Checks if the left <see cref="ModelFixture"/> is greater than the right <see cref="ModelFixture"/>.
        /// </summary>
        /// <param name="left">The left <see cref="ModelFixture"/> to compare.</param>
        /// <param name="right">The right <see cref="ModelFixture"/> to compare.</param>
        /// <returns>Returns <see langword="true"/> if the left instance is greater than the right; otherwise, <see langword="false"/>.</returns>
        public static bool operator >(ModelFixture left, ModelFixture right)
        {
            if (left is null && right is null) return false;
            if (right is null) return true;
            if (left is null) return false;

            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Checks if the left <see cref="ModelFixture"/> is less than or equal to the right <see cref="ModelFixture"/>.
        /// </summary>
        /// <param name="left">The left <see cref="ModelFixture"/> to compare.</param>
        /// <param name="right">The right <see cref="ModelFixture"/> to compare.</param>
        /// <returns>Returns <see langword="true"/> if the left instance is less than or equal to the right; otherwise, <see langword="false"/>.</returns>
        public static bool operator <=(ModelFixture left, ModelFixture right)
        {
            return left == right || left < right;
        }

        /// <summary>
        /// Checks if the left <see cref="ModelFixture"/> is greater than or equal to the right <see cref="ModelFixture"/>.
        /// </summary>
        /// <param name="left">The left <see cref="ModelFixture"/> to compare.</param>
        /// <param name="right">The right <see cref="ModelFixture"/> to compare.</param>
        /// <returns>Returns <see langword="true"/> if the left instance is greater than or equal to the right; otherwise, <see langword="false"/>.</returns>
        public static bool operator >=(ModelFixture left, ModelFixture right)
        {
            return left == right || left > right;
        }

        /// <summary>
        /// Gets the hashcode of the <see cref="ModelFixture"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="ModelFixture"/></returns>
        public override int GetHashCode()
            => (Id, Text, Number).GetHashCode();

        /// <summary>
        /// Determines whether the specified <see cref="ModelFixture"/> instances are equal.
        /// </summary>
        /// <param name="x">The first <see cref="ModelFixture"/> to compare.</param>
        /// <param name="y">The second <see cref="ModelFixture"/> to compare.</param>
        /// <returns>
        /// Returns <see langword="true"/> if the specified <see cref="ModelFixture"/> instances are equal; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(ModelFixture? x, ModelFixture? y)
        {
            if (x is null && y is null) return true;
            if (x is null || y is null) return false;
            return x.Id == y.Id && x.Text == y.Text && x.Number == y.Number;
        }

        /// <summary>
        /// Returns a hash code for the specified <see cref="ModelFixture"/>.
        /// </summary>
        /// <param name="obj">The <see cref="ModelFixture"/> for which a hash code is to be returned. Cannot be <see langword="null"/>.</param>
        /// <returns>
        /// A hash code for the specified <see cref="ModelFixture"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="obj"/> is <see langword="null"/>.</exception>
        public int GetHashCode([DisallowNull] ModelFixture obj)
        {
            return (obj.Id, obj.Text, obj.Number).GetHashCode();
        }
    }
}
