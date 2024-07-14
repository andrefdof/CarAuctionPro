using CarAuctionPro.Classes.VehicleTypes;
using CarAuctionProTests.Factories;
using NUnit.Framework;

namespace CarAuctionProTests.ClassesTests
{
    [TestFixture]
    public class TruckTests
    {
        private Truck? _truck;

        [SetUp]
        public void Setup()
        {
            _truck = VehicleFactory.CreateTruck();
        }

        [Test]
        public void Constructor_ValidParameters_ShouldInitializeProperties()
        {
            _truck!.LoadCapacity = "1200";

            // Rest of properties tested in VehicleTests
            Assert.That(_truck.LoadCapacity, Is.EqualTo("1200"));
        }

        [Test]
        public void NumberOfDoors_SetInvalidValue_ShouldThrowException()
        {
            var invalidLoadCapacity = ""; 

            var ex = Assert.Throws<ArgumentException>(() => _truck!.LoadCapacity = invalidLoadCapacity);
            Assert.That(ex?.Message, Is.EqualTo($"Validation failed: Load Capacity cannot be empty."));

            invalidLoadCapacity = null;

            ex = Assert.Throws<ArgumentNullException>(() => _truck!.LoadCapacity = invalidLoadCapacity!);
            Assert.That(ex?.Message, Is.EqualTo($"Value cannot be null. (Parameter 'loadCapacity')"));

            invalidLoadCapacity = "invalid decimal number";
            ex = Assert.Throws<ArgumentException>(() => _truck!.LoadCapacity = invalidLoadCapacity!);
            Assert.That(ex?.Message, Is.EqualTo($"Validation failed: Load capacity must be a number."));

            invalidLoadCapacity = "-1";
            ex = Assert.Throws<ArgumentException>(() => _truck!.LoadCapacity = invalidLoadCapacity!);
            Assert.That(ex?.Message, Is.EqualTo($"Validation failed: Load capacity must be a number between 0 and 1000000."));

            invalidLoadCapacity = "1000000000";
            ex = Assert.Throws<ArgumentException>(() => _truck!.LoadCapacity = invalidLoadCapacity!);
            Assert.That(ex?.Message, Is.EqualTo($"Validation failed: Load capacity must be a number between 0 and 1000000."));
        }

        [Test]
        public void GetVehicleTypeDetails_ShouldReturnCorrectString()
        {
            _truck!.LoadCapacity = "5200";

            var result = _truck.GetVehicleTypeDetails();
            Assert.That(result, Is.EqualTo("Load Capacity = 5200"));
        }
    }
}
