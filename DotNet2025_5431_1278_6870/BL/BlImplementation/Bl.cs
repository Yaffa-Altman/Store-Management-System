using BlApi;

namespace BlImplementation
{
    internal class Bl : IBl
    {
        public IProduct product => new ProductImplementation();

        public ICustomer customer => new CustomerImplementation();

        public ISale sale => new SaleImplementation();

        public IOrder order => new OrderImplementation();
    }
}
