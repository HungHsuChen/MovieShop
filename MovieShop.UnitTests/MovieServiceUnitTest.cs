using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ApplicationCore.Models;
using System.Linq;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        private MovieService _sut;
        private List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;
        private Mock<IPurchaseRepository> _mockPurchaseRepository;
        private Mock<IReviewRepository> _mockReviewRepository;

        // [oneTimeSetup] in nUnit
        [TestInitialize]
        public void OneTimeSetup()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockPurchaseRepository = new Mock<IPurchaseRepository>();
            _mockReviewRepository = new Mock<IReviewRepository>();

            _mockMovieRepository.Setup(expression: m => m.Get30HighestGrossingMovies()).ReturnsAsync(_movies);

            _sut = new MovieService(_mockMovieRepository.Object, _mockPurchaseRepository.Object, _mockReviewRepository.Object);
            
        }

        [ClassInitialize]
        public static void SetUp()
        {
            var _movies = new List<Movie>
            {
                new Movie{ Id=1, Title = "Inception", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg"},
                new Movie { Id=2, Title = "Interstellar", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"},
                new Movie { Id=3, Title = "The Dark Knight", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg"},
                new Movie { Id=4, Title = "Deadpool", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg"},
                new Movie { Id=5, Title = "The Avengers", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg"},
                new Movie{ Id=6, Title = "Ion", Budget= 1600, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg"},
                new Movie { Id=7, Title = "Intar", Budget= 1600, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"},
                new Movie { Id=8, Title = "ThKnight", Budget= 16000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg"},
                new Movie { Id=9, Title = "Deol", Budget= 1600000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg"},
                new Movie { Id=10, Title = "Thegers", Budget= 1600000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg"}
            };
        }

        [TestMethod]
        public async Task Test_ListOFHighestGrossingMovies_FromFakeData()
        {
            // check the actual output with expected value
            // AAA
            // Arrange, Act, and Assert
            // Arrange: Initializes objects, creates mockswith arguments that are passed to the method under test and adds expectations.
            // Act: Invokes the method or property under test with the arranged parameters.
            // Assert: Verifies that the action of the method under test behaves as expected.

            // call movieService method
            // GetHighestGrossingMovie from MovieService

            //Arrange
            // mock objects, data, methods, etc
            //_sut = new MovieService(new MockMovieRepository(), new MockPurchaseRepository(), new MockReviewRepository());

            // Act
            var movies = await _sut.GetHighestGrossingMovies();

            // Assert
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies, typeof(MovieCardResponseModel));
            Assert.AreEqual(10, movies.Count());

        }
    }

    //public class MockMovieRepository : IMovieRepository
    //{
    //    public Task<Movie> Add(Movie entity)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Movie> Delete(int id)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
    //    {
    //        // this method will get the fake data
    //        var _movies = new List<Movie>
    //        {
    //            new Movie{ Id=1, Title = "Inception", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg"},
    //            new Movie { Id=2, Title = "Interstellar", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"},
    //            new Movie { Id=3, Title = "The Dark Knight", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg"},
    //            new Movie { Id=4, Title = "Deadpool", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg"},
    //            new Movie { Id=5, Title = "The Avengers", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg"},
    //            new Movie{ Id=6, Title = "Ion", Budget= 1600, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg"},
    //            new Movie { Id=7, Title = "Intar", Budget= 1600, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"},
    //            new Movie { Id=8, Title = "ThKnight", Budget= 16000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg"},
    //            new Movie { Id=9, Title = "Deol", Budget= 1600000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg"},
    //            new Movie { Id=10, Title = "Thegers", Budget= 1600000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg"}
    //        };

    //        return _movies;
    //    }

    //    public Task<IEnumerable<Movie>> Get30TopRatedMovies()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<List<Movie>> GetAll()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Movie> GetById(int id)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Movie> Update(Movie entity)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    //public class MockPurchaseRepository : IPurchaseRepository
    //{
    //    public Task<Purchase> Add(Purchase entity)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Purchase> Delete(int id)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<List<Purchase>> GetAll()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Purchase> GetById(int movieId, int userId)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Purchase> GetById(int id)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Purchase> Update(Purchase entity)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    //public class MockReviewRepository : IReviewRepository
    //{
    //    public Task<Review> Add(Review entity)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<bool> Delete(int movieId, int userId)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Review> Delete(int id)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<List<Review>> GetAll()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Review> GetById(int movieId, int userId)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Review> GetById(int id)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<IEnumerable<Review>> GetByMovieId(int movieId)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<IEnumerable<Review>> GetByUserId(int userId)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Review> Update(Review entity)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}