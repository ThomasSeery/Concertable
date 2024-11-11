using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Artist : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public ArtistPartner User { get; set; }
        public ICollection<SocialMedia> SocialMedias { get; }
        public ICollection<Lease> Leases { get; }
        public ICollection<Video> Videos { get; set; }
        
    }
}
