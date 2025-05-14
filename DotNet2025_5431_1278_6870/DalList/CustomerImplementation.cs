using DO;
using DalApi;
using System.Reflection;
using Tools;

namespace Dal
{
    internal class CustomerImplementation : ICustomer
    {
        public int Create(Customer item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Create Customer");
            if (DataSource.Customers.Any(c => c?.Id == item.Id))
            {
                throw new DalIdAlreadyExistsException("ERROR: The customer ID already exists : Customer");
            }
            DataSource.Customers.Add(item);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Create Customer");
            return item.Id;
        }

        public Customer? Read(int id)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Find Customer");
            try
            {
                return DataSource.Customers.Single(c => c?.Id == id);
            }
            catch (Exception)
            {
                throw new DalIdDosentExistException("ERROR: The customer ID does not exist : Customer");
            }
        }

        public List<Customer?> ReadAll(Func<Customer, bool>? filter = null)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "ReadAll with filter Customers");
            if (filter == null)
                return new List<Customer?>(DataSource.Customers);
            return DataSource.Customers.Where(filter).ToList();
        }

        public void Delete(int id)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Delete Customer");
            Customer? customerToDelete = Read(id);
            DataSource.Customers.Remove(customerToDelete);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Delete Customer");
        }

        public void Update(Customer item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Update Customer");
            Delete(item.Id);
            DataSource.Customers.Add(item);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Update Customer");
        }

        public Customer? Read(Func<Customer,bool> filter)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Read with filter Customer");
            try
            {
                return DataSource.Customers.First(filter);
            }
            catch (Exception)
            {
                throw new DalIdDosentExistException("ERROR: There is no customer that meets the condition : Customer");
            }
            
        }
    }
}
