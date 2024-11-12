using Concertible.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationUser : IdentityUser<int> { }

    public class Customer : ApplicationUser
    {
        public ICollection<Ticket> Tickets { get; set; }
    }

    public class VenueOwner : ApplicationUser
    {
        public ICollection<Venue> Venues { get; }
    }

    public class ArtistPartner : ApplicationUser
    {
        public Artist? Artist { get; set; }
    }

    public class TaxiPartner : ApplicationUser
    {
        public ICollection<TaxiCompany> TaxiComapnies { get; }
    }

    public class HotelPartner : ApplicationUser 
    {
        public ICollection<Hotel> Hotels { get; }
    }

    public class ApplicationRole : IdentityRole<int> { }
}
