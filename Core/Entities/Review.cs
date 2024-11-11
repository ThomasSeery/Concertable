using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Review : BaseEntity
    {
        public int TicketId { get; set; }
        public byte Stars { get; set; }
        public string? Description { get; set; }
        public Ticket Ticket { get; set; }
    }
}
