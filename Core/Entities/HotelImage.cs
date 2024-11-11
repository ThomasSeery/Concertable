using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class HotelImage : BaseEntity
    {
        public int HotelId { get; set; }
        public string Url { get; set; }
        public Hotel Hotel { get; set; }
    }
}
