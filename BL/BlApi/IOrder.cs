//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BlApi
//{
//    public interface IOrder
//    {

//    }
//}
namespace BlApi;

public interface IOrder
{
    // כל פעולות הCRUD של ההזמנה
    List<BO.SaleInProduct> AddProductToOrder(BO.Order order, int productId, int amount);

    void CalcTotalPriceForProduct(BO.ProductInOrder product);

    void CalcTotalPrice(BO.Order order);

    void DoOrder(BO.Order order);

    void SearchSaleForProduct(BO.ProductInOrder product, bool isFavorite);
}