using Dal;

﻿
using DalApi;
using DalFacade.DalApi;
using DelTest;
using DO;
using System.IO;
using System;
using Tools;


namespace DalTest;

class Program
{
    //static IDal s_dal = new DalList();

    static IDal s_dal = DalApi.Factory.Get;

    static ICustomer dalCustomer = s_dal.Customer;
    static IProduct dalProduct = s_dal.Product;
    static ISale dalSale = s_dal.Sale;
  
   


     static void Main()
    {
        Initialization.Initialize(s_dal);
        //Initialization.Initialize();


        int mainChoice;

        do
        {
            ShowMainMenu();
            int.TryParse(Console.ReadLine(), out mainChoice);

            switch (mainChoice)
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
                    LogManager.DeleteInLog();
                    break;

                case 0:
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

        } while (mainChoice != 0);
    }

    static T? ReadEntity<T>(ICrud<T> dal, int id)
    {
        return dal.Read(id);
    }

    static IEnumerable<T?> ReadAllEntities<T>(ICrud<T> dal)
    {
        return dal.ReadAll();
    }

    static void DeleteEntity<T>(ICrud<T> dal, int id)
    {
        dal.Delete(id);
    }
    //  Main menu
    static void ShowMainMenu()
    {
        Console.WriteLine("\n=== Main Menu ===");
        Console.WriteLine("1 - Customers");
        Console.WriteLine("2 - Products");
        Console.WriteLine("3 - Sales");
        Console.WriteLine("4 - Delete all Log");
        Console.WriteLine("0 - Exit");
        Console.Write("Select an option: ");
    }

    //Customer menu
    static void CustomerMenu()
    {
        int choice;

        do
        {
            ShowCrudMenu("Customers");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1: CreateCustomer(); break;
                case 2: ReadCustomer(); break;
                case 3: ReadAllCustomers(); break;
                case 4: UpdateCustomer(); break;
                case 5: DeleteCustomer(); break;
            }

        } while (choice != 0);
    }

     static void ShowCrudMenu(string title)
    {
        Console.WriteLine($"\n=== {title} Menu ===");
        Console.WriteLine("1 - Add");
        Console.WriteLine("2 - Read by ID");
        Console.WriteLine("3 - Read All");
        Console.WriteLine("4 - Update");
        Console.WriteLine("5 - Delete");
        Console.WriteLine("0 - Back");
        Console.Write("Select action: ");
    }

    // Customer actions
   
    static void CreateCustomer()
    {
        try
        {
            Console.Write("Enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter Address: ");
            string address = Console.ReadLine()!;

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine()!;

            Customer c = new Customer(id, name, address, phone);
            dalCustomer.Create(c);

            Console.WriteLine("Customer added successfully");

            

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void ReadCustomer()
    {
        try
        {
            Console.Write("Enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Customer? c = ReadEntity(s_dal.Customer, id);

            if (c == null)
                Console.WriteLine("Not found");
            else
                Console.WriteLine(c);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void ReadAllCustomers()
    {
        IEnumerable<Customer?> list = ReadAllEntities(s_dal.Customer);

        foreach (var c in list)
            Console.WriteLine(c);
    }

    static void UpdateCustomer()
    {
        try
        {
            Console.Write("Enter existing ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Console.Write("New Name: ");
            string name = Console.ReadLine()!;

            Console.Write("New Address: ");
            string address = Console.ReadLine()!;

            Console.Write("New Phone: ");
            string phone = Console.ReadLine()!;

            Customer c = new Customer(id, name, address, phone);
            dalCustomer.Update(c);

            Console.WriteLine("Updated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void DeleteCustomer()
    {
        try
        {
            Console.Write("Enter ID to delete: ");
            int.TryParse(Console.ReadLine(), out int id);

            DeleteEntity(s_dal.Customer, id);
            Console.WriteLine("Deleted successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    //Product menu
    static void ProductMenu()
    {
        int choice;
        do
        {
            ShowCrudMenu("Products");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1: CreateProduct(); break;
                case 2: ReadProduct(); break;
                case 3: ReadAllProducts(); break;
                case 4: UpdateProduct(); break;
                case 5: DeleteProduct(); break;
            }

        } while (choice != 0);
    }
    //  CRUD functions for Product 
    static void CreateProduct()
    {
        try
        {
            Console.Write("Enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter Category (0-Face,1-Eye,2-Lips,3-Eyebrows,4-Skincare): ");
            int.TryParse(Console.ReadLine(), out int cat);
            Categries category = (Categries)cat;

            Console.Write("Enter Price: ");
            double.TryParse(Console.ReadLine(), out double price);

            Console.Write("Enter Stock: ");
            int.TryParse(Console.ReadLine(), out int stock);

            Product p = new Product(id, name, category, price, stock);
            dalProduct.Create(p);

            Console.WriteLine("Product added successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void ReadProduct()
    {
        try
        {
            Console.Write("Enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Product? p = ReadEntity(s_dal.Product, id);
            if (p == null)
                Console.WriteLine("Not found");
            else
                Console.WriteLine(p);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void ReadAllProducts()
    {
        foreach (var p in ReadAllEntities(s_dal.Product))
            Console.WriteLine(p);
    }

    static void UpdateProduct()
    {
        try
        {
            Console.Write("Enter existing ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Console.Write("New Name: ");
            string name = Console.ReadLine()!;

            Console.Write("New Category (0-Face,1-Eye,2-Lips,3-Eyebrows,4-Skincare): ");
            int.TryParse(Console.ReadLine(), out int cat);
            Categries category = (Categries)cat;

            Console.Write("New Price: ");
            double.TryParse(Console.ReadLine(), out double price);

            Console.Write("New Stock: ");
            int.TryParse(Console.ReadLine(), out int stock);

            Product p = new Product(id, name, category, price, stock);
            dalProduct.Update(p);

            Console.WriteLine("Updated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void DeleteProduct()
    {
        try
        {
            Console.Write("Enter ID to delete: ");
            int.TryParse(Console.ReadLine(), out int id);

            DeleteEntity(s_dal.Product, id);
            Console.WriteLine("Deleted successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    // Sale menu 
    static void SaleMenu()
    {
        int choice;
        do
        {
            ShowCrudMenu("Sales");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1: CreateSale(); break;
                case 2: ReadSale(); break;
                case 3: ReadAllSales(); break;
                case 4: UpdateSale(); break;
                case 5: DeleteSale(); break;
            }

        } while (choice != 0);
    }


    //  CRUD functions for Sale 
    static void CreateSale()
    {
        try
        {
            Console.Write("Enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Console.Write("Enter Product ID: ");
            int.TryParse(Console.ReadLine(), out int pid);

            Console.Write("Enter Quantity Required: ");
            int.TryParse(Console.ReadLine(), out int qty);

            Console.Write("Enter Sale Price: ");
            double.TryParse(Console.ReadLine(), out double price);

            Console.Write("Is Sale to Customer (true/false): ");
            bool.TryParse(Console.ReadLine(), out bool isSale);

            Console.Write("Enter Start Sale date (yyyy-mm-dd) or leave empty: ");
            DateTime? start = null;
            string s = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(s)) start = DateTime.Parse(s);

            Console.Write("Enter End Sale date (yyyy-mm-dd) or leave empty: ");
            DateTime? end = null;
            s = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(s)) end = DateTime.Parse(s);

            Sale sale = new Sale(id, pid, qty, price, isSale, start, end);
            dalSale.Create(sale);

            Console.WriteLine("Sale added successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void ReadSale()
    {
        try
        {
            Console.Write("Enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Sale? s = ReadEntity(s_dal.Sale, id);
            if (s == null)
                Console.WriteLine("Not found");
            else
                Console.WriteLine(s);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    static void ReadAllSales()
    {
        foreach (var s in ReadAllEntities(s_dal.Sale))
            Console.WriteLine(s);
    }


    static void UpdateSale()
    {
        try
        {
            Console.Write("Enter existing ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Console.Write("New Product ID: ");
            int.TryParse(Console.ReadLine(), out int pid);

            Console.Write("New Quantity Required: ");
            int.TryParse(Console.ReadLine(), out int qty);

            Console.Write("New Sale Price: ");
            double.TryParse(Console.ReadLine(), out double price);

            Console.Write("Is Sale to Customer (true/false): ");
            bool.TryParse(Console.ReadLine(), out bool isSale);

            Console.Write("Enter Start Sale date (yyyy-mm-dd) or leave empty: ");
            DateTime? start = null;
            string s = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(s)) start = DateTime.Parse(s);

            Console.Write("Enter End Sale date (yyyy-mm-dd) or leave empty: ");
            DateTime? end = null;
            s = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(s)) end = DateTime.Parse(s);

            Sale sale = new Sale(id, pid, qty, price, isSale, start, end);
            dalSale.Update(sale);

            Console.WriteLine("Updated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void DeleteSale()
    {
        try
        {
            Console.Write("Enter ID to delete: ");
            int.TryParse(Console.ReadLine(), out int id);

            DeleteEntity(s_dal.Sale, id);
            Console.WriteLine("Deleted successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}
