using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
using Tools;

namespace Dal
{
    internal class SaleImplementation : ISale
    {
        public const string FILE_PATH = "../xml/sales.xml";
        public int Create(Sale item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Create Sale");
            XElement salesXml = XElement.Load(FILE_PATH);
            Sale s = item with { SaleCode = Config.NextSaleCode };
            XElement e = new XElement("Sale",
                new XElement("SaleCode", s.SaleCode),
                new XElement("ProductId", s.ProductId),
                new XElement("QuantityForSale", s.QuantityForSale),
                new XElement("SalePrice", s.SalePrice));
            if (s.IsClub != null)
            {
                e.Add(new XElement("IsClub", s.IsClub));
            }
            if (s.StartSale != null)
            {
                e.Add(new XElement("StartSale", s.StartSale));
            }
            if (s.EndSale != null)
            {
                e.Add(new XElement("EndSale", s.EndSale));
            }
            salesXml.Add(e);
            salesXml.Save(FILE_PATH);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Create Sale");
            return s.SaleCode;
        }

        public void Delete(int id)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Delete Sale");
            XElement salesXml = XElement.Load(FILE_PATH);
            Sale? s = Read(id);
            ///////////////
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Delete Sale");
        }

        public Sale? Read(int id)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Read Sale");
            try
            {
                List<Sale> sales = Config.LoadFromXml<Sale>(FILE_PATH);
                return sales.Single(s => s?.SaleCode == id);
            }
            catch (Exception)
            {
                throw new DalIdDosentExistException("ERROR: The sale ID does not exist : Sale");
            }
        }

        public Sale? Read(Func<Sale, bool> filter)
        {
            throw new NotImplementedException(); LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Read with filter Sale");
            try
            {
                List<Sale> sales = Config.LoadFromXml<Sale>(FILE_PATH);
                return sales.FirstOrDefault(filter);
            }
            catch (Exception)
            {
                throw new DalNotFoundException("ERROR: There is no sale that meets the condition : Sale");
            }
        }

        public List<Sale> ReadAll(Func<Sale, bool>? filter = null)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "ReadAll with filter Sale");
            List<Sale> sales = Config.LoadFromXml<Sale>(FILE_PATH);
            if (filter == null)
                return new List<Sale>(sales);
            return sales.Where(filter).ToList();
        }

        public void Update(Sale item)
        {
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "Start Update Sale");
            Delete(item.SaleCode);
            List<Sale> sales = Config.LoadFromXml<Sale>(FILE_PATH);
            sales.Add(item);
            Config.SaveToXml<Sale>(FILE_PATH, sales);
            LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, "End Update Sale");
        }
    }
}
