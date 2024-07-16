using CarAuctionPro.Utilities.Helpers;
using CarAuctionPro.Interfaces;

namespace CarAuctionPro.Classes
{
    public abstract class Vehicle: IVehicle
    {
        public Guid Id { get;  set; }
        private string manufacturer = string.Empty;
        private string model = string.Empty;
        private int year;
        private decimal startingBid;

        public string Manufacturer
        {
            get { return manufacturer; }
            set 
            {
                ArgumentNullException.ThrowIfNull(value, "manufacturer");

                value.Validate(new ManufacturerValidator()); 
                manufacturer = value; 
            }
        }

        public string Model
        {
            get { return model; }
            set
            {
                ArgumentNullException.ThrowIfNull(value, "model");

                value.Validate(new ModelValidator()); 
                model = value;
            }
        }

        public int Year
        {
            get { return year; }
            set { value.Validate(new YearValidator()); year = value; }
        }

        public decimal StartingBid
        {
            get { return startingBid; }
            set { value.Validate(new StartingBidValidator()); startingBid = value; }
        }

        public abstract string GetVehicleTypeDetails();
    }
}
