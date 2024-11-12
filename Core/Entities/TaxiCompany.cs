using Concertible.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concertible.Entities
{
    public class TaxiCompany : BaseEntity
    {
        public int ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public bool Approved { get; set; }
        public ICollection<TaxiBooking> Bookings { get; }
    }
}
