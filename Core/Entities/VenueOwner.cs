namespace Concertible.Entities
{
    public class VenueOwner : User
    {
        public ICollection<Venue> Venues { get; }
    }
}
