using System.Reflection;
using System.Text;

namespace BO
{
    static class Tools
    {
        public static string ToStringProperty<T>(this T obj, string prefix = "")
        {
            StringBuilder sb = new StringBuilder();
            Type t = obj?.GetType() ?? throw new Exception("Obj is NULL");
            foreach (PropertyInfo prop in t.GetProperties())
            {
                if (prop.PropertyType.IsPrimitive
                    || prop.PropertyType == typeof(string)
                    || prop.PropertyType == typeof(DateTime))
                    sb.AppendLine($"{prefix}{prop.Name} = {prop.GetValue(obj)}");
                else
                    sb.Append($"{prefix}{prop.Name} =\n{prop.GetValue(obj).ToStringProperty(prefix + "\t")}");
            }
            return sb.ToString();
        }

        public static bool isCurrentSale(this Sale s)
        {
            if (s.StartSale == null) return false;
            return s.StartSale <= DateTime.Now && s.EndSale >= DateTime.Now;
        }

        public static BO.Sale ConvertDOtoBO(this DO.Sale obj)
        {
            return new BO.Sale(obj.SaleCode, obj.ProductId, obj.QuantityForSale, obj.SalePrice, obj.IsClub, obj.StartSale, obj.EndSale);
        }

        public static DO.Sale ConvertBOtoDO(this BO.Sale obj)
        {
            return new DO.Sale(obj.SaleCode, obj.ProductId, obj.QuantityForSale, obj.SalePrice, obj.IsClub, obj.StartSale, obj.EndSale);
        }

        public static BO.Customer ConvertDOtoBO(this DO.Customer c)
        {
            return new BO.Customer(c.Id, c.Name, c.Address, c.PhoneNumber);
        }

        public static DO.Customer ConvertBOtoDO(this BO.Customer c)
        {
            return new DO.Customer(c.Id, c.Name, c.Address, c.PhoneNumber);
        }

        public static BO.Product ConvertDoToBo(this DO.Product p)
        {
            return new BO.Product(p.ProductCode, p.ProductName, p.Price, p.Quantity, (BO.Categories?)p.Category);
        }
        public static DO.Product ConvertBoToDo(this BO.Product p)
        {
            return new DO.Product(p.ProductCode, p.ProductName, p.Price, p.Quantity, (DO.Categories?)p.Category);
        }
    }
}
