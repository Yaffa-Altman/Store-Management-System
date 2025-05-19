using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace Dal
{
    public class DalXml : IDal
    {
        public static readonly DalXml instance = new DalXml();
        public static DalXml Instance { get { return instance; } }
        public IProduct Product => new ProductImplementation();

        public ICustomer Customer => new CustomerImplementation();

        public ISale Sale => new SaleImplementation();
    }
}
