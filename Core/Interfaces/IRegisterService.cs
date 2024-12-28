using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRegisterService
    {
        Task<IEnumerable<Register>> GetRegistrationsForListingIdAsync(int listingId);
        Task RegisterForListing(int listingId);
    }
}
