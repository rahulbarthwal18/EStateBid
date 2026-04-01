using SearchService.Entities;

namespace SearchService.RequestHandler;

public class AuctionServiceHttpClient
{
    private readonly HttpClient _httpClient;
    private IConfiguration _configuration;

    public AuctionServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    public async Task<List<ItemSearch>> GeSearchProperties()
    {
        var baseUrl = _configuration.GetValue<string>("AuctionServiceBaseUrl");
        var auctionServiceUrl = $"{baseUrl}/api/auctions";
        var response = await _httpClient.GetFromJsonAsync<List<ItemSearch>>(auctionServiceUrl);
        return response ?? new List<ItemSearch>();
    }
}
