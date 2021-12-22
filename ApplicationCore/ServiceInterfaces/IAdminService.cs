using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IAdminService
    {
        Task<int> CreateMovie(MovieCreateRequestModel model);
        Task<int> UpdateMovie(MovieCreateRequestModel model);
        Task<IEnumerable<PurchaseDetailResponseModel>> GetAllPurchases();
    }
}
