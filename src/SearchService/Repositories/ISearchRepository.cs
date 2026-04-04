using SearchService.Contracts;
using SearchService.Entities;

namespace SearchService.Repositories;

public interface ISearchRepository
{
    Task<(List<ItemSearch> Items, int TotalCount)> SearchAsync(SearchParams searchParams);
}
