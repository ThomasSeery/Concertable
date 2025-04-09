﻿using Core.Entities;
using Core.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Venue : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public bool Approved { get; set; }
        public VenueManager User { get; set; }
        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
        public ICollection<VenueImage> Images { get; } = new List<VenueImage>();

    }
}
