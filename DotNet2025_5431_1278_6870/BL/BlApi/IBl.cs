using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlApi
{
    public interface IBl
    {
        IProduct product { get; }
        ICustomer customer { get; }
        ISale sale { get; }
        IOrder order { get; }
    }
}
