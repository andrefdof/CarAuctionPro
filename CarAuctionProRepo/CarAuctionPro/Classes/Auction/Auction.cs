using CarAuctionPro.Interfaces;
using CarAuctionPro.Utilities.Helpers;
using FluentValidation;

namespace CarAuctionPro.Classes.Auction
{
    public class Auction
    {
        private AuctionState state;
        public IVehicle Vehicle { get; set; }
        private decimal highestBid = 0;

        public Auction(IVehicle vehicle) 
        {
            Vehicle = vehicle; 
        }

        #region methods
        public void PlaceBid(decimal bid)
        {
            bid.Validate(new BidValidator(state));

            if (bid > highestBid)
                highestBid = bid;
        }

        public void StartAuction(List<Auction> auctions)
        {
            //Validate State
            AuctionState.Started.Validate(new AuctionStateValidator(state));

            //Validate inventory
            Vehicle.Id.Validate(new AuctionInventoryValidator(auctions));
            state = AuctionState.Started;
        }

        public void StopAuction()
        {
            AuctionState.Stopped.Validate(new AuctionStateValidator(state));
            state = AuctionState.Stopped;
        }

        public string GetState() => state.ToString();

        public decimal GetHighestBid() => highestBid;

        public void CancelAuction()
        {
            if (state == AuctionState.Started)
                state = AuctionState.Stopped;

            highestBid = 0;
        }

        #endregion
    }

    public enum AuctionState
    {
        Stopped,
        Started
    }
}
