

namespace Core.Entities
{
    public class Event : BaseEntity
    {
        public int ListingId { get; set; }
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public Listing Listing { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Ticket> SoldTickets { get; }
        public ICollection<EventImage> Images { get; }
        public ICollection<EventGenre> EventGenre { get; }
    }
}
