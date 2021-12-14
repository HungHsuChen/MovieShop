using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository =  movieRepository;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetail(int id)
        {
            //var movie = _movieRepository.GetMovie();
            //var casts = _castRepository.GetCasts();
            //var genres = _genreRepository.GetGenres();
            //var movieDetail = new MovieDetailsResponseModel
            //{
            //    Id=movie.Id,
            //    Title=movie.Title,
            //    Budget= (decimal)movie.Budget,
            //    Overview=movie.Overview,
            //    PosterUrl=movie.PosterUrl,
            //    Price= (decimal)movie.Price,
            //    ReleaseDate= (DateTime)movie.ReleaseDate,
            //    RunTime= (int)movie.RunTime,
            //    Tagline=movie.Tagline,
            //    Genres=genres,
            //    Casts=casts
            //};
            //return movieDetail;

            var movie = await _movieRepository.GetById(id);

            // map movie entity into Movie Details Model
            // Automapper that can be used for mapping one object to another object

            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                PosterUrl = movie.PosterUrl,
                Title = movie.Title,
                OriginalLanguage = movie.OriginalLanguage,
                Overview = movie.Overview,
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                TmdbUrl = movie.TmdbUrl,
                ImdbUrl = movie.ImdbUrl,
                Price = movie.Price,
                Budget = movie.Budget,
                ReleaseDate = movie.ReleaseDate
            };

            foreach (var movieCast in movie.CastOfMovie)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = movieCast.CastId,
                    Character = movieCast.Character,
                    Name = movieCast.Cast.Name,
                    PosterUrl = movieCast.Cast.ProfilePath
                });
            }

            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerResponseModel
                {
                    Id = trailer.Id,
                    MovieId = trailer.Id,
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl
                });
            }

            foreach (var movieGenre in movie.GenresOfMovie)
            {
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = movieGenre.GenreId,
                    Name = movieGenre.Genre.Name
                });
            }

            return movieDetails;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetHighestGrossingMovies()
        {
            // call my MovieRepository and get the data
            var movies = await _movieRepository.Get30HighestGrossingMovies();
            // 3rd party Automapper from
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(
                    new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title }
                    );
            }
            return movieCards;

            
        }
    }
}
