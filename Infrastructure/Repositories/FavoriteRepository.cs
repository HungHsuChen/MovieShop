using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Favorite> GetById(int movieId, int userId)
        {
            var favorite = await _dbContext.Favorite.FirstOrDefaultAsync(p => p.MovieId == movieId && p.UserId == userId);
            return favorite;
        }
    }
}
