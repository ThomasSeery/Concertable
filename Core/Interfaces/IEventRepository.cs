using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEventRepository : IBaseEntityRepository<Event>
    {
        Task<IEnumerable<Event>> GetUpcomingByVenueIdAsync(int id);
    }
}
