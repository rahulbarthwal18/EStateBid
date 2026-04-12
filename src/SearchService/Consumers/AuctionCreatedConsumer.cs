using MassTransit;
using SearchService.Data;
using SearchService.Entities;
using SharedMicro;

namespace SearchService.Consumers;

public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{
    private readonly ApplicationDbContext _dbContext;

    public AuctionCreatedConsumer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        var message = context.Message;
        if(_dbContext.SearchProperties.Any(p => p.Id == message.Id))
        {
            return;
        }
        var itemSearch = new ItemSearch
        {
            Id = message.Id,
            ReservePrice = message.ReservePrice,
            Seller = message.Seller,
            Winner = message.Winner,
            SoldAmount = message.SoldAmount,
            CurrentHighBid = message.CurrentHighBid,
            CreatedAt = message.CreatedAt,
            UpdatedAt = message.UpdatedAt,
            AuctionEnd = message.AuctionEnd,
            Title = message.Title,
            Description = message.Description,
            Address = message.Address,
            City = message.City,
            State = message.State,
            Bedrooms = message.Bedrooms,
            Bathrooms = message.Bathrooms,
            AreaSqFt = message.AreaSqFt,
            ImageUrl = message.ImageUrl
        };
        await _dbContext.SearchProperties.AddAsync(itemSearch);
        await _dbContext.SaveChangesAsync();
    }
}
