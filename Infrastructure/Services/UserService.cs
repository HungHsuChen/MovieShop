using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;

        public UserService(IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }

        public Task<bool> EditUserProfile(UserDetailsModel userDetailsModel)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDetailsModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetById(id);

            var userDetail = new UserDetailsModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return userDetail;
        }

        public async Task<List<MovieCardResponseModel>> GetUserFavoritedMovies(int id)
        {
            var user = await _userRepository.GetById(id);

            var movieCards = new List<MovieCardResponseModel>();

            foreach (var favoritedMovie in user.Favorites)
            {
                var movie = await _movieRepository.GetById(favoritedMovie.MovieId);
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<List<PurchaseDetailResponseModel>> GetUserPurchasedMovies(int id)
        {
            var user = await _userRepository.GetById(id);

            var purchaseDetail = new List<PurchaseDetailResponseModel>();

            foreach (var purchasedMovie in user.Purchases)
            {
                var movie = await _movieRepository.GetById(purchasedMovie.MovieId);
                purchaseDetail.Add(new PurchaseDetailResponseModel
                {
                    Id = purchasedMovie.Id,
                    PurchaseNumber = purchasedMovie.PurchaseNumber,
                    PurchaseDateTime = purchasedMovie.PurchaseDateTime,
                    TotalPrice = purchasedMovie.TotalPrice,
                    movieCard =
                    {
                        Id = movie.Id,
                        PosterUrl = movie.PosterUrl,
                        Title = movie.Title
                    }
                });
            }

            return purchaseDetail ;
        }
    }
}
