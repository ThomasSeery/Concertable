using Concertible.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concertible.Entities
{
    public class Artist : BaseEntity
    {
        public int ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public ICollection<SocialMedia> SocialMedias { get; }
        public ICollection<Lease> Leases { get; }
        public ICollection<Video> Videos { get; }
        
    }
}
