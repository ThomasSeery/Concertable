namespace Concertible.Entities
{
    public class HotelPartner : User
    {
        ICollection<Hotel> Hotels { get; }
    }
}
