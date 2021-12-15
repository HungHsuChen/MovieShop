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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async override Task<User> GetById(int id)
        {
            var userDetail = await _dbContext.Users.Include(m => m.Purchases).ThenInclude(m => m.Movie)
                .Include(m => m.Favorites).ThenInclude(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (userDetail == null) return null;

            return userDetail;
        }
    }
}
