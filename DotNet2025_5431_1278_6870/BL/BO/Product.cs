using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        public int ProductCode { get;}
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int? Quantity { get; set; }
        public Categories? Category { get; set; }
        public List<SaleInProduct> Sales { get; set; }

        public Product(int ProductCode, string ProductName, double Price, int? Quantity, Categories? Category)
        {
            this.ProductCode = ProductCode;
            this.ProductName = ProductName;
            this.Price = Price;
            this.Quantity = Quantity;
            this.Category = Category;
            this.Sales = new List<SaleInProduct>();
        }
    }
}
