﻿using CarAuctionPro.Interfaces;
using CarAuctionPro.Utilities.Helpers;
using FluentValidation;

namespace CarAuctionPro.Classes.VehicleTypes
{
    public class Hatchback : Vehicle, IVehicle
    {
        private int numberOfDoors;

        public int NumberOfDoors
        {
            get { return numberOfDoors; }
            set { value.Validate(new NumberOfDoorsValidator()); numberOfDoors = value; }
        }

        public Hatchback(Guid id, string manufacturer, string model, int year, decimal startingBid, int numberOfDoors)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            StartingBid = startingBid;
            NumberOfDoors = numberOfDoors;
        }
        public string GetVehicleTypeDetails()
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
                    .WithMessage("Hatchback number of doors must be a number between 2 and 5.");
            }
        }
    }
}
