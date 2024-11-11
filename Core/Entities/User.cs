using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string PictureUrl { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
    }
}
