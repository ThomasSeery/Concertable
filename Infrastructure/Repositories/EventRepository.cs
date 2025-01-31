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
    public class EventRepository : BaseEntityRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Event>> GetUpcomingByVenueIdAsync(int id)
        {
            var query = (from events in context.Events
                        join registers in context.Registers on events.RegisterId equals registers.Id
                        join listings in context.Listings on registers.ListingId equals listings.Id
                        join venues in context.Venues on listings.VenueId equals venues.Id
                        where venues.Id == id && listings.StartDate >= DateTime.Today
                        select events)
                        .Include(e => e.Register)
                        .ThenInclude(r => r.Listing);

            return await query.ToListAsync();
        }
    }
}
