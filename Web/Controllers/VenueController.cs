using Concertible.Entities;
using Core.Interfaces;
using Core.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize(Roles = "Leaseholder, VenueOwner")]
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService venueService;

        public VenueController(IVenueService venueService)
        {
            this.venueService = venueService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenues([FromQuery] VenueParams venueParams)
        {
            return Ok(venueService.GetVenuesAsync(venueParams));
        }
    }
}
