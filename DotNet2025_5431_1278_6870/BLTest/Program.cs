using System.Xml.Linq;
using BO;
using DalTest;

namespace BlTest;
internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;

    static void Main(string[] args)
    {
        Console.WriteLine("To Initialize: press 1, no: press other.");
        int init;
        int.TryParse(Console.ReadLine(), out init);
        if (init == 1)
        {
            Initialization.Initialize();
        }
        createOrder();
    }

    public static void createOrder()
    {
        Console.WriteLine("Please, insert your ID");
        int id;
        int.TryParse(Console.ReadLine(), out id);
        Customer? customer = s_bl.customer.Read(id);
        Console.WriteLine("Enter the customer type!");
        Console.WriteLine("To manager press 1\r\nTo the worker press 2\r\nFor the customer press 3");
        int n;
        int.TryParse(Console.ReadLine(), out n);
        CustomerPreference Preference = (CustomerPreference)n;
        Order order = new Order(Preference);
        int productId = 1, countToOrder;
        bool newOrder = true;
        while (newOrder)
        {
            while (productId > 0)
            {
                Console.WriteLine("To end the order, press 0.");
                Console.WriteLine("Enter product ID for order");
                int.TryParse(Console.ReadLine(), out productId);
                if (productId == 0)
                {
                    break;
                }
                Console.WriteLine("Enter quantity of products to order");
                int.TryParse(Console.ReadLine(), out countToOrder);
                Console.WriteLine("Your Sales: ");
                foreach (var item in s_bl.order.AddProductToOrder(order, productId, countToOrder))//Print the sale list of this product
                {
                    Console.WriteLine($"Product Code: {productId}  Quantity: {item.Quantity}  Price: {item.Price}");
                }
                
                Console.WriteLine();
                Console.WriteLine("The total price is: " + order.TotalPrice);
            }
            global::System.Console.WriteLine("To create new order press 1, to end press 0.");
            int x;
            int.TryParse(Console.ReadLine(), out x);
            newOrder = x == 1 ? true : false;
        }
        Console.WriteLine("\n\nYour order:");
        foreach (var item in order.ProductsInOrder)//Print the products of this order
        {
            Console.WriteLine($"Product Name: {item.ProductName}  Quantity: {item.Quantity}  Price: {item.Price}  Total Price: {item.TotalPrice}");
        }
        Console.WriteLine("\n\n");
    }
}