using DO;
using DalApi;
using System.Linq;
using System.Reflection;
using Tools;

namespace Dal
{
    internal class SaleImplementation : ISale
    {
        public int Create(Sale item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Create Sale");
            Sale s = item with { SaleCode = DataSource.Config.SaleCode };
            DataSource.Sales.Add(s);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Create Sale");
            return s.SaleCode;
        }

        public Sale? Read(int code)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Read Sale");
            try
            {
                return DataSource.Sales.Single(s => s?.SaleCode == code);
            }
            catch (Exception)
            {
                throw new DalIdDosentExistException("ERROR: The sale ID does not exist : Sale");
            }
            
        }

        public List<Sale?> ReadAll(Func<Sale, bool>? filter = null)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "ReadAll with filter Sale");
            if (filter == null)
                return new List<Sale?>(DataSource.Sales);
            return DataSource.Sales.Where(filter)?.ToList();
        }

        public void Delete(int code)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Delete Sale");
            Sale? s = Read(code);
            DataSource.Sales.Remove(s);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Delete Sale");
        }

        public void Update(Sale item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Update Sale");
            Delete(item.SaleCode);
            DataSource.Sales.Add(item);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Update Sale");
        }

        public Sale? Read(Func<Sale,bool> filter)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Read with filter Sale");
            try
            {
                return DataSource.Sales.First(filter);
            }
            catch (Exception)
            {
                throw new DalNotFoundException("ERROR: There is no sale that meets the condition : Sale");
            }
        }
    }
}
