namespace Concertible.Entities
{
    public class TaxiPartner : User
    {
        ICollection<TaxiBooking> TaxiComapnies { get; }
    }
}
