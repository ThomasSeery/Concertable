﻿using Core.Entities;
using Core.Interfaces;
using Core.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs;

namespace Web.Controllers
{
    [Authorize(Roles = "VenueManager, Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService venueService;

        public VenueController(IVenueService venueService)
        {
            this.venueService = venueService;
        }

        [HttpGet("headers")]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenueHeaders([FromQuery] VenueParams venueParams)
        {
            var venues = await venueService.GetVenueHeadersAsync(venueParams);
            return Ok(venues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenueDetailsById(int id)
        {
            return Ok(await venueService.GetVenueDetailsByIdAsync(id));
        }

        [HttpGet("user-venue")]
        public async Task<ActionResult<Venue?>> GetUserVenue()
        {
            return Ok(await venueService.GetUserVenueAsync(User));
        }

        [HttpPost]
        public async Task<ActionResult<Venue>> CreateVenue([FromBody] VenueDto venue)
        {
            return Ok(venueService.CreateVenueAsync());
        }
    }
}
