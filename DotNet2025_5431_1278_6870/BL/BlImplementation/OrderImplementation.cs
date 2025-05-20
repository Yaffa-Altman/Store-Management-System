
using BlApi;
using BO;
using DalApi;
using static BO.Tools;

namespace BlImplementation
{
    internal class OrderImplementation : IOrder
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        public List<BO.SaleInProduct> AddProductToOrder(BO.Order order, int productId, int countInOrder)
        {
            try
            {
                DO.Product? p1 = _dal.Product.Read(productId);
                if (p1 == null)
                {
                    throw new BlDoesNotExistException("The product with the requested code was not found.");
                }
                else if (p1.Quantity - countInOrder < 0)
                {
                    throw new BlOutOfStockException("There is not enough stock for this product.");
                }
                BO.ProductInOrder? p2 = order.ProductsInOrder.FirstOrDefault(p => p.ProductId == productId);
                BO.ProductInOrder productInOrder;
                if (p2 == null)
                {
                    productInOrder = new BO.ProductInOrder(productId, p1.ConvertDoToBo().ProductName, p1.ConvertDoToBo().Price,countInOrder);
                    order.ProductsInOrder.Add(productInOrder);
                }
                else
                {
                    productInOrder = p2;
                    productInOrder.Quantity += countInOrder;
                }
                SearchSaleForProduct(productInOrder);
                CalcTotalPriceForProduct(productInOrder);
                CalcTotalPrice(order);
                p1 = p1 with { Quantity = p1.Quantity - countInOrder };
                ProductImplementation p = new ProductImplementation();
                p.Update(p1.ConvertDoToBo());
                return productInOrder.SaleInProduct;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CalcTotalPrice(BO.Order order)
        {
            try
            {
                order.TotalPrice = order.ProductsInOrder.Select(s => s.TotalPrice).Sum();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CalcTotalPriceForProduct(BO.ProductInOrder product)
        {
            try
            {
                if (product.SaleInProduct.Count() == 0)
                {
                    product.TotalPrice = product.Price * product.Quantity;
                }
                else
                {
                    List<SaleInProduct> sales = new List<SaleInProduct>();
                    int numOfProducts = product.Quantity;
                    double p = product.SaleInProduct
                        .Select(s => { int cp = numOfProducts/s.Quantity ; 
                            if(cp > 0){numOfProducts -= s.Quantity*cp; sales.Add(s); } return s.Price*cp; }).Sum();
                    p += numOfProducts * product.Price;
                    product.TotalPrice = p;
                    product.SaleInProduct = sales;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DoOrder(BO.Order order)
        {
            try
            {
                order.ProductsInOrder.Select(p => { _dal.Product.Read(p.ProductId)!.ConvertDoToBo().Quantity -= p.Quantity; return 0; });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public void SearchSaleForProduct(ProductInOrder product)
        {
            try
            {
                product.SaleInProduct = _dal.Sale.ReadAll(s => s.ProductId == product.ProductId
                                        && s.ConvertDOtoBO().isCurrentSale()
                                        && product.Quantity >= s.QuantityForSale)
                                        .OrderBy(s => s?.SalePrice / s?.QuantityForSale)
                                        .Select(s => new SaleInProduct(s!.SaleCode, s.QuantityForSale, s.SalePrice)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
