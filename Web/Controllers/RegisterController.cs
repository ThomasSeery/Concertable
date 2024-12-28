using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService registerService;

        public RegisterController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        [Authorize(Roles = "VenueManager")]
        [HttpGet("all/{listingId}")]
        public async Task<ActionResult<IEnumerable<Register>>> GetRegistrationsForListingId(int listingId)
        {
            var registrations = await registerService.GetRegistrationsForListingIdAsync(listingId);
            return Ok();
        }

        [Authorize(Roles = "ArtistManager")]
        [HttpPost("{listingId}")]
        public async Task<ActionResult> RegisterForListing(int listingId)
        {
            await registerService.RegisterForListing(listingId);
            return CreatedAtAction("", new { });
        }
    }
}
