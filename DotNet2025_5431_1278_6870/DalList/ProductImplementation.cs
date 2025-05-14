using DO;
using DalApi;
using System.Linq;
using System.Reflection;
using Tools;

namespace Dal
{
    internal class ProductImplementation : IProduct
    {
        public int Create(Product item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Create Product");
            Product product = item with { ProductCode = DataSource.Config.ProductCode };
            DataSource.Products.Add(product);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Create Product");
            return product.ProductCode;
        }

        public Product? Read(int id)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Read Product");
            try
            {
                return DataSource.Products.Single(p => p?.ProductCode == id);
            }
            catch (Exception)
            {
                throw new DalIdDosentExistException("ERROR: The product ID does not exist : Product");
            }
        }

        public void Delete(int id)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Delete Product");
            DataSource.Products.Remove(Read(id));
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Delete Product");
        }

        public List<Product?> ReadAll(Func<Product, bool>? filter = null)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "ReadAll with filter Product");
            if (filter == null)
                return new List<Product?>(DataSource.Products);

            return DataSource.Products.Where(filter)?.ToList();
            
        }

        public void Update(Product item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Update Product");
            Delete(item.ProductCode);
            DataSource.Products.Add(item);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Update Product");
        }

        public Product? Read(Func<Product, bool> filter)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Read with filter Product");
            try
            {
                return DataSource.Products.First(filter);
            }
            catch (Exception)
            {
                throw new DalIdDosentExistException("ERROR: There is no element that meets the condition : Customer");
            }
        }
    }
}
