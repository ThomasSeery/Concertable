using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class EventImage : BaseEntity
    {
        public int EventId { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
        public Event Event { get; set; }


    }
}
