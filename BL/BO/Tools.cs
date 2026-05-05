

using System.Reflection;
using System.Runtime.CompilerServices;

namespace BO;

internal static class Tools
{
    // פונקציה שמחזירה מחרוזת עם כל הפרופרטים של האובייקט והערכים שלהם
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        Type Ttype = t.GetType();
        PropertyInfo[] info = Ttype.GetProperties();
        foreach (PropertyInfo item in info)
        {
            str += $"{item.Name}: {item.GetValue(t)}\n";
        }
        return str;

    }
    ///// <summary>פונקציות המרה מBO ל DO  וכן להפוך</summary>
    // המרת cuatomer
    public static BO.Customer ToBO(this DO.Customer customer) =>
   new BO.Customer
   {
       Id = customer.Id,
       CustomerName = customer.CustomerName,
       Address = customer.Address,
       Phone = customer.Phone
   };

    public static DO.Customer ToDO(this BO.Customer customer) =>
        new DO.Customer
        {
            Id = customer.Id,
            CustomerName = customer.CustomerName,
            Address = customer.Address,
            Phone = customer.Phone
        };
    // המרת product

    public static BO.Product ToBO(this DO.Product product) =>
        new BO.Product
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Category = (BO.Categries?)product.Category,
            Price = product.Price,
            Stock = product.Stock
        };

    public static DO.Product ToDO(this BO.Product product) =>
        new DO.Product
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Category = (DO.Categries)product.Category!,
            Price = product.Price,
            Stock = product.Stock
        };
    // המרת Sale
    public static BO.Sale ToBO(this DO.Sale sale) =>
     new BO.Sale
     {
         Id = sale.Id,
         ProductId = sale.ProductId,
         QuantityRequier = sale.QuantityRequier,
         SalePrice = sale.SalePrice,
         StartSale = sale.StartSale,
         EndSale = sale.EndSale
     };

    public static DO.Sale ToDO(this BO.Sale sale) =>
        new DO.Sale
        {
            Id = sale.Id,
            ProductId = sale.ProductId,
            QuantityRequier = sale.QuantityRequier,
            SalePrice = sale.SalePrice,
            StartSale = sale.StartSale,
            EndSale = sale.EndSale
        };

}
