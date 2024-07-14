using NUnit.Framework;
using CarAuctionPro.Classes.VehicleTypes;
using CarAuctionPro.Interfaces;
using CarAuctionProTests.Factories;
using Faker;

namespace CarAuctionProTests.ClassesTests
{
    [TestFixture]
    public class VehicleTests
    {
        private Hatchback? _hatchback;
        private Truck? _truck;
        private Sedan? _sedan;
        private Suv? _suv;

        [SetUp]
        public void Setup()
        {
            _hatchback = new Hatchback( Guid.NewGuid(), "Toyota", "Corolla", 2000, 15000m, 4);

            _suv = new Suv( Guid.NewGuid(), "Ford", "Explorer", 2002, 30000m, 7);

            _sedan = new Sedan( Guid.NewGuid(), "Honda", "Accord", 2021, 20000m, 4);

            _truck = new Truck( Guid.NewGuid(), "Chevrolet", "Silverado", 1988, 40000m, "1110");
        }

        [Test]
        public void Constructor_ValidParameters_ShouldInitializeProperties()
        {
            var vehicles = new List<IVehicle> { _hatchback!, _suv!, _sedan!, _truck! };

            var expectedValues = new Dictionary<IVehicle, (string Manufacturer, string Model, int Year, decimal StartingBid)>
            {
                { _hatchback!, ("Toyota", "Corolla", 2000, 15000m) },
                { _suv!, ("Ford", "Explorer", 2002, 30000m) },
                { _sedan!, ("Honda", "Accord", 2021, 20000m) },
                { _truck!, ("Chevrolet", "Silverado", 1988, 40000m) }
            };

            // Assert that the properties are initialized correctly for the abstract class Vehicle
            vehicles.ForEach(vehicle => { 
                Assert.That(vehicle.Id, Is.Not.Null);

                var (expectedManufacturer, expectedModel, expectedYear, expectedStartingBid) = expectedValues[vehicle];
                Assert.That(vehicle.Manufacturer, Is.EqualTo(expectedManufacturer));
                Assert.That(vehicle.Model, Is.EqualTo(expectedModel));
                Assert.That(vehicle.Year, Is.EqualTo(expectedYear));
                Assert.That(vehicle.StartingBid, Is.EqualTo(expectedStartingBid));
            });
        }

        [Test]
        [TestCase("Hatchback")]
        [TestCase("Suv")]
        [TestCase("Truck")]
        [TestCase("Sedan")]
        public void Manufacturer_SetInvalidValue_ShouldThrow(string vehicleType)
        {
            IVehicle vehicle = CreateVehicle(vehicleType);

            var invalidManufacturer = "";

            var ex = Assert.Throws<ArgumentException>(() => vehicle.Manufacturer = invalidManufacturer);

            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Manufacturer cannot be empty."));

            invalidManufacturer = null;

            ex = Assert.Throws<ArgumentNullException>(() => vehicle.Manufacturer = invalidManufacturer!);

            Assert.That(ex?.Message, Is.EqualTo($"Value cannot be null. (Parameter 'manufacturer')"));

            invalidManufacturer = Lorem.Sentence(40);

            ex = Assert.Throws<ArgumentException>(() => vehicle.Manufacturer = invalidManufacturer);

            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Manufacturer cannot exceed 50 characters."));

        }


        [Test]
        [TestCase("Hatchback")]
        [TestCase("Suv")]
        [TestCase("Truck")]
        [TestCase("Sedan")]
        public void Model_SetInvalidValue_ShouldThrow(string vehicleType)
        {
            IVehicle vehicle = CreateVehicle(vehicleType);

            var invalidModel = "";

            var ex = Assert.Throws<ArgumentException>(() => vehicle.Model = invalidModel);

            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Model cannot be empty."));

            invalidModel = null;

            ex = Assert.Throws<ArgumentNullException>(() => vehicle.Model = invalidModel!);

            Assert.That(ex?.Message, Is.EqualTo($"Value cannot be null. (Parameter 'model')"));

            invalidModel = Lorem.Sentence(40);

            ex = Assert.Throws<ArgumentException>(() => vehicle.Model = invalidModel);

            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Model cannot exceed 50 characters."));
        }

        [Test]
        [TestCase("Hatchback")]
        [TestCase("Suv")]
        [TestCase("Truck")]
        [TestCase("Sedan")]
        public void Year_SetInvalidValue_ShouldThrow(string vehicleType)
        {
            IVehicle vehicle = CreateVehicle(vehicleType);

            var invalidYear = DateTime.Now.Year +1; // From the future

            var ex = Assert.Throws<ArgumentException>(() => vehicle.Year = invalidYear);

            Assert.That(ex?.Message, Is.EqualTo($"Validation failed: Year must be between 1886 and {DateTime.Now.Year}."));

            invalidYear = 1885; // The first car was made in 1886

            ex = Assert.Throws<ArgumentException>(() => vehicle.Year = invalidYear);

            Assert.That(ex?.Message, Is.EqualTo($"Validation failed: Year must be between 1886 and {DateTime.Now.Year}."));
        }

        [Test]
        [TestCase("Hatchback")]
        [TestCase("Suv")]
        [TestCase("Truck")]
        [TestCase("Sedan")]
        public void StartingYear_SetInvalidValue_ShouldThrow(string vehicleType)
        {
            IVehicle vehicle = CreateVehicle(vehicleType);

            var invalidStartingBid = -1;

            var ex = Assert.Throws<ArgumentException>(() => vehicle.StartingBid = invalidStartingBid);

            Assert.That(ex?.Message, Is.EqualTo($"Validation failed: Value must be a non-negative number."));
        }


        private static IVehicle CreateVehicle(string vehicleType)
        {
            return vehicleType switch
            {
                "Hatchback" => VehicleFactory.CreateHatchback(),
                "Suv" => VehicleFactory.CreateSuv(),
                "Truck" => VehicleFactory.CreateTruck(),
                "Sedan" => VehicleFactory.CreateSedan(),
                _ => throw new ArgumentException("Invalid vehicle type")
            };
        }
    }
}
