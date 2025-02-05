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
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImageUrl { get; set; }
        public string County { get; set; }
        public string Town { get; set; }
        public bool Approved { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Listing> Listings { get; set; }
        public ICollection<VenueImage> Images { get; }

    }
}
