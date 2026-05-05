using DalApi;
using DalFacade.DalApi;
using DO;
using System.Reflection;
using System.Xml.Serialization;

using Tools;

namespace Dal;

internal class CustomerImplementation : ICustomer
{
    // כתיבה לקבצי XML
    private static readonly string path = Path.Combine(AppContext.BaseDirectory, "xml", "Customers.xml");
    private static readonly XmlSerializer serializer = new(typeof(List<Customer>));

    private List<Customer> LoadList()
    {
        if (!File.Exists(path))
            return new List<Customer>();

        using StreamReader sr = new StreamReader(path);
        return serializer.Deserialize(sr) as List<Customer> ?? new List<Customer>();
    }

    private void SaveList(List<Customer> list)
    {
        using StreamWriter sw = new StreamWriter(path);
        serializer.Serialize(sw, list);
    }

    public int Create(Customer item)
    {
        LogManager.WriteToLog("Start creating customer", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var customers = LoadList();

        if (customers.Any(c => c.Id == item.Id))
            throw new DalIsExistException("Customer already exists");

        customers.Add(item);
        SaveList(customers);

        LogManager.WriteToLog("Finished creating customer", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return item.Id;
    }

    public Customer? Read(int id)
    {
        LogManager.WriteToLog("Start reading customer by ID", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var customers = LoadList();

        var customer = customers.FirstOrDefault(c => c.Id == id);

        if (customer == null)
            throw new DalIsNotExistException("Customer not found");

        LogManager.WriteToLog("Finished reading customer by ID", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return customer;
    }

    public Customer? Read(Func<Customer, bool> filter)
    {
        LogManager.WriteToLog("Start reading customer by filter", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        if (filter == null)
            throw new ArgumentNullException(nameof(filter));

        var customers = LoadList();

        var customer = customers.FirstOrDefault(filter);

        if (customer == null)
            throw new DalIsNotExistException("No customer matches filter");

        LogManager.WriteToLog("Finished reading customer by filter", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return customer;
    }

    public List<Customer?> ReadAll(Func<Customer, bool>? filter = null)
    {
        LogManager.WriteToLog("Start reading all customers", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var customers = LoadList();

        var result = (filter == null)
            ? customers
            : customers.Where(filter).ToList();

        LogManager.WriteToLog("Finished reading all customers", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return result.Cast<Customer?>().ToList();
    }

    public void Update(Customer item)
    {
        LogManager.WriteToLog("Start updating customer", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var customers = LoadList();

        int index = customers.FindIndex(c => c.Id == item.Id);

        if (index == -1)
            throw new DalIsNotExistException("Customer not found");

        customers[index] = item;

        SaveList(customers);

        LogManager.WriteToLog("Finished updating customer", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);
    }

    public void Delete(int id)
    {
        LogManager.WriteToLog("Start deleting customer", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var customers = LoadList();

        var customer = customers.FirstOrDefault(c => c.Id == id);

        if (customer == null)
            throw new DalIsNotExistException("Customer not found");

        customers.Remove(customer);

        SaveList(customers);

        LogManager.WriteToLog("Finished deleting customer", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);
    }
}
