using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
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

        public async Task<IEnumerable<Venue>> GetHeadersAsync(VenueParams? venueParams)
        {
            var query = context.Venues.AsQueryable();
            query.Select(v => new { v.Id, v.Name, v.Longitude, v.Latitude });

            if (!string.IsNullOrWhiteSpace(venueParams?.Sort))
            {
                query = query.OrderBy(v => venueParams.Sort);
            }
            return await query.ToListAsync();
        }
    }
}
