namespace CarAuctionPro.Interfaces
{
    public interface IVehicle
    {
        Guid Id { get; set; }
        string Manufacturer { get; set; }
        string Model { get; set; }
        int Year { get; set; }
        decimal StartingBid { get; set; }
        string GetVehicleTypeDetails();
    }
}
