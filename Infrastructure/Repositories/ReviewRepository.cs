using ApplicationCore.Entities;
using ApplicationCore.Models;
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
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Delete(int movieId, int userId)
        {
            var review = await _dbContext.Review.Where(r => r.MovieId == movieId && r.UserId == userId).FirstOrDefaultAsync();
            _dbContext.Review.Remove(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Review> GetById(int movieId, int userId)
        {
            var review = await _dbContext.Review.Where(r => r.MovieId == movieId && r.UserId == userId).FirstOrDefaultAsync();

            return review;
        }

        public async Task<IEnumerable<Review>> GetByMovieId(int movieId)
        {
            var reviews = await _dbContext.Review.Where(r => r.MovieId == movieId).ToListAsync();

            return reviews;

        }

        public async Task<IEnumerable<Review>> GetByUserId(int userId)
        {
            var reviews = await _dbContext.Review.Where(r => r.UserId == userId).ToListAsync();

            return reviews;
        }
    }
}
