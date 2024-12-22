using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [PrimaryKey(nameof(ListingId), nameof(GenreId))]
    public class ListingGenre
    {
        public int ListingId { get; set; }
        public int GenreId { get; set; }
        public Listing Listing { get; set; }
        public Genre Genre { get; set; }
    }
}
