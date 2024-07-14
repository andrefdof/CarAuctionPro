using CarAuctionPro.Classes.VehicleTypes;
using CarAuctionProTests.Factories;
using NUnit.Framework;

namespace CarAuctionProTests.ClassesTests
{
    [TestFixture]
    public class SuvTests
    {
        private Suv? _suv;

        [SetUp]
        public void Setup()
        {
            _suv = VehicleFactory.CreateSuv();
        }

        [Test]
        public void Constructor_ValidParameters_ShouldInitializeProperties()
        {
            _suv!.NumberOfSeats = 3;

            // Rest of properties tested in VehicleTests
            Assert.That(_suv.NumberOfSeats, Is.EqualTo(3));
        }

        [Test]
        public void NumberOfDoors_SetInvalidValue_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _suv!.NumberOfSeats = 11);
            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Suv number of seats must be a number between 1 and 10."));
        }

        [Test]
        public void GetVehicleTypeDetails_ShouldReturnCorrectString()
        {
            _suv!.NumberOfSeats = 4;

            var result = _suv.GetVehicleTypeDetails();
            Assert.That(result, Is.EqualTo("Number of seats = 4"));
        }
    }
}
