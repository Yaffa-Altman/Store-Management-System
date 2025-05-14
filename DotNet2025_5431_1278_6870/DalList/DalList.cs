using System.ComponentModel;
using DalApi;

namespace Dal
{
    internal sealed class DalList : IDal
    {

        public ICustomer Customer => new CustomerImplementation();
        public IProduct Product => new ProductImplementation();
        public ISale Sale => new SaleImplementation();

        public static readonly DalList Instance= new DalList();
        public static DalList getInstance => Instance;
    }

}

