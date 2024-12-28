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
    public class ArtistRepository : BaseEntityRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Artist?> GetByUserIdAsync(int id)
        {
            var query = context.Artists.AsQueryable();
            query = query.Where(v => v.UserId == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
