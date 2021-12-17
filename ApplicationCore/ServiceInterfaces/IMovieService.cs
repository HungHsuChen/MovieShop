using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        // Expose the methods that are required by the client/views
        Task <IEnumerable<MovieCardResponseModel>> GetHighestGrossingMovies();

        Task<MovieDetailsResponseModel> GetMovieDetail(int id);

        Task<bool> PurchaseStat(int movieId, int userId);
    }
}
