using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchasesRepository;

        public AdminService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _movieRepository = movieRepository;
            _purchasesRepository = purchaseRepository;
        }

        public async Task<int> CreateMovie(MovieCreateRequestModel model)
        {
            var dbMovie = await _movieRepository.GetById(model.Id);

            if (dbMovie != null) throw new Exception("Movie already exists");

            var movie = new Movie
            {
                Id = model.Id,
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Budget = model.Budget,
                Revenue = model.Revenue,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                UpdatedBy = model.UpdatedBy,
                CreatedBy = model.CreatedBy
            };

            var createdMovie = await _movieRepository.Add(movie);

            return createdMovie.Id;

        }

        public async Task<IEnumerable<PurchaseDetailResponseModel>> GetAllPurchases()
        {
            var purchases = await _purchasesRepository.GetAll();

            var purchaseModels = new List<PurchaseDetailResponseModel>();

            foreach(var purchase in purchases)
            {
                var movie = await _movieRepository.GetById(purchase.MovieId);
                purchaseModels.Add(new PurchaseDetailResponseModel
                {
                    Id = purchase.Id,
                    UserId = purchase.UserId,
                    PurchaseNumber = purchase.PurchaseNumber,
                    PurchaseDateTime = purchase.PurchaseDateTime,
                    TotalPrice = purchase.TotalPrice,
                    movieCard = new MovieCardResponseModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        PosterUrl = movie.PosterUrl
                    }
                });
            }
            return purchaseModels;
        }

        public async Task<int> UpdateMovie(MovieCreateRequestModel model)
        {
            var movie = await _movieRepository.GetById(model.Id);

            if (movie == null) throw new Exception("Movie doesn't exists");

            movie.Id = model.Id;
            movie.Title = model.Title;
            movie.Overview = model.Overview;
            movie.Tagline = model.Tagline;
            movie.Budget = model.Budget;
            movie.Revenue = model.Revenue;
            movie.ImdbUrl = model.ImdbUrl;
            movie.TmdbUrl = model.TmdbUrl;
            movie.PosterUrl = model.PosterUrl;
            movie.BackdropUrl = model.BackdropUrl;
            movie.OriginalLanguage = model.OriginalLanguage;
            movie.ReleaseDate = model.ReleaseDate;
            movie.RunTime = model.RunTime;
            movie.Price = model.Price;
            movie.CreatedDate = model.CreatedDate;
            movie.UpdatedDate = model.UpdatedDate;
            movie.UpdatedBy = model.UpdatedBy;
            movie.CreatedBy = model.CreatedBy;

            var updatedMovie = await _movieRepository.Update(movie);

            return updatedMovie.Id;
        }
    }
}
