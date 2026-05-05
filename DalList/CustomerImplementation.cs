using DO;
using DalApi;
using DalFacade.DalApi;
using Tools;
using System.Reflection;

namespace Dal;

internal class CustomerImplementation : ICustomer
{
    public int Create(Customer item)
    {


        var q = DataSource.Customers.Any(c => c.Id == item.Id);
        if (q)
        {
            LogManager.WriteToLog("Create in Customer failed ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsExistException("Customer Exist ");
        }

        else
        {
            LogManager.WriteToLog("Create in Customer succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            //חיב לקבל משתנה מבצע עם מזהה קיים
            DataSource.Customers.Add(item);
        }
            return item.Id;
        

    }

    public void Delete(int id)
    {
        var q = DataSource.Customers.Single(c => c.Id == id);
        if (q is null)
        {
            LogManager.WriteToLog("Delete in Customer not found ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsExistException("this sale not found");
        }
        else
        {
            LogManager.WriteToLog("Delete in Customer succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            DataSource.Customers.Remove(q);
        }



    }
    public Customer? Read(Func<Customer, bool> filter)
    {
        var q = DataSource.Customers.First(c => filter(c));
        if (q is null)
        {
            LogManager.WriteToLog("Read in Customer  not existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsNotExistException("Customer is not exist");
        }

        else
        {
            LogManager.WriteToLog("Read in Customer succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            return q;
        }

    }
    public Customer Read(int id)
    {

        var q = DataSource.Customers.First(c => c.Id==id);
        if (q is null)
        {
            LogManager.WriteToLog("Read in Customer  not existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsNotExistException("Customer is not exist");
        }
        else
        {
            LogManager.WriteToLog("Read in Customer succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);
            return q;
        }
    }

    public List<Customer?> ReadAll(Func<Customer, bool>? filter)
    {
        if (filter == null)
        {

            return new List<Customer?>(DataSource.Customers);
        }
        var q = DataSource.Customers.Where(c => filter(c));
        LogManager.WriteToLog("ReadAll in Customer succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

        return q.ToList();

    }


    public void Update(Customer item)
    {

        var q = DataSource.Customers.Single(c => c.Id == item.Id);
        if (q is null)
        {
            LogManager.WriteToLog("Update in Customer  not existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsNotExistException(" Customer not exsist ");
        }
        else
        {
            DataSource.Customers.Remove(q);
            DataSource.Customers.Add(item);
        }
        LogManager.WriteToLog("Update in Customer succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);



    }


}