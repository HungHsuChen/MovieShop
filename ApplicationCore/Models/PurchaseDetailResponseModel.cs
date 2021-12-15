using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseDetailResponseModel
    {
        public PurchaseDetailResponseModel()
        {
            movieCard = new MovieCardResponseModel();
        }

        public int Id { get; set; }
        public Guid PurchaseNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public MovieCardResponseModel movieCard { get; set; }
    }
}
