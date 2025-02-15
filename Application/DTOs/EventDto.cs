﻿namespace Application.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int? TotalTickets { get; set; }
        public int? AvailableTickets { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public VenueDto Venue { get; set; }
        public ArtistDto Artist { get; set; }
    }
}
