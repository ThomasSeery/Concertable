

namespace Core.Entities
{
    public class Listing : BaseEntity
    {
        public int VenueId { get; set; }
        public DateTime Date { get; set; }
        public Venue Venue { get; set; }
        public Event? Event { get; }
    }
}
