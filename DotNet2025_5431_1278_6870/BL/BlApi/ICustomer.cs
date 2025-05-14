using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface ICustomer
    {
        int Create(Customer item); //יצירת לקוח
        Customer? Read(int id); //חיפוש לקוח
        List<Customer?> ReadAll(Func<Customer, bool>? filter = null); //מחזיר רשימת לקוחות
        void Update(Customer item); //מעדכן לקוח
        void Delete(int id); //מחיקת לקוח
        Customer? Read(Func<Customer, bool>? filter);
        bool IsExist(int id); //האם הלקוח נמצא
    }
}
