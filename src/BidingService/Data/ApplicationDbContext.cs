using Microsoft.EntityFrameworkCore;

namespace BidingService.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
