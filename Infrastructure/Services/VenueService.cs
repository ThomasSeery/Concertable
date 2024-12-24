using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Parameters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository venueRepository;
        private readonly IAuthService authService;

        public VenueService(IVenueRepository venueRepository, IAuthService authService)
        {
            this.venueRepository = venueRepository;
            this.authService = authService;
        }

        public async Task<IEnumerable<Venue>> GetVenueHeadersAsync(VenueParams? venueParams)
        {
            return await venueRepository.GetHeadersAsync(venueParams);
        }

        public async Task<Venue> GetVenueDetailsByIdAsync(int id)
        {
            return await venueRepository.GetByIdAsync(id);
        }

        public void CreateVenue(Venue venue)
        {
            var user = authService.GetCurrentUser();
            venue.UserId = user.Id;

            venueRepository.Add(venue);
        }

        public async Task<Venue?> GetUserVenueAsync()
        {
            var user = authService.GetCurrentUser();
            return await venueRepository.GetByUserIdAsync(user.Id);

        }
    }
}
