using Core.Entities;
using Core.Interfaces;
using Core.Parameters;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService) 
        {
            this.eventService = eventService;
        }

        [HttpGet("venue/{id}")]
        public async Task<ActionResult<IEnumerable<Venue>>> GetUpcomingEventsByVenueId(int id)
        {
            var venueEvents = await eventService.GetUpcomingEventsByVenueIdAsync(id);
            return Ok(venueEvents);
        }

    }
}
