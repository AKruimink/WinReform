using System;
using System.Diagnostics.CodeAnalysis;

namespace Resizer.Domain.Tests.Mocks
{
    /// <summary>
    /// Mock implementation of a model
    /// </summary>
    public class ModelMock : IEquatable<ModelMock>, IComparable<ModelMock>
    {
        /// <summary>
        /// Gets or Sets the identification of the model
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets a random <see cref="string"/> property
        /// </summary>
        public string SomeText { get; set; }

        /// <summary>
        /// Gets or Sets a random <see cref="int"/> property
        /// </summary>
        public int SomeNumber { get; set; }

        /// <summary>
        /// Comapares the current <see cref="ModelMock"/> to a given <see cref="ModelMock"/>
        /// </summary>
        /// <param name="other"><see cref="ModelMock"/> to compare to the current instance</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="ModelMock"/>, otherwise returns <see langword="false"/></returns>
        public bool Equals([AllowNull] ModelMock other) => other?.Id == Id && other?.SomeText == SomeText && other?.SomeNumber == SomeNumber;

        /// <summary>
        /// Comapares the current <see cref="ModelMock"/> to a given <see cref="object"/>
        /// </summary>
        /// <param name="obj"><see cref="object"/> to compare to the current instance</param>
        /// <returns>Returns <see langword="true"/> if the current istance is equal to the given <see cref="object"/>, otherwise returns <see langword="false"/></returns>
        public override bool Equals(object? obj) => obj is ModelMock window && Equals(window);

        /// <summary>
        /// Gets the hashCode of the <see cref="ModelMock"/>
        /// </summary>
        /// <returns>Returns <see cref="int"/> containing a unique hashcode that represents the instance of the current <see cref="ModelMock"/></returns>
        public override int GetHashCode() => (Id, SomeText, SomeNumber).GetHashCode();

        /// <summary>
        /// Compare if the current <see cref="MockModel"/> represents the same item as a given <see cref="MockModel"/>
        /// </summary>
        /// <param name="other"><see cref="ModelMock"/> to compare against</param>
        /// <returns>Returns <see langword="true"/> if the <see cref="ModelMock"/> represent the same <see cref="ModelMock"/>, otherwise returns <see langword="false"/></returns>
        public int CompareTo([AllowNull] ModelMock other)
        {
            if(Id == other?.Id)
            {
                return 0;
            }
            return -1;
        }
    }
}