using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<List<PurchaseDetailResponseModel>> GetUserPurchasedMovies(int id);
        Task<List<MovieCardResponseModel>> GetUserFavoritedMovies(int id);
        Task<List<ReviewResponseModel>> GetUserReviews(int id);
        Task<UserDetailsModel> GetUserDetails(int id);
        Task<bool> EditUserProfile(UserDetailsModel userDetailsModel);
        Task<int> PurchaseMovie(PurchaseDetailResponseModel model);
        Task<int> FavoriteMovie(FavoriteModel model);
        Task<FavoriteModel> GetFavoriteMovie(int movieId, int userId);
        Task<bool> WriteReview(ReviewResponseModel model);
        Task<bool> UpdateReview(ReviewResponseModel model);
        Task<bool> DeleteReview(int movieId, int userId);
    }
}
