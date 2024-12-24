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

    public class Manager : ApplicationUser
    {
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }

    public class VenueManager : Manager
    {
        public Venue? Venue { get; }
    }

    public class ArtistManager : Manager
    {
        public Artist? Artist { get; set; }
    }

    public class ApplicationRole : IdentityRole<int>
    {
    }
}
