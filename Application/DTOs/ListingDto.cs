﻿using Core.Entities;

namespace Application.DTOs
{
    public class ListingDto
    {
        public int? Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; }
        public double Pay { get; set; }
    }
}
