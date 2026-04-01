namespace AuctionService.Dtos;

public class UpdateAuctionDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public int Bedrooms { get; set; }
    public decimal Bathrooms { get; set; }
    public decimal AreaSqFt { get; set; }
    public string? ImageUrl { get; set; }
}
