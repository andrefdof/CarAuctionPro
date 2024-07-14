using FluentValidation;

namespace CarAuctionPro.Classes
{
    public class ManufacturerValidator : AbstractValidator<string>
    {
        public ManufacturerValidator()
        {
            RuleFor(r => r)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Manufacturer cannot be empty.")
                .MaximumLength(50)
                .WithMessage("Manufacturer cannot exceed 50 characters.");
        }
    }

    public class ModelValidator : AbstractValidator<string>
    {
        public ModelValidator()
        {
            RuleFor(r => r)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Model cannot be empty.")
                .MaximumLength(50)
                .WithMessage("Model cannot exceed 50 characters.");
        }
    }

    public class YearValidator : AbstractValidator<int>
    {
        public YearValidator()
        {
            RuleFor(r => r)
                .Cascade(CascadeMode.Stop)
                .Must(y => y >= 1886 && y <= DateTime.Now.Year) // The first car was made in 1886
                .WithMessage($"Year must be between 1886 and {DateTime.Now.Year}.");
        }
    }

    public class StartingBidValidator : AbstractValidator<decimal>
    {
        public StartingBidValidator()
        {
            RuleFor(r => r)
                .GreaterThanOrEqualTo(0).WithMessage("Value must be a non-negative number.");
        }
    }
}
