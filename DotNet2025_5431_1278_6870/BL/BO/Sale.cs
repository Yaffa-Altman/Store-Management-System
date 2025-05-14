using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Sale
    {
        public int SaleCode { get; }
        public int ProductId { get; set; }
        public int QuantityForSale { get; set; }
        public double SalePrice { get; set; }
        public bool? IsClub { get; set; }
        public DateTime? StartSale { get; set; } = null;
        public DateTime? EndSale { get; set; } = null;

        public Sale(int saleCode, int productId, int quantityForSale, double salePrice, bool? isClub, DateTime? startSale = null, DateTime? endSale = null)
        {
            this.SaleCode = saleCode;
            this.ProductId = productId;
            this.QuantityForSale = quantityForSale;
            this.SalePrice = salePrice;
            this.IsClub = isClub;
            this.StartSale = startSale;
            this.EndSale = endSale;
        }
    }
}
