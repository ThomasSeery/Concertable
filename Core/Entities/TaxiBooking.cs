using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class TaxiBooking : BaseEntity
    {
        public int TaxiId { get; set; }
        public int TicketId { get; set; }
        public TaxiCompany TaxiCompany { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
