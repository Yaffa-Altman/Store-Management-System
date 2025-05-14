
using DalApi;
using Dal;
using DO;
using System.Reflection;
using Tools;
using System.Xml.Linq;

namespace DalTest;
internal class Program
{
    public enum NAME_CLASS {Product,Customer,Sale }
    private static readonly IDal s_dal = DalApi.Factory.Get;
    static void Main(string[] args)
    {
        try
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Initialize");
            Initialization.Initialize();
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Initialize");
            PrintMenu(PrintMainMenu());
        }
        catch(Exception ex)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, ex.Message);
            Console.WriteLine(ex.Message);
        }
    }

    private static int PrintMainMenu()
    {
        Console.WriteLine("For Product press 1.");
        Console.WriteLine("For Customer press 2.");
        Console.WriteLine("For Sale press 3.");
        Console.WriteLine("For clean the Log directory press 111.");
        Console.WriteLine("For Exit press 0.");

        int select;
        if(! int.TryParse(Console.ReadLine(), out select))
            select = -1;
        return select;
    }
    private static void PrintMenu(int select)
    {
        while(select > 0)
        {
            switch (select)
            {
                case 1:
                    Crud(NAME_CLASS.Product,s_dal.Product);
                    break;
                case 2:
                    Crud(NAME_CLASS.Customer, s_dal.Customer);
                    break;
                case 3:
                    Crud(NAME_CLASS.Sale,s_dal.Sale);
                    break;
                case 111:
                    LogManager.clearLog();
                    break;
                default:
                    Console.WriteLine("Wrong selection please select again.");
                    break;
            }
            select = PrintMainMenu();
        }
    }
    private static int PrintSubMenu(NAME_CLASS item)
    {
        Console.WriteLine($"To add {item} press 1.");
        Console.WriteLine($"To read on {item} press 2.");
        Console.WriteLine($"To read all {item}s press 3.");
        Console.WriteLine($"To update {item} press 4.");
        Console.WriteLine($"To delete {item} press 5.");
        Console.WriteLine($"To go back press 0.");

        int select;
        if (!int.TryParse(Console.ReadLine(), out select))
            select = -1;
        return select;
    }
    private static void Crud<T>(NAME_CLASS item,ICrud<T> crud)
    {
        int select = PrintSubMenu(item);
        while(select != 0){
            switch (select)
            {
                case 1:
                    switch (item)
                    {
                        case NAME_CLASS.Customer:
                            CreateCustomer();
                            break;
                        case NAME_CLASS.Product:
                            CreateProduct();
                            break;
                        case NAME_CLASS.Sale:
                            CreateSale();
                            break;
                    }
                    break;
                case 2:
                    Read(crud);
                    break;
                case 3:
                    ReadAll(crud);
                    break;
                case 4:
                    switch (item)
                    {
                        case NAME_CLASS.Customer:
                            UpdateCustomer();
                            break;
                        case NAME_CLASS.Product:
                            UpdateProduct();
                            break;
                        case NAME_CLASS.Sale:
                            UpdateSale();
                            break;
                    }
                    break;
                case 5:
                    Delete(crud);
                    break;
                default:
                    Console.WriteLine("Wrong selection please select again.");
                    break;
            }
            select = PrintSubMenu(item);
        }
    }
    private static void Delete<T>(ICrud<T> crud)
    {
        try {
            Console.WriteLine("insert a code:");
            int code = int.Parse(Console.ReadLine());
            crud.Delete(code);
        }
        catch(Exception e)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
            Console.WriteLine(e.Message);
        }
    }
    private static void ReadAll<T>(ICrud<T> crud)
    {
        try
        {
            Console.WriteLine();
            foreach (var item in crud.ReadAll())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
        }
    }
    private static void Read<T>(ICrud<T> crud)
    {
        try
        {
            Console.WriteLine("insert code to read");
            int code = int.Parse(Console.ReadLine());
            Console.WriteLine("\n"+crud.Read(code)+"\n");
        }
        catch (Exception e)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
            Console.WriteLine(e.Message);
        }
    }
    private static void CreateProduct()
    {
        try
        {
            Console.WriteLine("\nthe code is: "+ s_dal.Product.Create(CreateOrUpdateProduct())+"\n");
        }
        catch (Exception e)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
            Console.WriteLine(e.Message);
        }
    }
    private static void UpdateProduct()
    {
        try
        {
            Console.WriteLine("ProductCode:");
            int productCode;
            int.TryParse(Console.ReadLine(), out productCode);
            s_dal.Product.Update(CreateOrUpdateProduct(productCode));
        }
        catch (Exception e)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
            Console.WriteLine(e.Message);
        }
    }
    private static Product CreateOrUpdateProduct(int code = 0)
    {
        Console.WriteLine("Insert product details:");
        Console.WriteLine("productName:");
        string name = Console.ReadLine();
        Console.WriteLine("price:");
        int price; 
        int.TryParse(Console.ReadLine(), out price);
        Console.WriteLine("quantity:");
        int quantity;
        int.TryParse(Console.ReadLine(), out quantity);
        Console.WriteLine("category:");
        Categories category;
        Categories.TryParse(Console.ReadLine(), out category);
        return new Product(code, name,price,quantity,category);
    }
    private static void CreateSale()
    {
        try
        {
            Console.WriteLine("\nthe code is: "+s_dal.Sale.Create(CreateOrUpdateSale())+"\n");
        }
        catch(Exception e)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
            Console.WriteLine(e.Message);
        }
    }
    private static void UpdateSale()
    {
        try
        {
            Console.WriteLine("SaleCode:");
            int saleCode;
            int.TryParse(Console.ReadLine(), out saleCode);
            s_dal.Sale.Update(CreateOrUpdateSale(saleCode));
        }
        catch (Exception e)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
            Console.WriteLine(e.Message);
        }
    }
    private static Sale CreateOrUpdateSale(int code = 0)
    {
        Console.WriteLine("Insert sale details:");
        Console.WriteLine("ProductId:");
        int productId;
        int.TryParse(Console.ReadLine(), out productId);
        Console.WriteLine("QuantityForSale:");
        int quantityForSale;
        int.TryParse(Console.ReadLine(), out quantityForSale);
        Console.WriteLine("SalePrice:");
        double salePrice;
        double.TryParse(Console.ReadLine(), out salePrice);
        Console.WriteLine("IsClub:");
        bool isClub;
        bool.TryParse(Console.ReadLine(), out isClub);
        Console.WriteLine("StartSale:");
        DateTime startSale;
        if(!DateTime.TryParse(Console.ReadLine(), out startSale))
            startSale = DateTime.Now;
        Console.WriteLine("EndSale:");
        DateTime endSale;
        if(!DateTime.TryParse(Console.ReadLine(), out endSale))
            endSale = DateTime.Now.AddDays(10);
        return new Sale(code,productId,quantityForSale,salePrice,isClub,startSale,endSale);
    }
    private static void CreateCustomer()
    {
        try
        {
            s_dal.Customer.Create(CreateOrUpdateCustomer());
        }
        catch (Exception e)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
            Console.WriteLine(e.Message);
        }
    }
    private static void UpdateCustomer()
    {
        try
        {
            s_dal.Customer.Update(CreateOrUpdateCustomer());
        }
        catch (Exception e)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
            Console.WriteLine(e.Message);
        }
    }
    private static Customer CreateOrUpdateCustomer()
    {
        Console.WriteLine("Insert customer details:");
        Console.WriteLine("id:");
        int id;
        int.TryParse(Console.ReadLine(), out id);
        Console.WriteLine("customerName:");
        string name = Console.ReadLine();
        Console.WriteLine("address:");
        string address = Console.ReadLine();
        Console.WriteLine("phone number:");
        string phoneNumber = Console.ReadLine();
        return new Customer(id, name, address, phoneNumber);
    }
}
