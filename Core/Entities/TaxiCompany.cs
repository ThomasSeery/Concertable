using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class TaxiCompany : BaseEntity
    {
        public string Name { get; set; }
        public string About { get; set; }
        public bool Approved { get; set; }
        public ICollection<TaxiBooking> Bookings { get; }
    }
}
