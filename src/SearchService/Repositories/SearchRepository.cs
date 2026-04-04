using Microsoft.EntityFrameworkCore;
using SearchService.Contracts;
using SearchService.Data;
using SearchService.Entities;

namespace SearchService.Repositories;

public class SearchRepository : ISearchRepository
{
    private readonly ApplicationDbContext _context;

    public SearchRepository(ApplicationDbContext context)
    {
        _context = context;
    }
  
    public async Task<(List<ItemSearch> Items, int TotalCount)> SearchAsync(SearchParams searchParams)
    {
        var query = _context.SearchProperties.AsQueryable();

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query = query.Where(i => i.Title.Contains(searchParams.SearchTerm)
                || i.Description.Contains(searchParams.SearchTerm)
                || i.City.Contains(searchParams.SearchTerm));
        }

        query = searchParams.OrderBy switch
        {
            "price" => query.OrderBy(i => i.ReservePrice),
            "priceDesc" => query.OrderByDescending(i => i.ReservePrice),
            "new" => query.OrderByDescending(i => i.CreatedAt),
            "city" => query.OrderBy(i => i.City),
            _ => query.OrderBy(i => i.AuctionEnd)
        };

        if (!string.IsNullOrEmpty(searchParams.FilterBy))
        {
            query = searchParams.FilterBy switch
            {
                "finished" => query.Where(i => i.AuctionEnd > DateTime.UtcNow),
                "endingSoon" => query.Where(i => i.AuctionEnd < DateTime.UtcNow.AddHours(6)
                    && i.AuctionEnd > DateTime.UtcNow),
                _ => query.Where(i => i.AuctionEnd > DateTime.UtcNow)
            };
        }

        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query = query.Where(i => i.Seller == searchParams.Seller);
        }

        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query = query.Where(i => i.Winner == searchParams.Winner);
        }

        var totalCount = await query.CountAsync();
        var items = await query.Skip((searchParams.PageNumber - 1) * searchParams.PageSize)
            .Take(searchParams.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}
