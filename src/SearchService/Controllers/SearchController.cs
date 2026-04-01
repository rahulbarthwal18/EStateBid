using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchService.Contracts;
using SearchService.Data;
using SearchService.Entities;

namespace SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<ItemSearch>>> SearchItems([FromQuery] SearchParams searchParams)
        {
            var query = _context.SearchProperties.AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query = query.Where(i => i.Title.Contains(searchParams.SearchTerm)
                || i.Description.Contains(searchParams.SearchTerm)
                || i.City.Contains(searchParams.SearchTerm));
            }
            query  = searchParams.OrderBy switch
            {
                "price" => query.OrderBy(i => i.ReservePrice),
                "priceDesc" => query.OrderByDescending(i => i.ReservePrice),
                "new" => query.OrderByDescending(i => i.CreatedAt),
                "city" => query.OrderBy(i => i.City),
                _ => query.OrderBy(i => i.AuctionEnd)
            };
            if(!string.IsNullOrEmpty(searchParams.FilterBy))
            {
                query = searchParams.FilterBy switch
                {
                    "finished" => query.Where(i => i.AuctionEnd > DateTime.UtcNow),
                    "endingSoon" => query.Where(i => i.AuctionEnd < DateTime.UtcNow.AddHours(6)
                    && i.AuctionEnd > DateTime.UtcNow),
                    _ => query.Where(i => i.AuctionEnd > DateTime.UtcNow)
                };
            }
            if(!string.IsNullOrEmpty(searchParams.Seller))
            {
                query = query.Where(i => i.Seller == searchParams.Seller);
            }
            if(!string.IsNullOrEmpty(searchParams.Winner))
            {
                query = query.Where(i => i.Winner == searchParams.Winner);
            }
            var totalCount = await query.CountAsync();
            var items = await query.Skip((searchParams.PageNumber - 1) * searchParams.PageSize)
                .Take(searchParams.PageSize)
                .ToListAsync();
            var pageCount = (int)Math.Ceiling((double)totalCount / searchParams.PageSize);

            return Ok(new
            {
                results = items, 
                totalCount,
                pageCount,
            });
        }
    }
}
