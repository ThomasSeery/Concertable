using Core.Entities;
using Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IVenueService
    {
        Task<IEnumerable<Venue>> GetVenueHeadersAsync(VenueParams? venueParams);

        Task<Venue> GetVenueDetailsByIdAsync(int id);

        Task<Venue?> GetUserVenueAsync(ClaimsPrincipal principal);

        Task<Venue> CreateVenueAsync();
    }
}
