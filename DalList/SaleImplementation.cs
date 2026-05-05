
using DO;
using DalApi;
using DalFacade.DalApi;
using System.Reflection;
using Tools;

namespace Dal;

internal class SaleImplementation : ISale
{
    public int Create(Sale item)
    {
        var q = DataSource.Sales.Any(c => c.Id == item.Id);
        if (q)
        {
            LogManager.WriteToLog("Create in Sale not existd ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsExistException("There is a promotion on this code");
        }
           
        else
        {
            LogManager.WriteToLog("Create in Sale succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            DataSource.Sales.Add(item);

        }


        return item.Id;
    }

    public void Delete(int id)
    {
        var q = DataSource.Sales.Single(c => c.Id == id);
        if (q is null)
        {
            LogManager.WriteToLog("Delete in Sale not exist ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);
            throw new DalIsNotExistException("this sale not found");
            
        }
        else
        {
            LogManager.WriteToLog("Delete in Sale succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);
            DataSource.Sales.Remove(q);
        }
            


    }
    public Sale?Read(Func<Sale, bool> filter)
    {
        var q = DataSource.Sales.First(c => filter(c));


        if (q is null)
        {
            LogManager.WriteToLog("Read in Sale not exist ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

            throw new DalIsNotExistException("id Sales not exist");
        }
        else
        {
            LogManager.WriteToLog("Read in Sale succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);
            return (Sale)q;
        }
    }
    public Sale? Read(int id)
    {
        var q = DataSource.Sales.First(c =>c.Id==id);


        if (q is null)
        {
            LogManager.WriteToLog("Read in Sale not exist ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);
            throw new DalIsNotExistException("id Sales not exist");
        }
        else
        {
            LogManager.WriteToLog("Read in Sale succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);
            return (Sale)q;
        }
    }


    List<Sale?> ICrud<Sale>.ReadAll(Func<Sale, bool>? filter)
    {
        if (filter == null)
            return new List<Sale?>(DataSource.Sales);
        var q = DataSource.Sales.Where(c => filter(c));
        LogManager.WriteToLog("ReadAll in Sale succeeded ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);

        return DataSource.Sales;

    }


    public void Update(Sale item)
    {

        var q = DataSource.Sales.Single(c => c.Id == item.Id);
        if (q is null)
        {
            LogManager.WriteToLog("Update in Sale not exist ", MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.FullName);
            throw new DalIsNotExistException("No operation found with this ID");
        }
        else
        {
            DataSource.Sales.Remove(q);
            DataSource.Sales.Add(item);


        }
    }

   
}