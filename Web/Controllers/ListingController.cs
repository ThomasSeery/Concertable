using Core.Entities;
using Core.Interfaces;
using Core.Parameters;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly IListingService listingService;
        public ListingController(IListingService listingService)
        {
            this.listingService = listingService;
        }

        [HttpGet("active/venue/{id}")]
        public async Task<ActionResult<IEnumerable<ListingDto>>> GetActiveListingsByVenueId(int id)
        {
            var listings = await listingService.GetActiveListingsByVenueIdAsync(id);
            var listingDtos = listings.Select(l => new ListingDto
            {
                Id = l.Id,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Pay = l.Pay,
                Genres = l.ListingGenres.Select(g => g.Genre.Name).ToList()
            });
            return Ok(listingDtos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateListing([FromBody] ListingDto listingDto)
        {
            var listing = new Listing()
            {
                StartDate = listingDto.StartDate,
                EndDate = listingDto.EndDate,
                Pay = listingDto.Pay,
            };
            listingService.CreateListing(listing);
            return CreatedAtAction("", new {Id = 1});
        }
    }
}
