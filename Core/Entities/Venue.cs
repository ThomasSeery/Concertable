using Concertible.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concertible.Entities
{
    public class Venue : BaseEntity
    {
        public int ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Approved { get; set; }
        public bool Posted { get; set; }
        public ICollection<Lease> Leases { get; set; }
        public ICollection<VenueImage> Images { get; }
        public ICollection<Zone> Zones { get; }
    }
}
