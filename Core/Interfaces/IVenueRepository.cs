using Core.Entities;
using Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        Task<IEnumerable<Venue>> GetHeadersAsync(VenueParams? venueParams);
        Task<Venue?> GetByUserIdAsync(int id);
    }
}
