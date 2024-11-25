using Concertible.Core.Interfaces;
using Concertible.Entities;
using Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IVenueService
    {
        Task<IEnumerable<Venue>> GetVenueHeadersAsync(VenueParams? venueParams);
    }
}
