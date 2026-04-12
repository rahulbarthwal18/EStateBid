using AuctionService.Data;
using AuctionService.Data;
using AuctionService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedMicro;

namespace AuctionService.Consumers;

public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    private readonly ApplicationDbContext _dbContext;

    public AuctionCreatedFaultConsumer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        var auctionId = context.Message.Message.Id;

        // Include related Property data to ensure cascade delete works properly
        var auction = await _dbContext.Auctions
            .Include(a => a.Property)
            .FirstOrDefaultAsync(a => a.Id == auctionId);

        if (auction == null)
        {
            return;
        }

        // Rollback: Delete the auction and its associated property from the database
        // This ensures data consistency if SearchService failed to process the message
        _dbContext.Auctions.Remove(auction);
        await _dbContext.SaveChangesAsync();
    }
}
