using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchService.Contracts;
using SearchService.Entities;
using SearchService.Repositories;

namespace SearchService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchRepository _searchRepository;

    public SearchController(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ItemSearch>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        var (items, totalCount) = await _searchRepository.SearchAsync(searchParams);
        var pageCount = (int)Math.Ceiling((double)totalCount / searchParams.PageSize);

        return Ok(new
        {
            results = items,
            totalCount,
            pageCount,
        });
    }
}
