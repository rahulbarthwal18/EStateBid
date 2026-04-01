using AuctionService.Dtos;
using AuctionService.Extensions;
using AuctionService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuctionsController : ControllerBase
{
    private readonly IAuctionRepository _auctionRepository;

    public AuctionsController(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAuctions()
    {
        var auctions = await _auctionRepository.GetAllAsync();
        return Ok(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuction(Guid id)
    {
        var auctions = await _auctionRepository.GetAuctionByIdAsync(id);
        return Ok(auctions);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto dto)
    {
        var auction = dto.ToAuctionEntity();
         _auctionRepository.AddAuction(auction);
        var results = await _auctionRepository.SaveChangesAsync();
        if(!results)
            return BadRequest("Failed to save auction");
        return CreatedAtAction(nameof(GetAuction), new { id = auction.Id }, auction.ToAuctionDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AuctionDto>> UpdateAuctionAsync(Guid id,UpdateAuctionDto dto)
    {
        var auction = await _auctionRepository.GetAuctionByIdAsync(id);
        if (auction == null)
            return BadRequest();
        auction.ToUpdateAuction(dto);
        await _auctionRepository.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _auctionRepository.GetAuctionByIdAsync(id);
        if (auction == null)
            return BadRequest();
        _auctionRepository.RemoveAuction(auction);
        await _auctionRepository.SaveChangesAsync();
        return NoContent();
    }
}