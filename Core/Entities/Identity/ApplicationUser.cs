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
    }

    public class Customer : ApplicationUser
    {
        public ICollection<Ticket> Tickets { get; set; }
    }

    public class VenueManager : ApplicationUser
    {
        public Venue? Venue { get; }
    }

    public class ArtistManager : ApplicationUser
    {
        public Artist? Artist { get; set; }
    }

    public class ApplicationRole : IdentityRole<int>
    {
    }
}
