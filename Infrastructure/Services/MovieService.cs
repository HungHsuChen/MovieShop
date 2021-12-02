using ApplicationCore.Models;
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
        private MovieRepository _movieRepository;
        private CastRepository _castRepository;
        private GenreRepository _genreRepository;
        public MovieService()
        {
            _movieRepository = new MovieRepository();
            _castRepository = new CastRepository();
            _genreRepository = new GenreRepository();
        }

        public MovieDetailResponseModel GetMovieDetail()
        {
            var movie = _movieRepository.GetMovie();
            var casts = _castRepository.GetCasts();
            var genres = _genreRepository.GetGenres();
            var movieDetail = new MovieDetailResponseModel
            {
                Id=movie.Id,
                Title=movie.Title,
                Budget=movie.Budget,
                Overview=movie.Overview,
                PosterUrl=movie.PosterUrl,
                Price=movie.Price,
                ReleaseDate=movie.ReleaseDate,
                RunTime=movie.RunTime,
                Tagline=movie.Tagline,
                Genres=genres,
                Casts=casts
            };
            return movieDetail;
        }

        public IEnumerable<MovieCardResponseModel> GetTopMovies()
        {
            // call my MovieRepository and get the data
            var movies = _movieRepository.Get30HighestGrossingMovies();
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
