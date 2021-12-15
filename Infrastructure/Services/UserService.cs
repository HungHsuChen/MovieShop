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

        public Task<UserDetailsModel> GetUserDetailss(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MovieCardResponseModel>> GetUserFavoritedMovies(int id)
        {
            throw new NotImplementedException();
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
