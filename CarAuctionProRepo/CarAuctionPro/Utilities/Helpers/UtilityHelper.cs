using FluentValidation;
using System.Text;

namespace CarAuctionPro.Utilities.Helpers
{
    public static class UtilityHelper
    {
        public static void Validate<T>(this T instance, IValidator<T> validator)
        {
           
            var results = validator.Validate(instance);

            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            if (!results.IsValid)
            {
                var errors = string.Join(", ", results.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation failed: {errors}");
            }
        }

        public static string GetStringPropertyDetails<T>(this T property, string propertyName)
        {
            var sb = new StringBuilder();
            sb.Append(propertyName);
            sb.Append(" = ");
            sb.Append(property?.ToString());

            return sb.ToString();
        }

        public static bool BeAValidDecimal(string value)
        {
            return decimal.TryParse(value, out _);
        }

        public static bool BeInValidRange(string value, int intialRange , int maxRange)
        {
            if (decimal.TryParse(value, out decimal result))
            {
                return result >= intialRange && result <= maxRange;
            }

            return false;
        }
    }
}
