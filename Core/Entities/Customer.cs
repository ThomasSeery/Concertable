namespace Concertible.Entities
{
    public class Customer : User
    {
        public ICollection<Ticket> Tickets { get; set; }
    }
}
