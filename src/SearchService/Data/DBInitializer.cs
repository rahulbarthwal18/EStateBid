using Microsoft.EntityFrameworkCore;
using SearchService.RequestHandler;

namespace SearchService.Data;

public class DBInitializer
{
    public static async Task Initialize(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        var auctionService = scope.ServiceProvider.GetRequiredService<AuctionServiceHttpClient>();
        var auctions = await auctionService.GeSearchProperties();
        if(auctions.Count > 0)
        {
            context.SearchProperties.AddRange(auctions);
            await context.SaveChangesAsync();
        }

    }
}
