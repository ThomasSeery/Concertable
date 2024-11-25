using Concertible.Entities;
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

        public async Task<IEnumerable<Venue>> GetVenuesHeadersAsync(VenueParams? venueParams)
        {
            return await venueRepository.GetAllHeadersAsync(venueParams);
        }
    }
}
