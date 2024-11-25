using Concertible.Entities;
using Concertible.Infrastructure.Repositories;
using Core.Interfaces;
using Core.Parameters;
using Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class VenueRepository : Repository<Venue>, IVenueRepository
    {
        public VenueRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Venue>> GetAllHeadersAsync(VenueParams? venueParams)
        {
            var query = context.Venues.AsQueryable();
            query.Select(v => new { v.Id, v.Name, v.Longitude, v.Latitude });
            query.Where(v => v.Posted);

            if (!string.IsNullOrWhiteSpace(venueParams?.Sort))
            {
                query = query.OrderBy(v => venueParams.Sort);
            }
            return await query.ToListAsync();
        }
    }
}
