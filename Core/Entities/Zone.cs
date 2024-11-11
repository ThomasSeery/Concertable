using Concertible.Core.Entities;
using Concertible.Entities;

namespace Concertible.Entities
{
    public class Zone : BaseEntity
    {
        public int VenueId { get; set; }
        public char Code { get; set; }
        public ZoneType Type { get; set; }
        public Venue Venue { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
