using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class SaleInProduct
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public CustomerPreference Preference { get; set; }

        public SaleInProduct(int Id, int Quantity, double Price, CustomerPreference Preference = BO.CustomerPreference.CUSTOMER)
        {
            this.Id = Id;
            this.Quantity = Quantity;
            this.Price = Price;
            this.Preference = Preference;
        }
    }
}
