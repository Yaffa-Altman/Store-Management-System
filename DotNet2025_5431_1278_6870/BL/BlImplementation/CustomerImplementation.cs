using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using static BO.Tools;

namespace BlImplementation
{
    internal class CustomerImplementation : ICustomer
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public int Create(BO.Customer item)
        {
            try
            {
                return _dal.Customer.Create(item.ConvertBOtoDO());
            }
            catch (Exception ex)
            {
                throw new BlIdAlreadyExistsException(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _dal.Customer.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsExist(int id)
        {
            try
            {
                DO.Customer customer = _dal.Customer.Read(id);
                if (customer == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new BlDoesNotExistException(ex.Message);
            }
        }

        public BO.Customer? Read(int id)
        {
            try
            {
                return _dal.Customer.Read(id)?.ConvertDOtoBO();
            }
            catch (DO.DalIdDosentExistException ex)
            {
                throw new BlDoesNotExistException("", ex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BO.Customer? Read(Func<BO.Customer, bool>? filter)
        {
            try
            {
                return _dal.Customer.Read(c => filter(c.ConvertDOtoBO()))?.ConvertDOtoBO();
            }
            catch (Exception ex)
            {
                throw new BlDoesNotExistException(ex.Message);
            }
        }

        public List<BO.Customer?> ReadAll(Func<BO.Customer, bool>? filter = null)
        {
            try
            {
                return _dal.Customer.ReadAll(c => filter(c.ConvertDOtoBO())).Select(s => s.ConvertDOtoBO()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(BO.Customer item)
        {
            try
            {
                _dal.Customer.Update(item.ConvertBOtoDO());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
