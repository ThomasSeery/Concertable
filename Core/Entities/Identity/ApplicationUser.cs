using Concertible.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int> 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Customer : ApplicationUser
    {
        public ICollection<Ticket> Tickets { get; set; }
    }

    public class VenueManager : ApplicationUser
    {
        public ICollection<Venue> Venues { get; }
    }

    public class ArtistManager : ApplicationUser
    {
        public ICollection<Artist> Artists { get; set; }
    }

    public class TaxiManager : ApplicationUser
    {
        public ICollection<TaxiCompany> TaxiComapnies { get; }
    }

    public class HotelManager : ApplicationUser
    {
        public ICollection<Hotel> Hotels { get; }
    }

    public class ApplicationRole : IdentityRole<int>
    {
    }
}
