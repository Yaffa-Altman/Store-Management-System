using System.Collections;
using DO;
  
namespace Dal
{
    internal static class DataSource
    {
        static internal List<Product?> Products = new List<Product?>();
        static internal List<Customer?> Customers = new List<Customer?>();
        static internal List<Sale?> Sales = new List<Sale?>();


        internal static class Config
        {
            internal const int START_PRODUCT_CODE = 100;
            private static int productCode = START_PRODUCT_CODE;

            internal const int START_CODE_SALE = 100;
            private static int saleCode = START_CODE_SALE;

            public static int ProductCode { get { return productCode++; } }
            public static int SaleCode { get { return saleCode++; } }
        }

    }

    
}
