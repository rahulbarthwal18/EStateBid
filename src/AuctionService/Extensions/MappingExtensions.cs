using AuctionService.Dtos;
using AuctionService.Entities;
using SharedMicro;

namespace AuctionService.Extensions;

public static class MappingExtensions
{
    public static AuctionDto ToAuctionDto(this Auction auction)
    {
        return new AuctionDto
        {
            Id = auction.Id,
            ReservePrice = auction.ReservePrice,
            Seller = auction.Seller,
            Winner = auction.Winner,
            SoldAmount = auction.SoldAmount,
            CurrentHighBid = auction.CurrentHighBid,
            CreatedAt = auction.CreatedAt,
            UpdatedAt = auction.UpdatedAt,
            AuctionEnd = auction.AuctionEnd,
            Status = auction.Status,
            Title = auction.Property.Title,
            Description = auction.Property.Description,
            Address = auction.Property.Address,
            City = auction.Property.City,
            State = auction.Property.State,
            Bedrooms = auction.Property.Bedrooms,
            Bathrooms = auction.Property.Bathrooms,
            AreaSqFt = auction.Property.AreaSqFt,
            ImageUrl = auction.Property.ImageUrl
        };
    }

    public static Auction ToAuctionEntity(this CreateAuctionDto createAuctionDto)
    {
        return new Auction
        {
            ReservePrice = createAuctionDto.ReservePrice,
            Seller = "demo",
            AuctionEnd = createAuctionDto.AuctionEnd,
            Status = AuctionStatus.Live,
            Property = new Property
            {
                Title = createAuctionDto.Title,
                Description = createAuctionDto.Description,
                Address = createAuctionDto.Address,
                City = createAuctionDto.City,
                State = createAuctionDto.State,
                Bedrooms = createAuctionDto.Bedrooms,
                Bathrooms = createAuctionDto.Bathrooms,
                AreaSqFt = createAuctionDto.AreaSqFt,
                StartingPrice = createAuctionDto.ReservePrice,
                ImageUrl = createAuctionDto.ImageUrl
            }
        };
    }

    public static void ToUpdateAuction(this Auction auction,UpdateAuctionDto updateAuctionDto)
    {
       auction.UpdatedAt = DateTime.UtcNow;
       auction.Property.Title = updateAuctionDto.Title ?? auction.Property.Title;
       auction.Property.Description = updateAuctionDto.Description ?? auction.Property.Description;
       auction.Property.Address = updateAuctionDto.Address ?? auction.Property.Address;
       auction.Property.City = updateAuctionDto.City ?? auction.Property.City;
       auction.Property.State = updateAuctionDto.State ?? auction.Property.State;
       auction.Property.Bedrooms = updateAuctionDto.Bedrooms;
       auction.Property.Bathrooms = updateAuctionDto.Bathrooms;
       auction.Property.AreaSqFt = updateAuctionDto.AreaSqFt;
       auction.Property.ImageUrl = updateAuctionDto.ImageUrl ?? string.Empty;
    }
    public static AuctionCreated ToAuctionCreated(this Auction auction)
    {
        return new AuctionCreated
        {
            Id = auction.Id,
            ReservePrice = auction.ReservePrice,
            Seller = auction.Seller,
            Winner = auction.Winner,
            SoldAmount = auction.SoldAmount,
            CurrentHighBid = auction.CurrentHighBid,
            CreatedAt = auction.CreatedAt,
            UpdatedAt = auction.UpdatedAt,
            AuctionEnd = auction.AuctionEnd,
            Status = auction.Status.ToString(),
            Title = auction.Property.Title,
            Description = auction.Property.Description,
            Address = auction.Property.Address,
            City = auction.Property.City,
            State = auction.Property.State,
            Bedrooms = auction.Property.Bedrooms,
            Bathrooms = auction.Property.Bathrooms,
            AreaSqFt = auction.Property.AreaSqFt,
            ImageUrl = auction.Property.ImageUrl
        };
    }
}

