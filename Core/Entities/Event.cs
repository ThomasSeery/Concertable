using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Event : BaseEntity
    {
        public int LeaseId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public int GAAvailable { get; set; }
        public int RSAvailable { get; set; }
        public int VIPAvailable { get; set; }
        public int BPAvailable { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Lease Lease { get; set; }
        public ICollection<Ticket> SoldTickets { get; }
        public ICollection<EventImage> Images { get; }
        public ICollection<EventGenre> EventGenre { get; }
    }
}
