using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieDetailResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Budget { get; set; }
        public string Overview { get; set; }
        public string PosterUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RunTime { get; set; }
        public string Tagline { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Cast> Casts { get; set; }
    }
}
