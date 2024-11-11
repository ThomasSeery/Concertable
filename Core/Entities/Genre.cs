using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<EventGenre> EventGenres { get; }
    }
}
