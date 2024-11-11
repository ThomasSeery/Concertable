using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Ticket : BaseEntity
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int SeatId { get; set; }
        public int? HotelBookingId { get; set; }
        public int? TaxiBookingId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Customer User { get; set; }
        public Event Event { get; set; }
        public Seat Seat { get; set; }
        public HotelBooking? HotelBooking { get; set; }
        public TaxiBooking? TaxiBooking { get; set; }
        public Review? Review { get; set; }
    }
}
