using System;
using System.Diagnostics.CodeAnalysis;

namespace WinReform.Tests.Fixtures
{
    /// <summary>
    /// Defines a class that represents a model fixture that provides a fake model properties for testing puprose
    /// </summary>
    public class ModelFixture : IEquatable<ModelFixture>, IComparable<ModelFixture>
    {
        /// <summary>
        /// Gets or Sets the identifier of the model
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets a test text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or Sets a test number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Compares a given <see cref="ModelFixture"/> to the current <see cref="ModelFixture"/>
        /// </summary>
        /// <param name="obj"><see cref="ModelFixture"/> to compare</param>
        /// <returns>Returns <see langword="true"/> if both <see cref="ModelFixture"/> are equal, otherwise returns <see langword="false"/></returns>
        public bool Equals([AllowNull] ModelFixture obj)
            => obj?.Id == Id
            && obj?.Text == Text
            && obj?.Number == Number;

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
        public override bool Equals(object obj)
            => obj is ModelFixture fixture
            && Equals(fixture);

        /// <summary>
        /// Gets the hashcode of the <see cref="ModelFixture"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="ModelFixture"/></returns>
        public override int GetHashCode()
            => (Id, Text, Number).GetHashCode();
    }
}
