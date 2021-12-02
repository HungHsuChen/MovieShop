using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public IEnumerable<Genre> GetGenres()
        {
            var genres = new List<Genre>
            {
                new Genre { Id= 1, Name= "Adventure" },
                new Genre { Id= 6, Name= "Action" },
                new Genre { Id= 13, Name= "Science Fiction" }
            };
            return genres;
        }
    }
}
