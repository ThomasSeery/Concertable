using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Video : BaseEntity
    {
        public string ArtistId { get; set; }
        public string Url { get; set; }
        public Artist Artist { get; set; }
    }
}
