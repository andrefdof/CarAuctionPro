using CarAuctionPro.Classes.VehicleTypes;
using CarAuctionProTests.Factories;
using NUnit.Framework;

namespace CarAuctionProTests.ClassesTests
{
    [TestFixture]
    public class SedanTests
    {
        private Sedan? _sedan;

        [SetUp]
        public void Setup()
        {
            _sedan = VehicleFactory.CreateSedan();
        }

        [Test]
        public void Constructor_ValidParameters_ShouldInitializeProperties()
        {
            _sedan!.NumberOfDoors = 3;

            // Rest of properties tested in VehicleTests
            Assert.That(_sedan.NumberOfDoors, Is.EqualTo(3));
        }

        [Test]
        public void NumberOfDoors_SetInvalidValue_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _sedan!.NumberOfDoors = 1);
            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Sedan number of doors must be a number between 2 and 5."));
        }

        [Test]
        public void GetVehicleTypeDetails_ShouldReturnCorrectString()
        {
            _sedan!.NumberOfDoors = 4;

            var result = _sedan.GetVehicleTypeDetails();
            Assert.That(result, Is.EqualTo("Number of doors = 4"));
        }
    }
}
