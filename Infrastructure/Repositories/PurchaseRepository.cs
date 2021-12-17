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
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Purchase> GetById(int movieId, int userId)
        {
            var purchase = await _dbContext.Purchase.FirstOrDefaultAsync(p => p.MovieId == movieId && p.UserId == userId);
            return purchase;
        }
    }
}
