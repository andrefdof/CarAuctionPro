using FluentValidation;

namespace CarAuctionPro.Classes.Auction
{
    public class AuctionStateValidator : AbstractValidator<AuctionState>
    {
        public AuctionStateValidator(AuctionState currentState)
        {
            RuleFor(r => r)
                .Must(newState => newState != currentState)
                .WithMessage(r => $"Invalid state transition: Auction is already {currentState}.");
        }

    }

    public class BidValidator : AbstractValidator<decimal>
    {
        public BidValidator(AuctionState currentState)
        {
            RuleFor(r => r)
                .Must(_ => currentState == AuctionState.Started)
                .WithMessage($"Auction state must be started to be able to bid.")
                .GreaterThan(0)
                .WithMessage("Bid value must be greater than zero.");
        }
    }

    public class AuctionInventoryValidator : AbstractValidator<Guid>
    {
        public AuctionInventoryValidator(List<Auction> auctions)
        {
            RuleFor(r => r)
                .Must(r => auctions.Any(q => q.Vehicle.Id == r))
                .WithMessage(guid => $"Vehicle not present in auction inventory.");
        }
    }
}
