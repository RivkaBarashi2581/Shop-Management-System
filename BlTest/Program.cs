using BlApi;

namespace BlTest
{
    internal class Program
    {
        static readonly IBl s_bl = Factory.Get();

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("""
                    Main Menu:
                    0 - Exit
                    1 - Customer
                    2 - Product
                    3 - Sale
                    4 - Order
                    """);

                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        CustomerMenu();
                        break;
                    case 2:
                        ProductMenu();
                        break;
                    case 3:
                        SaleMenu();
                        break;
                    case 4:
                        OrderMenu();
                        break;
                }

            } while (choice != 0);
        }

        static BO.Order currentOrder = new BO.Order
        {
            IsFavorite = false,
            Products = new List<BO.ProductInOrder>(),
            TotalPrice = 0
        };

        static void OrderMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("""
                    Order Menu:
                    0 - Back
                    1 - Create new order
                    2 - Add product to order
                    3 - Calculate product price
                    4 - Calculate order price
                    5 - Do order (finalize)
                    6 - Show order
                    """);

                int.TryParse(Console.ReadLine(), out choice);

                try
                {
                    switch (choice)
                    {
                        case 1:
                            currentOrder = new BO.Order
                            {
                                IsFavorite = false,
                                Products = new List<BO.ProductInOrder>(),
                                TotalPrice = 0
                            };
                            Console.WriteLine("New order created");
                            break;

                        case 2:
                            Console.Write("Product Id: ");
                            int productId = int.Parse(Console.ReadLine()!);

                            Console.Write("Amount: ");
                            int amount = int.Parse(Console.ReadLine()!);

                            var sales = s_bl.Order.AddProductToOrder(currentOrder, productId, amount);

                            Console.WriteLine("Product added. Sales:");
                            foreach (var s in sales)
                                Console.WriteLine(s);
                            break;

                        case 3:
                            Console.Write("Product Id: ");
                            int pid = int.Parse(Console.ReadLine()!);

                            var product = currentOrder.Products?
                                .FirstOrDefault(p => p.ProductId == pid);

                            if (product != null)
                            {
                                s_bl.Order.CalcTotalPriceForProduct(product);
                                Console.WriteLine("Product price calculated");
                            }
                            break;

                        case 4:
                            s_bl.Order.CalcTotalPrice(currentOrder);
                            Console.WriteLine($"Order Total: {currentOrder.TotalPrice}");
                            break;

                        case 5:
                            s_bl.Order.DoOrder(currentOrder);
                            Console.WriteLine("Order completed");
                            break;

                        case 6:
                            Console.WriteLine("Order details:");
                            Console.WriteLine(currentOrder);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (choice != 0);
        }

        static void CustomerMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("""
                    Customer Menu:
                    0 - Back
                    1 - Create
                    2 - Read by id
                    3 - Read all
                    4 - Update
                    5 - Delete
                    """);

                int.TryParse(Console.ReadLine(), out choice);

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Id: ");
                            int id = int.Parse(Console.ReadLine()!);
                            Console.Write("Name: ");
                            string name = Console.ReadLine()!;
                            Console.Write("Address: ");
                            string? address = Console.ReadLine();
                            Console.Write("Phone: ");
                            string? phone = Console.ReadLine();

                            BO.Customer customer = new BO.Customer
                            {
                                Id = id,
                                CustomerName = name,
                                Address = address,
                                Phone = phone
                            };

                            Console.WriteLine($"Created id: {s_bl.Customer.Create(customer)}");
                            break;

                        case 2:
                            Console.Write("Id: ");
                            int readId = int.Parse(Console.ReadLine()!);
                            Console.WriteLine(s_bl.Customer.Read(readId));
                            break;

                        case 3:
                            foreach (var item in s_bl.Customer.ReadAll())
                                Console.WriteLine(item);
                            break;

                        case 4:
                            Console.Write("Id: ");
                            int updateId = int.Parse(Console.ReadLine()!);
                            Console.Write("Name: ");
                            string updateName = Console.ReadLine()!;
                            Console.Write("Address: ");
                            string? updateAddress = Console.ReadLine();
                            Console.Write("Phone: ");
                            string? updatePhone = Console.ReadLine();

                            BO.Customer updateCustomer = new BO.Customer
                            {
                                Id = updateId,
                                CustomerName = updateName,
                                Address = updateAddress,
                                Phone = updatePhone
                            };

                            s_bl.Customer.Update(updateCustomer);
                            Console.WriteLine("Updated");
                            break;

                        case 5:
                            Console.Write("Id: ");
                            int deleteId = int.Parse(Console.ReadLine()!);
                            s_bl.Customer.Delete(deleteId);
                            Console.WriteLine("Deleted");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (choice != 0);
        }

        static void ProductMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("""
                    Product Menu:
                    0 - Back
                    1 - Create
                    2 - Read by id
                    3 - Read all
                    4 - Update
                    5 - Delete
                    """);

                int.TryParse(Console.ReadLine(), out choice);

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Id: ");
                            int id = int.Parse(Console.ReadLine()!);
                            Console.Write("Product Name: ");
                            string name = Console.ReadLine()!;
                            Console.Write("Category: ");
                            BO.Categries? category = Enum.Parse<BO.Categries>(Console.ReadLine()!);
                            Console.Write("Price: ");
                            double price = double.Parse(Console.ReadLine()!);
                            Console.Write("Stock: ");
                            int stock = int.Parse(Console.ReadLine()!);

                            BO.Product product = new BO.Product
                            {
                                Id = id,
                                ProductName = name,
                                Category = category,
                                Price = price,
                                Stock = stock
                            };

                            Console.WriteLine($"Created id: {s_bl.Product.Create(product)}");
                            break;

                        case 2:
                            Console.Write("Id: ");
                            int readId = int.Parse(Console.ReadLine()!);
                            Console.WriteLine(s_bl.Product.Read(readId));
                            break;

                        case 3:
                            foreach (var item in s_bl.Product.ReadAll())
                                Console.WriteLine(item);
                            break;

                        case 4:
                            Console.Write("Id: ");
                            int updateId = int.Parse(Console.ReadLine()!);
                            Console.Write("Product Name: ");
                            string updateName = Console.ReadLine()!;
                            Console.Write("Category: ");
                            BO.Categries? updateCategory = Enum.Parse<BO.Categries>(Console.ReadLine()!);
                            Console.Write("Price: ");
                            double updatePrice = double.Parse(Console.ReadLine()!);
                            Console.Write("Stock: ");
                            int updateStock = int.Parse(Console.ReadLine()!);

                            BO.Product updateProduct = new BO.Product
                            {
                                Id = updateId,
                                ProductName = updateName,
                                Category = updateCategory,
                                Price = updatePrice,
                                Stock = updateStock
                            };

                            s_bl.Product.Update(updateProduct);
                            Console.WriteLine("Updated");
                            break;

                        case 5:
                            Console.Write("Id: ");
                            int deleteId = int.Parse(Console.ReadLine()!);
                            s_bl.Product.Delete(deleteId);
                            Console.WriteLine("Deleted");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (choice != 0);
        }

        static void SaleMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("""
                    Sale Menu:
                    0 - Back
                    1 - Create
                    2 - Read by id
                    3 - Read all
                    4 - Update
                    5 - Delete
                    """);

                int.TryParse(Console.ReadLine(), out choice);

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Id: ");
                            int id = int.Parse(Console.ReadLine()!);
                            Console.Write("Product Id: ");
                            int productId = int.Parse(Console.ReadLine()!);
                            Console.Write("Quantity Required: ");
                            int quantity = int.Parse(Console.ReadLine()!);
                            Console.Write("Sale Price: ");
                            double price = double.Parse(Console.ReadLine()!);
                            Console.Write("Start Sale: ");
                            DateTime start = DateTime.Parse(Console.ReadLine()!);
                            Console.Write("End Sale: ");
                            DateTime end = DateTime.Parse(Console.ReadLine()!);

                            BO.Sale sale = new BO.Sale
                            {
                                Id = id,
                                ProductId = productId,
                                QuantityRequier = quantity,
                                SalePrice = price,
                                StartSale = start,
                                EndSale = end
                            };

                            Console.WriteLine($"Created id: {s_bl.Sale.Create(sale)}");
                            break;

                        case 2:
                            Console.Write("Id: ");
                            int readId = int.Parse(Console.ReadLine()!);
                            Console.WriteLine(s_bl.Sale.Read(readId));
                            break;

                        case 3:
                            foreach (var item in s_bl.Sale.ReadAll())
                                Console.WriteLine(item);
                            break;

                        case 4:
                            Console.Write("Id: ");
                            int updateId = int.Parse(Console.ReadLine()!);
                            Console.Write("Product Id: ");
                            int updateProductId = int.Parse(Console.ReadLine()!);
                            Console.Write("Quantity Required: ");
                            int updateQuantity = int.Parse(Console.ReadLine()!);
                            Console.Write("Sale Price: ");
                            double updatePrice = double.Parse(Console.ReadLine()!);
                            Console.Write("Start Sale: ");
                            DateTime updateStart = DateTime.Parse(Console.ReadLine()!);
                            Console.Write("End Sale: ");
                            DateTime updateEnd = DateTime.Parse(Console.ReadLine()!);

                            BO.Sale updateSale = new BO.Sale
                            {
                                Id = updateId,
                                ProductId = updateProductId,
                                QuantityRequier = updateQuantity,
                                SalePrice = updatePrice,
                                StartSale = updateStart,
                                EndSale = updateEnd
                            };

                            s_bl.Sale.Update(updateSale);
                            Console.WriteLine("Updated");
                            break;

                        case 5:
                            Console.Write("Id: ");
                            int deleteId = int.Parse(Console.ReadLine()!);
                            s_bl.Sale.Delete(deleteId);
                            Console.WriteLine("Deleted");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (choice != 0);
        }
    }
}