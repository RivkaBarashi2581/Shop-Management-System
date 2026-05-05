using BlApi;
using BO;
using DalFacade.DalApi;
using System;
using System.Collections.Generic;
using System.Linq;



namespace BlImplementation
{
    internal class OrderImplementation : IOrder
    {
        private IDal _dal = DalApi.Factory.Get;
        //הוספת מוצר להזמנה, אם המוצר כבר קיים בהזמנה, עדכון הכמות שלו, אחרת הוספת מוצר חדש להזמנה
        public List<BO.SaleInProduct> AddProductToOrder(BO.Order order, int productId, int amount)
        {
            if (order == null)
                throw new BO.BlInvalidInputException("Order is null");

            var doProduct = _dal.Product.Read(productId)
                ?? throw new BO.BlDoesNotExistException("Product not found");

            order.Products ??= new List<BO.ProductInOrder>();

            var productInOrder = order.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (productInOrder == null)
            {
                if (amount <= 0)
                    throw new BO.BlInvalidInputException("Invalid amount");
                // יצירת מוצר חדש להזמנה
                productInOrder = new BO.ProductInOrder
                {
                    ProductId = doProduct.Id,
                    ProductName = doProduct.ProductName,
                    BasePrice = doProduct.Price ?? 0,
                    Amount = amount,
                    TotalPrice = 0,
                    Sales = new List<BO.SaleInProduct>()
                };

                order.Products.Add(productInOrder);
            }
            else
            {
                if (productInOrder.Amount + amount <= 0)
                    throw new BO.BlInvalidInputException("Invalid amount");
                // עדכון כמות המוצר בהזמנה
                productInOrder.Amount += amount;
            }
            //
            //
            // חיפוש מבצעים רלוונטיים למוצר בהזמנה
            SearchSaleForProduct(productInOrder, order.IsFavorite);
            // חישוב המחיר הכולל של המוצר בהזמנה בהתחשב במבצעים
            CalcTotalPriceForProduct(productInOrder);
            // חישוב המחיר הכולל של ההזמנה
            CalcTotalPrice(order);
            // החזרת רשימת המבצעים הרלוונטיים למוצר בהזמנה
            return productInOrder.Sales ?? new List<BO.SaleInProduct>();
        }

        public void SearchSaleForProduct(BO.ProductInOrder product, bool isFavorite)
        {
            
            var now = DateTime.Now;
            // חיפוש מבצעים רלוונטיים למוצר בהזמנה
            product.Sales = _dal.Sale.ReadAll()
                .Where(s =>
                    s.ProductId == product.ProductId &&
                    (s.StartSale == null || s.StartSale <= now) &&
                    (s.EndSale == null || s.EndSale >= now) &&
                    product.Amount >= s.QuantityRequier
                )
                .Select(s => new BO.SaleInProduct
                {
                    SaleId = s.Id,
                    QuantityForSale = s.QuantityRequier,
                    Price = s.SalePrice ?? 0
                })
                .OrderBy(s => s.Price)
                .ToList();
        }

        public void CalcTotalPriceForProduct(BO.ProductInOrder product)
        {
            double total = 0;
            int count = product.Amount;

            var usedSales = new List<BO.SaleInProduct>();

            if (product.Sales != null)
            {
                foreach (var sale in product.Sales.OrderBy(s => s.Price))
                {
                    if (count <= 0)
                        break;

                    if (count < sale.QuantityForSale)
                        continue;

                    int times = count / sale.QuantityForSale;

                    if (times > 0)
                    {
                        // חישוב המחיר הכולל של המוצר בהזמנה בהתחשב במבצעים
                        total += times * sale.QuantityForSale * sale.Price;
                        count -= times * sale.QuantityForSale;
                        // הוספת המבצע לרשימת המבצעים שהוחלו על המוצר בהזמנה
                        usedSales.Add(sale);
                    }
                }
            }
           
            total += count * product.BasePrice;

            product.TotalPrice = total;
            product.Sales = usedSales;
        }

        public void CalcTotalPrice(BO.Order order)
        {
            // חישוב המחיר הכולל של ההזמנה על ידי סכימת המחירים הכוללים של המוצרים בהזמנה
            order.TotalPrice =  order.Products?.Sum(p => p.TotalPrice) ?? 0;
        }

        public void DoOrder(BO.Order order)
        {
            // ביצוע ההזמנה על ידי עדכון המלאי של המוצרים בהזמנה
            if (order.Products == null)
                return;

            foreach (var item in order.Products)
            {
                // קריאת המוצר מהמסד נתונים כדי לבדוק את המלאי שלו
                var product = _dal.Product.Read(item.ProductId)
                    ?? throw new BO.BlDoesNotExistException("Product not found");
                // בדיקת המלאי של המוצר בהזמנה מול המלאי הקיים במסד נתונים
                if (product.Stock < item.Amount)
                    throw new BO.BlInvalidInputException("Not enough stock");

                var updated = product with
                {
                    // עדכון המלאי של המוצר במסד נתונים על ידי הפחתת הכמות שהוזמנה מהמלאי הקיים
                    Stock = product.Stock - item.Amount
                };
                // עדכון המוצר במסד נתונים עם המלאי החדש
                _dal.Product.Update(updated);
            }
        }
    }
}