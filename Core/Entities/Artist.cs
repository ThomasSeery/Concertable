
using Core.Entities;
using Core.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Artist : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<ArtistGenre> ArtistGenres { get; set; }
        public ICollection<SocialMedia> SocialMedias { get; }
        public ICollection<Register> Registers { get; }
        public ICollection<Event> Events { get; }
        public ICollection<Video> Videos { get; }
        
    }
}
