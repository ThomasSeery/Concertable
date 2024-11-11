using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Concertible.Core.Entities;

namespace Concertible.Entities
{
    [PrimaryKey(nameof(EventId), nameof(GenreId))]
    public class EventGenre
    {
        public int EventId { get; set; }
        public int GenreId { get; set; }
        public Event Event { get; set; }
        public Genre Genre { get; set; }
    }
}
