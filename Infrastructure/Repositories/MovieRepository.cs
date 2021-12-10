using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext): base(dbContext) 
        {
        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            //we neeed to go to database and get the movies using Dapper or EF Core
            //var movies = new List<Movie>
            //{
            //    new Movie{ Id=1, Title = "Inception", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg"},
            //    new Movie { Id=2, Title = "Interstellar", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"},
            //    new Movie { Id=3, Title = "The Dark Knight", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg"},
            //    new Movie { Id=4, Title = "Deadpool", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg"},
            //    new Movie { Id=5, Title = "The Avengers", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg"}
            // };


            // access the dbcontext object and dbset of movies object to query the movies table
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();

            return movies;
        }

        public async override Task<Movie> GetById(int id)
        {
            // call the Movie dbset and also include the navigation properties such as Genres, Trailers, and Cast

            // Include method in EF will help us navigate to related tables and get data
            var movieDetails = await _dbContext.Movies.Include(m => m.CastOfMovie).ThenInclude(m => m.Cast)
                .Include(m => m.GenresOfMovie).ThenInclude(m=>m.Genre).Include(m=>m.Trailers)
                .FirstOrDefaultAsync(m=>m.Id==id);

            if (movieDetails == null) return null;

            var rating = await _dbContext.Review.Where(m => m.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);

            movieDetails.Rating = rating;

            return movieDetails;
        }

        //public Movie GetMovie()
        //{
        //    var movie = new Movie
        //    {
        //        Id = 1,
        //        Title = "Inception",
        //        Budget = 160000000.0000M,
        //        Overview = "Cobb, a skilled thief who commits corporate espionage by infiltrating the subconscious of his targets is offered a chance to regain his old life as payment for a task considered to be impossible: \"inception\", the implantation of another person's idea into a target's subconscious.",
        //        PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",
        //        Price = 9.90M,
        //        ReleaseDate = DateTime.Parse("2010-07-15T00:00:00"),
        //        RunTime = 148,
        //        Tagline = "Your mind is the scene of the crime."
        //    };
        //    return movie;
        //}
    }
}
