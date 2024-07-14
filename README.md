# Car Auction Pro

This was develloped as an interview requirement for a job applycation.

It represents a BackEnd C# code for a car auction system.

[System Description and Requirements](../CarAuctionPro/CarAuctionProRepo/Media/Problem%20Statement%20Car%20Auction%20Manag.md)

# Assumptions

- `VehicleId` is a GUID.
- `Model` is a string that cannot be null or empty, with a maximum character length of 50.
- `Manufacturer` is a string that cannot be null or empty, with a maximum character length of 50.
- `Year` is a DateTime value between 1886 and the current year, inclusive.
- `StartingBid` is an integer that cannot be less than 0.
- `LoadCapacity` for a truck is a string that must represent a number between 1 and 1,000,000.
- `NumberOfDoors` for a sedan is an integer value between 1 and 6.
- The `AuctionState` property of an auction object is an enum that always starts as `Stopped` and can be changed afterward. It cannot be changed to the same value it already had.
- The `HighestBid` property of an auction object starts with a value of 0.

# Design
![](/CarAuctionProRepo/Media/Design.jpg)