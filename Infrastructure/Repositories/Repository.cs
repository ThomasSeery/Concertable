using Concertible.Core.Entities;
using Concertible.Core.Interfaces;
using Concertible.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concertible.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> dbSet = context.Set<T>();

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public bool Exists(int id) => dbSet.Any(e => e.Id == id);

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }   

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }


}
