using CarAuctionPro.Classes.VehicleTypes;
using Faker;
using System;


namespace CarAuctionProTests.Factories
{
    public static class VehicleFactory
    {
        private static readonly Random _random = new Random();

        public static Suv CreateSuv(Guid? id = null, string? manufacturer = null)
        {
            return new Suv(
                id: id ?? Guid.NewGuid(),
                manufacturer: manufacturer ?? Company.Name(),
                model: Company.Name(),
                year: _random.Next(1886, DateTime.Now.Year),
                startingBid: _random.Next(0, 100000000),
                numberOfSeats: _random.Next(1, 10));
        }

        public static Sedan CreateSedan()
        {
            return new Sedan(
                id: Guid.NewGuid(),
                manufacturer: Company.Name(),
                model: Company.Name(),
                year: _random.Next(1886, DateTime.Now.Year),
                startingBid: _random.Next(0, 100000000),
                numberOfDoors: _random.Next(2, 5));
        }

        public static Hatchback CreateHatchback()
        {
            return new Hatchback(
                id: Guid.NewGuid(),
                manufacturer: Company.Name(),
                model: Company.Name(),
                year: _random.Next(1886, DateTime.Now.Year),
                startingBid: _random.Next(0, 100000000),
                numberOfDoors: _random.Next(2, 5));
        }

        public static Truck CreateTruck()
        {
            return new Truck(
                id: Guid.NewGuid(),
                manufacturer: Company.Name(),
                model: Company.Name(),
                year: _random.Next(1886, DateTime.Now.Year),
                startingBid: _random.Next(0, 100000000),
                loadCapacity: _random.Next(0, 1000000).ToString());
        }
    }
}
