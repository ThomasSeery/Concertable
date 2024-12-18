using Core.Entities;
using Core.Interfaces;
using Core.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository venueRepository;

        public VenueService(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        public async Task<IEnumerable<Venue>> GetVenueHeadersAsync(VenueParams? venueParams)
        {
            return await venueRepository.GetHeadersAsync(venueParams);
        }

        public async Task<Venue> GetVenueDetailsByIdAsync(int id)
        {
            return await venueRepository.GetByIdAsync(id);
        }

        public async Task<Venue> CreateVenueAsync()
        {
            return null;
        }
    }
}
