using System;
using System.Globalization;
using System.Windows.Controls;

namespace WinReform.Infrastructure.Validation
{
    /// <summary>
    /// Defines a class that validates if a given object is of a specific numeric value type
    /// </summary>
    public class NumericValidationRule : ValidationRule
    {
        /// <summary>
        /// Indicates if a null value is allowed, if allowed <see cref="NumericValidationRule"/> will return a valid result
        /// Note: defaults to true
        /// </summary>
        public bool AllowNull { get; set; } = true;

        /// <summary>
        /// <see cref="Type"/> that should be validated against
        /// </summary>
        public Type? ValidationType { get; set; }

        /// <summary>
        /// Validates if a given value is of the correct numeric type
        /// </summary>
        /// <param name="value"><see cref="object"/> containing the value to be validated</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> used during validation</param>
        /// <returns>Returns <see cref="ValidationResult"/> that defines if the value was valid or not</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var strValue = Convert.ToString(value);

            if (string.IsNullOrEmpty(strValue))
            {
                if (AllowNull)
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, $"Value cannot be converted to string");
            }

            var canConvert = (ValidationType?.Name) switch
            {
                nameof(Boolean) => bool.TryParse(strValue, out var boolValue),
                nameof(Double) => double.TryParse(strValue, out var doubleValue),
                nameof(Int32) => int.TryParse(strValue, out var intValue),
                nameof(Int64) => long.TryParse(strValue, out var longValue),
                _ => throw new InvalidCastException($"{ValidationType?.Name} is not a supported type"),
            };

            return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should only contain numeric values");
        }
    }
}
