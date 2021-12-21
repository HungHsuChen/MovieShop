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
        Task<UserDetailsModel> GetUserDetails(int id);
        Task<bool> EditUserProfile(UserDetailsModel userDetailsModel);
        Task<IEnumerable<UserDetailsModel>> GetAllUsers();
    }
}
