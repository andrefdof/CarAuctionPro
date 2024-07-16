using CarAuctionPro.Utilities.Helpers;
using FluentValidation;

namespace CarAuctionPro.Classes.VehicleTypes
{
    public class Suv : Vehicle
    {
        private int numberOfSeats;

        public int NumberOfSeats
        {
            get { return numberOfSeats; }
            set { value.Validate(new NumberOfSeatsValidator()); numberOfSeats = value; }
        }

        public Suv(Guid id, string manufacturer, string model, int year, decimal startingBid, int numberOfSeats)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            StartingBid = startingBid;
            NumberOfSeats = numberOfSeats;
        }

        public override string GetVehicleTypeDetails()
        {
            return NumberOfSeats.GetStringPropertyDetails("Number of seats");
        }

        private class NumberOfSeatsValidator : AbstractValidator<int>
        {
            public NumberOfSeatsValidator()
            {
                RuleFor(r => r)
                    .Cascade(CascadeMode.Stop)
                    .Must(value => UtilityHelper.BeInValidRange(value.ToString(), 1, maxRange: 10))
                    .WithMessage("Suv number of seats must be a number between 1 and 10.");
            }
        }
    }
}
