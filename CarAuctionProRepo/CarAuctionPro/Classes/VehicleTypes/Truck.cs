using CarAuctionPro.Utilities.Helpers;
using FluentValidation;

namespace CarAuctionPro.Classes.VehicleTypes
{
    public class Truck : Vehicle
    {
        private string loadCapacity = string.Empty;

        public string LoadCapacity
        {
            get { return loadCapacity; }
            set 
            {
                ArgumentNullException.ThrowIfNull(value, "loadCapacity");
                value.Validate(new LoadCapacityValidator()); 
                loadCapacity = value; 
            }
        }

        public Truck(Guid id, string manufacturer, string model, int year, decimal startingBid, string loadCapacity) 
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            StartingBid = startingBid;
            LoadCapacity = loadCapacity;
        }
        public override string GetVehicleTypeDetails()
        {
            return LoadCapacity.GetStringPropertyDetails("Load Capacity");
        }

        private class LoadCapacityValidator : AbstractValidator<string>
        {
            public LoadCapacityValidator()
            {
                RuleFor(r => r)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage("Load Capacity cannot be empty.")
                    .Must(UtilityHelper.BeAValidDecimal)
                    .WithMessage("Load capacity must be a number.")
                    .Must(value => UtilityHelper.BeInValidRange(value, 1, maxRange: 1000000))
                    .WithMessage("Load capacity must be a number between 0 and 1000000.");
            }
        }
    }
}
