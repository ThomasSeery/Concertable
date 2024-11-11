﻿using Concertible.Core.Entities;

namespace Concertible.Entities
{
    public class Venue : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Approved { get; set; }
        public bool Posted { get; set; }
        public VenueOwner User { get; set; }
        public ICollection<Lease> Leases { get; set; }
        public ICollection<VenueImage> Images { get; }
        public ICollection<Zone> Zones { get; }
    }
}
