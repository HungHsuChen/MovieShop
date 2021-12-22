using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IReviewRepository: IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByMovieId(int movieId);
        Task<IEnumerable<Review>> GetByUserId(int userId);
        Task<Review> GetById(int movieId, int userId);
        Task<bool> Delete(int movieId, int userId);
    }
}
