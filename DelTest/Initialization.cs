using DalApi;
using DalFacade.DalApi;
using DO;     

namespace DelTest
{
    public static class Initialization
    {
      
        private static IDal s_dal;
        private static void CreateCustomers()
        {
            var customer1 = new Customer { Id = 1, CustomerName = "Rivka", Address = "Bnebrak", Phone = "0501234567" };
            var customer2 = new Customer { Id = 2, CustomerName = "Dana", Address = "Jerusalem", Phone = "0529876543" };

            s_dal.Customer.Create(customer1);
            s_dal.Customer.Create(customer2);
        }

        private static void CreateProducts()
        {
            var product1 = new Product { Id = 100, ProductName = "Lipstick", Category = Categries.Lips, Price = 25.0, Stock = 10 };
            var product2 = new Product { Id = 101, ProductName = "Eyeshadow", Category = Categries.Eye, Price = 40.0, Stock = 5 };

            s_dal.Product.Create(product1);
            s_dal.Product.Create(product2);
        }

        private static void CreateSales()
        {
            var sale1 = new Sale { Id = 1, ProductId = 100, QuantityRequier = 2, SalePrice = 20.0, IsSaleToCustomer = true, StartSale = DateTime.Today, EndSale = DateTime.Today.AddDays(7) };
            var sale2 = new Sale { Id = 2, ProductId = 101, QuantityRequier = 1, SalePrice = 35.0, IsSaleToCustomer = true, StartSale = DateTime.Today, EndSale = DateTime.Today.AddDays(7) };

            s_dal.Sale.Create(sale1);
            s_dal.Sale.Create(sale2);
        }

        public static void Initialize(IDal dal)
        {
            // שמירת הממשקים בשדות סטטיים
            s_dal = dal;
        }
            
        public static void Initialize()
        {
            // שמירת הממשקים בשדות סטטיים
            s_dal = DalApi.Factory.Get;


            // אתחול הרשימות
            CreateCustomers();
            CreateProducts();
            CreateSales();
        }

        


    }
}
