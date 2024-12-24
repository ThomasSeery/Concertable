using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ListingRepository : BaseEntityRepository<Listing>, IListingRepository
    {
        public ListingRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Listing>> GetActiveByVenueIdAsync(int id)
        {
            var query = context.Listings.AsQueryable();
            query = query.Where(v => v.VenueId == id);

            return await query.ToListAsync();
        }
    }
}
