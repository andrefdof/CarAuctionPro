using CarAuctionPro.Classes.Auction;
using CarAuctionPro.Services;
using NUnit.Framework;
using CarAuctionPro.Classes.VehicleTypes;
using CarAuctionProTests.Factories;
using CarAuctionPro.Interfaces;

namespace CarAuctionProTests.ServicesTests
{
    [TestFixture]
    public class AuctionInventoryServiceTests
    {
        private AuctionInventoryService? _service;
        private Hatchback? _hatchback;
        private Truck? _truck;
        private Sedan? _sedan;
        private Suv? _suv;

        [SetUp]
        public void SetUp()
        {
            _service = new AuctionInventoryService();
            _hatchback = VehicleFactory.CreateHatchback();
            _suv = VehicleFactory.CreateSuv();
            _sedan = VehicleFactory.CreateSedan();
            _truck = VehicleFactory.CreateTruck();
        }

        [Test]
        public void AddAuction_ShouldAddAuction_WhenVehicleIsNew()
        {
            var auction = new Auction(_suv!);

            _service?.AddAuction(auction);

            Assert.That(_service?.Auctions.Count, Is.EqualTo(1));
            Assert.That(_service?.Auctions[0].Vehicle.Id, Is.EqualTo(_suv!.Id));
        }

        [Test]
        public void AddAuction_ShouldThrowArgumentException_WhenVehicleAlreadyExists()
        {
            var auction = new Auction(_truck!);

            _service?.AddAuction(auction);

            var ex = Assert.Throws<ArgumentException>(() => _service?.AddAuction(auction));
            Assert.That(ex?.Message, Is.EqualTo("Vehicle already in auction inventory."));
        }

        [Test]
        public void SearchVehicles_ShouldReturnCorrectVehicles_WhenCriteriaMatch()
        {
            var testManufacturer = "Mazda";
            var mazda1Id = Guid.NewGuid();
            var mazda2Id = Guid.NewGuid();
            var mazda3Id = Guid.NewGuid();

            var vehicleList = new List<IVehicle> { _truck!, _hatchback!, _suv!, _sedan!,
                                    VehicleFactory.CreateSuv( id: mazda1Id, manufacturer: testManufacturer),
                                    VehicleFactory.CreateSuv( id: mazda2Id, manufacturer: testManufacturer),
                                    VehicleFactory.CreateSuv( id: mazda3Id, manufacturer: testManufacturer)};

            vehicleList.ForEach(vehicle => _service?.AddAuction(new Auction(vehicle)));
           
            var result = _service?.SearchVehicles(manufacturer: testManufacturer);

            Assert.That(result!.Count, Is.EqualTo(3));
            Assert.That(result!.Any(r => r.Id == mazda1Id), Is.True);
            Assert.That(result!.Any(r => r.Id == mazda2Id), Is.True);
            Assert.That(result!.Any(r => r.Id == mazda3Id), Is.True);

            Assert.That(result!.All(vehicle => vehicle.Manufacturer == testManufacturer), Is.True);
        }

        [Test]
        public void SearchVehicles_ShouldReturnAllVehicles_WhenNoCriteriaGiven()
        {
            var vehicleList = new List<IVehicle> { _truck!, _hatchback!, _suv!, _sedan! };

            vehicleList.ForEach(vehicle => _service?.AddAuction(new Auction(vehicle)));

            var result = _service!.SearchVehicles();

            Assert.That(result!.Count, Is.EqualTo(4));
        }

        [Test]
        public void SearchVehicles_ShouldReturnEmpty_WhenNoVehiclesMatchCriteria()
        {
            var vehicleList = new List<IVehicle> { _truck!, _hatchback!, _suv!, _sedan! };

            vehicleList.ForEach(vehicle => _service?.AddAuction(new Auction(vehicle)));

            var result = _service?.SearchVehicles(manufacturer: "Honda");

            Assert.That(result!.Count, Is.EqualTo(0));
        }
    }
}
