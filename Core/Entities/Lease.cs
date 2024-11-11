using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Lease : BaseEntity
    {
        public int VenueId { get; set; }
        public int ArtistId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Venue Venue { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Event> Events { get; }
    }
}
