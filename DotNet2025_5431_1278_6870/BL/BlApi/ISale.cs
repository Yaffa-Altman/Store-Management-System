using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface ISale
    {
        int Create(Sale item); //יצירת אוביקט
        Sale? Read(int id); //חיפוש אוביקט עי מספרו 
        List<Sale?> ReadAll(Func<Sale, bool>? filter = null); //מחזיר רשימה של האוביקטים
        void Update(Sale item); //מעדכן את האוביקט
        void Delete(int id); //מוחק אוביקט
        Sale? Read(Func<Sale, bool> filter);
    }
}
