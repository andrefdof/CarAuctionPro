using NUnit.Framework;
using CarAuctionPro.Classes.VehicleTypes;
using CarAuctionProTests.Factories;

namespace CarAuctionProTests.ClassesTests
{
    [TestFixture]
    public class HatchbackTests
    {
        private Hatchback? _hatchback;

        [SetUp]
        public void Setup()
        {
            _hatchback = VehicleFactory.CreateHatchback();
        }

        [Test]
        public void Constructor_ValidParameters_ShouldInitializeProperties()
        {
            _hatchback!.NumberOfDoors = 3;

            // Rest of properties tested in VehicleTests
            Assert.That(_hatchback.NumberOfDoors, Is.EqualTo(3));
        }

        [Test]
        public void NumberOfDoors_SetInvalidValue_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _hatchback!.NumberOfDoors = 1);
            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Hatchback number of doors must be a number between 2 and 5."));
        }

        [Test]
        public void GetVehicleTypeDetails_ShouldReturnCorrectString()
        {
            _hatchback!.NumberOfDoors = 4;

            var result = _hatchback.GetVehicleTypeDetails();
            Assert.That(result, Is.EqualTo("Number of doors = 4"));
        }
    }
}
