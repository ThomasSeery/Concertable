using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class VenueImage : BaseEntity
    {
        public int VenueId { get; set; }
        public string Url { get; set; }
        public Venue Venue { get; set; }
    }
}
