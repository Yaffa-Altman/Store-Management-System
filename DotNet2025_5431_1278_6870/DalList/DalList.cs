using System.ComponentModel;
using DalApi;

namespace Dal
{
    internal sealed class DalList : IDal
    {

        public ICustomer Customer => new CustomerImplementation();
        public IProduct Product => new ProductImplementation();
        public ISale Sale => new SaleImplementation();
        private DalList() { }

        public static readonly DalList instance = new DalList();
        public static DalList Instance { get { return instance; } }
    }

}

