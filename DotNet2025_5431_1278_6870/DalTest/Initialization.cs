using DO;
using DalApi; 
using System.Reflection;
using Tools;


namespace DalTest
{
    public class Initialization
    {
        private static IDal s_dal;
        private static List<int> productsCodes = new List<int>();

        public static void Initialize()
        {
            s_dal = Factory.Get;
            createCustomers();
            createProduct();
            createSales();
        }
        public static void createProduct()
        {
            productsCodes.Add(s_dal.Product.Create(new Product(0, "ביסלי", 5.9, 500, Categories.SNACKS)));
            productsCodes.Add(s_dal.Product.Create(new Product(0, "צ'יפס", 4.9, 500, Categories.SNACKS)));
            productsCodes.Add(s_dal.Product.Create(new Product(0, "שוקולד שוויצרי", 8.9, 400, Categories.CHOCOLATES)));
            productsCodes.Add(s_dal.Product.Create(new Product(0, "נחשים", 5.9, 20, Categories.JELLYS)));
            productsCodes.Add(s_dal.Product.Create(new Product(0, "חמצוץ ממולא", 4.9, 20, Categories.JELLYS)));
            productsCodes.Add(s_dal.Product.Create(new Product(0, "ליקר שוקולד", 42, 15, Categories.LIQUORS)));
            productsCodes.Add(s_dal.Product.Create(new Product(0, "יין גן עדן", 38, 15, Categories.LIQUORS)));
            productsCodes.Add(s_dal.Product.Create(new Product(0, "שחורים", 2.9, 30, Categories.CRACKS)));
            productsCodes.Add(s_dal.Product.Create(new Product(0, "במבה", 3.9, 500, Categories.SNACKS)));
        }
        public static void createCustomers()
        {
            s_dal.Customer.Create(new Customer(1234, "Ari", "jerusalem", "0523784590"));
            s_dal.Customer.Create(new Customer(1568, "Shani", "jerusalem", "0558972004"));
            s_dal.Customer.Create(new Customer(9087, "Bati", "jerusalem", "0538756435"));
            s_dal.Customer.Create(new Customer(7493, "Dani", "jerusalem", "0534107896"));
            s_dal.Customer.Create(new Customer(8654, "Avi", "modiin ilit", "0534169998"));
            s_dal.Customer.Create(new Customer(7894, "Gadi", "modiin ilit", "0534237898"));
            s_dal.Customer.Create(new Customer(7564, "Gili", "modiin ilit", "0534109898"));
            s_dal.Customer.Create(new Customer(1213, "Adi", "modiin ilit", "0534507698"));
        }
        public static void createSales()
        {
            s_dal.Sale.Create(new Sale(0,100,3,15,false, DateTime.Now, DateTime.Now.AddDays(12)));
            s_dal.Sale.Create(new Sale(0,105,2,70,true,DateTime.Now, DateTime.Now.AddDays(12)));
            s_dal.Sale.Create(new Sale(0,104,1,4,true, DateTime.Now.AddDays(2), DateTime.Now.AddDays(12)));
            s_dal.Sale.Create(new Sale(0,106,2,60,false, DateTime.Now, DateTime.Now.AddDays(14)));
            s_dal.Sale.Create(new Sale(0,102,2,15,true, DateTime.Now, DateTime.Now.AddDays(12)));
            s_dal.Sale.Create(new Sale(0,108,3,10,false, DateTime.Now, DateTime.Now.AddDays(12)));
        }

    }
}
