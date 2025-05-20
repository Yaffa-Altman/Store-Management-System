using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DalApi;
using DO;
using Tools;

namespace Dal
{
    internal class ProductImplementation : IProduct
    {
        public const string FILE_PATH = "../xml/products.xml";
        public int Create(Product item)
        {
            try
            {
                LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Create Product");

                List<Product> products = new List<Product>();
                if (File.Exists(FILE_PATH))
                {
                    products = Config.LoadFromXml<Product>(FILE_PATH);
                    item =  item with { ProductCode = Config.NextProductCode };
                    bool Product = products.Any(c => c?.ProductCode == item.ProductCode);

                    if (Product)
                    {
                        throw new DalIdAlreadyExistsException("Create - ERROR: Product Id already exists");
                    }

                    products.Add(item);
                    Config.SaveToXml(FILE_PATH, products);
                    LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Create Product");
                    return item.ProductCode;
                }
                throw new DalIdDosentExistException("this file doesnt exist!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()!.Name, "start delete Product");

                List<Product> products = new List<Product>();
                if (File.Exists(FILE_PATH))
                {
                    products = Config.LoadFromXml<Product>(FILE_PATH);
                    Product ProductToDelete = products.FirstOrDefault(c => c?.ProductCode == id);
                    if (ProductToDelete != null)
                    {
                        products.Remove(ProductToDelete);
                        Config.SaveToXml(FILE_PATH, products);
                        LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()!.Name, "end delete Product");
                    }
                    else
                    {
                        throw new DO.DalIdDosentExistException("Delete - ERROR: Product Id not exists");
                    }
                }
                else
                {
                    throw new DalIdDosentExistException("this file doesnt exist!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product? Read(int id)
        {
            try
            {
                LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()!.Name, "start read Product");
                List<Product> products = new List<Product>();
                if (File.Exists(FILE_PATH))
                {
                    products = Config.LoadFromXml<Product>(FILE_PATH);
                    Product findProduct = products.FirstOrDefault(c => c?.ProductCode == id);
                    if (findProduct != null)
                    {
                        return findProduct;
                    }
                    throw new DO.DalIdDosentExistException("Read - ERROR: Product Id not exists");
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product? Read(Func<Product, bool>? filter)
        {
            try
            {
                List<Product> products = new List<Product>();
                if (File.Exists(FILE_PATH))
                {
                    products = Config.LoadFromXml<Product>(FILE_PATH);
                    Product findProduct = products.FirstOrDefault(filter);
                    return findProduct;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product?> ReadAll(Func<Product, bool>? filter = null)
        {
            try
            {
                LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()!.Name, " readAll with filter Product");
                List<Product> products = new List<Product>();
                if (File.Exists(FILE_PATH))
                {
                    products = Config.LoadFromXml<Product>(FILE_PATH);
                    if (filter == null)
                    {
                        return products;
                    }
                    return products.Where(filter).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Product item)
        {
            try
            {
                LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()!.Name, "start update Product");
                Delete(item.ProductCode);
                Create(item);
                LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()!.Name, " end update Product");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
