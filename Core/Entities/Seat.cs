using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Seat : BaseEntity
    {
        public int ZoneId { get; set; }
        public int? SeatNo { get; set; }
        public Zone Zone { get; set; }
        public ICollection<Ticket> Tickets { get; }
    }
}
