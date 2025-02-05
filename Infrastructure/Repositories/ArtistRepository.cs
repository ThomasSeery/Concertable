﻿using Core.Entities;
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
    public class ArtistRepository : BaseEntityRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Artist>> GetHeadersAsync(SearchParams searchParams)
        {
            var query = context.Artists.AsQueryable();
            query = query.Select(v => new Artist
            {
                Id = v.Id,
                Name = v.Name
            });

            if (!string.IsNullOrWhiteSpace(searchParams?.Sort))
            {
                query = query.OrderBy(v => searchParams.Sort);
            }
            return await query.ToListAsync();
        }

        public async Task<Artist?> GetByUserIdAsync(int id)
        {
            var query = context.Artists.AsQueryable();
            query = query.Where(v => v.UserId == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
