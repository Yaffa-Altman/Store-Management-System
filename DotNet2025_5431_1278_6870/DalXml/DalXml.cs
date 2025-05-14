using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace Dal
{
    internal sealed class DalXml : IDal
    {
        public IProduct Product => new ProductImplementation();

        public ICustomer Customer => new CustomerImplementation();

        public ISale Sale => new SaleImplementation();
    }
}
