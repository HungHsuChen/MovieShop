using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel model)
        {
            // make sure the email user entered does not exsits in our database
            var dbUser = await _userRepository.GetUserByEmail(model.Email);

            if (dbUser != null) throw new Exception("Email already exists");

            // continue with registration
            // create a unique salt
            var salt = GenerateSalt();

            // has the password with the salt created above
            var hashedPassword = GetHashedPassword(model.Password, salt);

            var user = new User
            {
                Email = model.Email,
                HashedPassword = hashedPassword,
                Salt = salt,
                DateOfBirth = model.DateOfBirth,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var creaedUser = await _userRepository.Add(user);
            // save to the database
            // return back

            return creaedUser.Id;
        }

        public async Task<UserLoginResponseModel> ValidateUser(LoginRequestModel model)
        {
            // check if the hased password is correct
            // get the data for the user's email
            var user = await _userRepository.GetUserByEmail(model.Email);

            if (user == null) return null; //or exception

            // hash the password the user entered with the salt from the database
            var hashedPassword = GetHashedPassword(model.Password, user.Salt);

            // compare the newly created hashPassword with database password
            if( hashedPassword == user.HashedPassword)
            {
                // correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                return userLoginResponseModel;
            }
            return null;
        }

        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                    password: password,
                                                                    salt: Convert.FromBase64String(salt),
                                                                    prf: KeyDerivationPrf.HMACSHA512,
                                                                    iterationCount: 10000,
                                                                    numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
