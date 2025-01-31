﻿using Core.Entities;
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
    public class VenueRepository : BaseEntityRepository<Venue>, IVenueRepository
    {
        public VenueRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Venue>> GetHeadersAsync(VenueParams? venueParams)
        {
            var query = context.Venues.AsQueryable();
            query = query.Select(v => new Venue
            {
                Id = v.Id,
                Name = v.Name
            });

            if (!string.IsNullOrWhiteSpace(venueParams?.Sort))
            {
                query = query.OrderBy(v => venueParams.Sort);
            }
            return await query.ToListAsync();
        }

        public async Task<Venue?> GetByUserIdAsync(int id)
        {
            var query = context.Venues.AsQueryable();
            query = query.Where(v => v.UserId == id);
                

            return await query.FirstOrDefaultAsync();
        }
    }
}
