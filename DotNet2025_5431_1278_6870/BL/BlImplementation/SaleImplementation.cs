using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using static BO.Tools;

namespace BlImplementation
{
    internal class SaleImplementation : ISale
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        public int Create(BO.Sale item)
        {
            try
            {
                return _dal.Sale.Create(item.ConvertBOtoDO());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _dal.Sale.Delete(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BO.Sale? Read(int id)
        {
            try
            {
                return _dal.Sale.Read(id)?.ConvertDOtoBO();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BO.Sale? Read(Func<BO.Sale, bool> filter)
        {
            try
            {
                return _dal.Sale.Read(s => filter(s.ConvertDOtoBO()))?.ConvertDOtoBO();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BO.Sale?> ReadAll(Func<BO.Sale, bool>? filter = null)
        {
            try
            {
                return _dal.Sale.ReadAll(s => filter(s.ConvertDOtoBO())).Select(s => s.ConvertDOtoBO()).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(BO.Sale item)
        {
            try
            {
                _dal.Sale.Update(item.ConvertBOtoDO());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
