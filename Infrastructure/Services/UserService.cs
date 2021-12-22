using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;
        public UserService(IUserRepository userRepository, IMovieRepository movieRepository, 
            IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }

        public Task<bool> DeleteReview(int movieId, int userId)
        {
            var delete = _reviewRepository.Delete(movieId, userId);
            return delete;
        }

        public async Task<bool> EditUserProfile(UserDetailsModel model)
        {
            var dbuser = await _userRepository.GetById(model.Id);

            dbuser.FirstName = model.FirstName;
            dbuser.LastName = model.LastName;
            dbuser.Email = model.Email;
            dbuser.DateOfBirth = model.DateOfBirth;
            dbuser.PhoneNumber = model.PhoneNumber;

            var updateUser = await _userRepository.Update(dbuser);

            return true;
        }

        public async Task<int> FavoriteMovie(FavoriteModel model)
        {
            var dbFavorite  = await _favoriteRepository.GetById(model.MovieId,model.UserId);

            if (dbFavorite != null) throw new Exception("Already Favorited");

            var favorite = new Favorite
            {
                Id = model.Id,
                MovieId = model.MovieId,
                UserId = model.UserId,
            };

            var createdFavorite = await _favoriteRepository.Add(favorite);

            return createdFavorite.Id;
        }

        public async Task<FavoriteModel> GetFavoriteMovie(int movieId, int userId)
        {
            var favorite = await _favoriteRepository.GetById(movieId, userId);
            var favoriteModel = new FavoriteModel
            {
                Id = favorite.Id,
                MovieId = movieId,
                UserId = userId
            };
            return favoriteModel;
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

        public async Task<List<ReviewResponseModel>> GetUserReviews(int id)
        {
            var reviews = await _reviewRepository.GetByUserId(id);

            var reviewModels = new List<ReviewResponseModel>();
            foreach (var review in reviews)
            {
                reviewModels.Add(new ReviewResponseModel
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }

            return reviewModels;
        }

        public async Task<int> PurchaseMovie(PurchaseDetailResponseModel model, int userId)
        {
            var dbPurchase = await _purchaseRepository.GetById(model.movieCard.Id, userId);

            if (dbPurchase != null) throw new Exception("Already purchased");

            var purchase = new Purchase
            {
                Id = model.Id,
                UserId = userId,
                PurchaseNumber = model.PurchaseNumber,
                TotalPrice = model.TotalPrice,
                PurchaseDateTime = model.PurchaseDateTime,
                MovieId = model.movieCard.Id
            };

            var createdPurchase = await _purchaseRepository.Add(purchase);
            // save to the database
            // return back

            return createdPurchase.Id;
        }

        public async Task<bool> UpdateReview(ReviewResponseModel model)
        {
            var review = await _reviewRepository.GetById(model.MovieId,model.UserId);

            review.Rating = model.Rating;
            review.ReviewText = model.ReviewText;

            var updateReview = await _reviewRepository.Update(review);

            return true;
        }

        public async Task<bool> WriteReview(ReviewResponseModel model)
        {
            var dbReview = await _reviewRepository.GetById(model.MovieId, model.UserId);

            if (dbReview != null) throw new Exception("Review Existed");

            var review = new Review
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                Rating = model.Rating,
                ReviewText = model.ReviewText
            };
            var createdReview = await _reviewRepository.Add(review);
            return true;
        }
    }
}
