using DO;
using DalApi;
using DalFacade.DalApi;
using System.Reflection;
using Tools;

namespace Dal;

internal class ProductImplementation : IProduct
{
    public int Create(Product item)
    {
        var q = DataSource.Products.Any(c => c.Id == item.Id);
        if (q)
        {
            LogManager.WriteToLog("Create in Product  existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsExistException("Customer Exist ");
        }

        else
        {
            LogManager.WriteToLog("Create in Product not succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            //חיב לקבל משתנה מבצע עם מזהה קיים
            DataSource.Products.Add(item);
        }

        return item.Id;

    }

    public void Delete(int id)
    {

        var q = DataSource.Products.Single(c => c.Id == id);
        if (q is null)
        {
            LogManager.WriteToLog("Delete in Product not existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsNotExistException("this sale not found");
        }
        else
        {
            LogManager.WriteToLog("Delete in Product succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            DataSource.Products.Remove(q);
        }

    }

    public Product? Read(int id)
    {
        var q = DataSource.Products.First(c => c.Id == id);
        if (q is null)
        {
            LogManager.WriteToLog("Read in Product not existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsNotExistException("id prodact not exist");
        }
        else
        {
            LogManager.WriteToLog("Read in Product succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            return q;
        }
    }
    public Product? Read(Func<Product, bool> filter)
    {
        var q = DataSource.Products.First(c => filter(c));
        if (q is null)
        {
            LogManager.WriteToLog("Read in Product not existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsNotExistException("id prodact not exist");
        }
        else
        {
            LogManager.WriteToLog("Read in Product succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);
            return q;
        }
            
    }

    public List<Product?> ReadAll(Func<Product, bool>? filter)
    {

        if (filter == null)
            return new List<Product?>(DataSource.Products);
        var q = DataSource.Products.Where(c => filter(c));
        LogManager.WriteToLog("ReadAll in Product succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

        return DataSource.Products;

    }

    public void Update(Product item)
    {
        var q = DataSource.Products.Single(c => c.Id == item.Id);
        if (q is null)
        {
            LogManager.WriteToLog("Update in Product not existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsNotExistException("No operation found with this ID");

        }

        else
        {
            DataSource.Products.Remove(q);
            DataSource.Products.Add(item);
            LogManager.WriteToLog("Product in Product succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

        }



    }


}