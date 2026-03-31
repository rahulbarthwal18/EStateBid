using Microsoft.EntityFrameworkCore;
using SearchService.Entities;

namespace SearchService.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    DbSet<ItemSearch> ItemSearches { get; set; }
}
