﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public  class Register : BaseEntity
    {
        public int ListingId { get; set; }
        public int ArtistId { get; set; }
        public bool Approved { get; set; }
        public Listing Listing { get; set; }
        public Artist Artist { get; set; }
    }
}
