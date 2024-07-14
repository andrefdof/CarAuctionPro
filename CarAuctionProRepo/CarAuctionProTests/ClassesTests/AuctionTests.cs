using CarAuctionPro.Classes.Auction;
using CarAuctionPro.Interfaces;
using Moq;
using NUnit.Framework;

namespace CarAuctionProTests.ClassesTests
{
    [TestFixture]
    public class AuctionTests
    {
        private Mock<IVehicle>? mockVehicle;
        private Auction? _auction;
        private readonly Guid _auctionId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(v => v.Id).Returns(_auctionId); // Set up vehicle ID for testing
            _auction = new Auction(mockVehicle.Object);
        }

        [Test]
        [TestCase (AuctionState.Stopped)]
        [TestCase(AuctionState.Started)]
        public void StartOrStopAuction_ShouldThrowArgumentException_AuctionAlreadyStartedOrStopped(AuctionState auctionState)
        {
            var auctionList = new List<Auction> { _auction! };
            var stringEx = "";

            if (auctionState == AuctionState.Started)
                _auction?.StartAuction(auctionList);

            stringEx = auctionState == AuctionState.Started ?
                Assert.Throws<ArgumentException>(() => _auction?.StartAuction(auctionList))?.Message :
                Assert.Throws<ArgumentException>(() => _auction?.StopAuction())?.Message;

            Assert.That(stringEx, Is.EqualTo($"Validation failed: Invalid state transition: Auction is already {auctionState}."));
        }

        [Test]
        public void StartAuction_ShouldThrowArgumentException_CarNotInInventory()
        {
            var ex = Assert.Throws<ArgumentException>(() => _auction?.StartAuction(new List<Auction>()));
            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Vehicle not present in auction inventory."));
        }

        [Test]
        public void StartAuction_ValidConditions_ChangesStateToStarted()
        {
            var auctionList = new List<Auction> { _auction! };
            _auction?.StartAuction(auctionList);

            Assert.That(_auction!.GetState(), Is.EqualTo(AuctionState.Started.ToString()));
        }

        [Test]
        public void StopAuction_ValidConditions_ChangesStateToStopped()
        {
            var auctionList = new List<Auction> { _auction! };
            _auction?.StartAuction(auctionList);

            _auction?.StopAuction();

            Assert.That(_auction!.GetState(), Is.EqualTo(AuctionState.Stopped.ToString()));
        }

        [Test]
        public void PlaceBid_ShouldThrowArgumentException_AuctionNotStarted()
        {
            var bid = 100;

            var ex = Assert.Throws<ArgumentException>(() => _auction?.PlaceBid(bid));
            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Auction state must be started to be able to bid."));
        }

        [Test]
        public void PlaceBid_ShouldThrowArgumentException_BidValueInvalid()
        {
            var auctionList = new List<Auction> { _auction! };
            _auction?.StartAuction(auctionList);

            var bid = 0;

            var ex = Assert.Throws<ArgumentException>(() => _auction?.PlaceBid(bid));
            Assert.That(ex?.Message, Is.EqualTo("Validation failed: Bid value must be greater than zero."));
        }

        [Test]
        public void PlaceBid_LowerBid_DoesNotChangeHighestBid()
        {
            var auctionList = new List<Auction> { _auction! };
            _auction?.StartAuction(auctionList);

            var bigBid = 1000;
            var smallBid = 999;

            _auction?.PlaceBid(bigBid);
            _auction?.PlaceBid(smallBid);

            Assert.That(_auction!.GetHighestBid(), Is.EqualTo(bigBid));
        }

        [Test]
        public void PlaceBid_ValidBid_SetsHighestBid()
        {
            var auctionList = new List<Auction> { _auction! };
            _auction?.StartAuction(auctionList);

            var bid = 1000;
            _auction?.PlaceBid(bid);

            Assert.That(_auction!.GetHighestBid(), Is.EqualTo(bid));
        }

        [Test]
        public void CancelAuction_StartedAuction_ResetsStateAndBid()
        {
            var auctionList = new List<Auction> { _auction! };
            _auction?.StartAuction(auctionList);

            var bid = 1000;
            _auction?.PlaceBid(bid);

            _auction?.CancelAuction();

            Assert.That(_auction?.GetHighestBid(), Is.EqualTo(0));
            Assert.That(_auction?.GetState(), Is.EqualTo(AuctionState.Stopped.ToString()));
        }

        [Test]
        public void CancelAuction_StoppedAuction_ResetsOnlyBid()
        {
            var auctionList = new List<Auction> { _auction! };
            _auction?.StartAuction(auctionList);

            var bid = 1000;
            _auction?.PlaceBid(bid);
            _auction?.StopAuction();

            _auction?.CancelAuction();

            Assert.That(_auction?.GetHighestBid(), Is.EqualTo(0));
            Assert.That(_auction?.GetState(), Is.EqualTo(AuctionState.Stopped.ToString()));
        }
    }
}
