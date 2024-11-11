using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class HotelBooking : BaseEntity
    {
        public int HotelId { get; set; }
        public int TicketId { get; set; }
        public int? RoomNo { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Ticket> Tickets { get; }
    }
}
