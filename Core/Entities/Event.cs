

namespace Core.Entities
{
    public class Event : BaseEntity
    {
        public int RegisterId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public double Price { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
        public string ImageUrl { get; set; }
        public Register Register { get; set; }
        public ICollection<Ticket> Tickets { get; }
        public ICollection<EventImage> Images { get; }
        public ICollection<EventGenre> EventGenre { get; }
    }
}
