using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IRepository<Movie>
    {
        // Common CRUD -> create a common Interface with Generics and reuse that Interface across multiple interfaces
        // Add Movie
        // Get By Id
        // Update
        // Delete
        // GetMovies -> collection by condition -> list of movies whose budget > 100 million

        Task<IEnumerable<Movie>> Get30HighestGrossingMovies();
        Task<IEnumerable<Movie>> Get30TopRatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId);
 
        //Movie GetMovie();

    }
}
