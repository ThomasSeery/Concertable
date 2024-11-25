namespace Web.DTOs
{
    public class VenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Approved { get; set; }
    }
}
