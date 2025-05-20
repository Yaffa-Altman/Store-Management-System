using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductInOrder
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<SaleInProduct> SaleInProduct { get; set; }
        public double TotalPrice { get; set; }

        public ProductInOrder(int productId, string productName, double price, int quantity)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Price = price;
            this.Quantity = quantity;
            this.SaleInProduct = new List<SaleInProduct>();
            this.TotalPrice = price*quantity;
        }

    }
}
