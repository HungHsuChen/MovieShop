using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(64)]
        public string? Name { get; set; }

        public List<MovieGenre> MoviesOfGenre { get; set; }
    }
}
