using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [PrimaryKey(nameof(ArtistId), nameof(GenreId))]
    public class ArtistGenre
    {
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
    }
}
