﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ArtistDto : ItemDto
    {
        public IEnumerable<GenreDto> Genres { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
        public string County { get; set; }
        public string Town { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public ArtistDto()
        {
            Type = "artist";
        }
    }
}
