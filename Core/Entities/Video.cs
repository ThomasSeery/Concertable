﻿

namespace Core.Entities
{
    public class Video : BaseEntity
    {
        public int ArtistId { get; set; }
        public string Url { get; set; }
        public Artist Artist { get; set; }
    }
}
