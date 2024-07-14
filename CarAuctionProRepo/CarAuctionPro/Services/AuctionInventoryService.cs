using CarAuctionPro.Classes;
using CarAuctionPro.Classes.Auction;
using CarAuctionPro.Interfaces;

namespace CarAuctionPro.Services
{
    public class AuctionInventoryService
    {
        //In memory DB
        public List<Auction> Auctions { get;  set; } = [];

        public void AddAuction(Auction auction)
        {
            if (Auctions.Any(q => q.Vehicle.Id == auction.Vehicle.Id))
                throw new ArgumentException("Vehicle already in auction inventory.");

            Auctions.Add(auction);
        }

        // Search method to find vehicles by type, manufacturer, model, or year
        public IEnumerable<IVehicle> SearchVehicles( Type? vehicleType = null, string? manufacturer = null,
            string? model = null, int? year = null)
        {
            if (Auctions == null)
                return [];

            return Auctions
                .Where(auction => MatchesVehicle(auction.Vehicle, vehicleType, manufacturer, model, year))
                .Select(auction => auction.Vehicle).ToList();
        }

        private static bool MatchesVehicle(IVehicle vehicle, Type? vehicleType, string? manufacturer, string? model, int? year)
        {
            return (vehicleType == null || vehicle.GetType() == vehicleType) &&
                   (manufacturer == null || vehicle.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase)) &&
                   (model == null || vehicle.Model.Equals(model, StringComparison.OrdinalIgnoreCase)) &&
                   (!year.HasValue || vehicle.Year == year.Value);
        }
    }
}
