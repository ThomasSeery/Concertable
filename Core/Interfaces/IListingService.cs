using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IListingService
    {
        void CreateListing(Listing listing);
        Task<IEnumerable<Listing>> GetActiveListingsByVenueIdAsync(int id);
    }
}
