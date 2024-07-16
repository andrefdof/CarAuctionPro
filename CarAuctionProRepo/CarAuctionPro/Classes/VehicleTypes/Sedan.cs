using CarAuctionPro.Utilities.Helpers;
using FluentValidation;

namespace CarAuctionPro.Classes.VehicleTypes
{
    public class Sedan : Vehicle
    {
        private int numberOfDoors;

        public int NumberOfDoors
        {
            get { return numberOfDoors; }
            set { value.Validate(new NumberOfDoorsValidator()); numberOfDoors = value; }
        }

        public Sedan(Guid id, string manufacturer, string model, int year, decimal startingBid, int numberOfDoors)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            StartingBid = startingBid;
            NumberOfDoors = numberOfDoors;
        }
        public override string GetVehicleTypeDetails()
        {
            return NumberOfDoors.GetStringPropertyDetails("Number of doors");
        }

        private class NumberOfDoorsValidator : AbstractValidator<int>
        {
            public NumberOfDoorsValidator()
            {
                RuleFor(r => r)
                    .Cascade(CascadeMode.Stop)
                    .Must(value => UtilityHelper.BeInValidRange(value.ToString(), 2, maxRange: 5))
                    .WithMessage("Sedan number of doors must be a number between 2 and 5.");
            }
        }
    }
}
