
using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Artist : BaseEntity
    {
        public int ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ArtistGenre> ArtistGenres { get; set; }
        public ICollection<SocialMedia> SocialMedias { get; }
        public ICollection<Register> Registers { get; }
        public ICollection<Event> Events { get; }
        public ICollection<Video> Videos { get; }
        
    }
}
