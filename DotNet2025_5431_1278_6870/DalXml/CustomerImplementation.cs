using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using Tools;

namespace Dal
{
    internal class CustomerImplementation : ICustomer
    {
        public const string FILE_PATH = "../xml/customers.xml";
        public int Create(Customer item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Create Customer");
            if (File.Exists(FILE_PATH))
            {
                List<Customer> customers = Config.LoadFromXml<Customer>(FILE_PATH);
                if (customers.Any(c => c?.Id == item.Id))
                {
                    throw new DalIdAlreadyExistsException("ERROR: The customer ID already exists : Customer");
                }
                customers.Add(item);
                Config.SaveToXml<Customer>("../../xml/sales.xml", customers);
            }
            else
            {
                throw new DalNotFoundException("ERROR: The xml file is not found..");
            }
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Create Customer");
            return item.Id;
        }

        public void Delete(int id)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Delete Customer");
            Customer? customerToDelete = Read(id);
            List<Customer> customers = Config.LoadFromXml<Customer>(FILE_PATH);
            customers.Remove(customerToDelete);
            Config.SaveToXml<Customer>("../../xml/sales.xml", customers);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Delete Customer");
        }

        public Customer? Read(int id)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Find Customer");
            try
            {
                List<Customer> customers = Config.LoadFromXml<Customer>(FILE_PATH);
                return customers.Single(c => c?.Id == id);
            }
            catch (Exception)
            {
                throw new DalIdDosentExistException("ERROR: The customer ID does not exist : Customer");
            }
        }

        public Customer? Read(Func<Customer, bool> filter)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Read with filter Customer");
            try
            {
                List<Customer> customers = Config.LoadFromXml<Customer>(FILE_PATH);
                return customers.First(filter);
            }
            catch (Exception)
            {
                throw new DalIdDosentExistException("ERROR: There is no customer that meets the condition : Customer");
            }
        }

        public List<Customer> ReadAll(Func<Customer, bool>? filter = null)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "ReadAll with filter Customers");
            List<Customer> customers = Config.LoadFromXml<Customer>(FILE_PATH);
            if (filter == null)
                return new List<Customer>(customers);
            return customers.Where(filter).ToList();
        }

        public void Update(Customer item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Update Customer");
            Delete(item.Id);
            List<Customer> customers = Config.LoadFromXml<Customer>(FILE_PATH);
            customers.Add(item);
            Config.SaveToXml<Customer>("../../xml/sales.xml", customers);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Update Customer");
        }
    }
}
