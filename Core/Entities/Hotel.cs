using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Hotel : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Approved { get; set; }
        public HotelPartner User { get; set; }
        public ICollection<HotelImage> Images { get; }
        public ICollection<HotelBooking> Bookings { get; }

    }
}
