using Core.Entities;
using Core.Interfaces;
using Core.Parameters;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using Web.DTOs;

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
        public async Task<ActionResult<IEnumerable<EventDto>>> GetUpcomingEventsByVenueId(int id)
        {
            var events = await eventService.GetUpcomingEventsByVenueIdAsync(id);
            var eventDtos = events.Select(e => new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                About = e.About,
                Price = e.Price,
                ImageUrl = e.ImageUrl,
                StartDate = e.Register.Listing.StartDate,
                EndDate = e.Register.Listing.EndDate,
                TotalTickets = e.TotalTickets,
                AvailableTickets = e.AvailableTickets,
            }).ToList();
            return Ok(eventDtos);
        }

    }
}
