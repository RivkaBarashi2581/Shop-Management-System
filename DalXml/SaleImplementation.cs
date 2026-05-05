using DalApi;
using DalFacade.DalApi;
using DalXml;
using DO;
using System.Reflection;
using System.Xml.Serialization;
using Tools;


namespace Dal;

internal class SaleImplementation : ISale
{
    // כתיבה לקבצי XML
    private static readonly string path = Path.Combine(AppContext.BaseDirectory, "xml", "Sales.xml");
    private static readonly XmlSerializer serializer = new(typeof(List<Sale>));

    private List<Sale> LoadList()
    {
        if (!File.Exists(path))
            return new List<Sale>();

        using StreamReader sr = new StreamReader(path);
        return serializer.Deserialize(sr) as List<Sale> ?? new List<Sale>();
    }

    private void SaveList(List<Sale> list)
    {
        using StreamWriter sw = new StreamWriter(path);
        serializer.Serialize(sw, list);
    }

    public int Create(Sale item)
    {
        LogManager.WriteToLog("Start creating sale", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var sales = LoadList();

        if (sales.Any(s => s.Id == item.Id))
            throw new DalIsExistException("Sale already exists");

        int newId = Config.GetSaleId;
        Sale newSale = item with { Id = newId };

        sales.Add(newSale);
        SaveList(sales);

        LogManager.WriteToLog("Finished creating sale", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return newSale.Id;
    }

    public Sale? Read(int id)
    {
        LogManager.WriteToLog("Start reading sale by ID", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var sales = LoadList();

        var sale = sales.FirstOrDefault(s => s.Id == id);

        if (sale == null)
            throw new DalIsNotExistException("Sale not found");

        LogManager.WriteToLog("Finished reading sale by ID", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return sale;
    }

    public Sale? Read(Func<Sale, bool> filter)
    {
        LogManager.WriteToLog("Start reading sale by filter", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        if (filter == null)
            throw new ArgumentNullException(nameof(filter));

        var sales = LoadList();

        var sale = sales.FirstOrDefault(filter);

        if (sale == null)
            throw new DalIsNotExistException("No sale matches filter");

        LogManager.WriteToLog("Finished reading sale by filter", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return sale;
    }

    public List<Sale?> ReadAll(Func<Sale, bool>? filter = null)
    {
        LogManager.WriteToLog("Start reading all sales", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var sales = LoadList();

        var result = (filter == null)
            ? sales
            : sales.Where(filter).ToList();

        LogManager.WriteToLog("Finished reading all sales", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return result.Cast<Sale?>().ToList();
    }

    public void Update(Sale item)
    {
        LogManager.WriteToLog("Start updating sale", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var sales = LoadList();

        int index = sales.FindIndex(s => s.Id == item.Id);

        if (index == -1)
            throw new DalIsNotExistException("Sale not found");

        sales[index] = item;

        SaveList(sales);

        LogManager.WriteToLog("Finished updating sale", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);
    }

    public void Delete(int id)
    {
        LogManager.WriteToLog("Start deleting sale", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var sales = LoadList();

        var sale = sales.FirstOrDefault(s => s.Id == id);

        if (sale == null)
            throw new DalIsNotExistException("Sale not found");

        sales.Remove(sale);

        SaveList(sales);

        LogManager.WriteToLog("Finished deleting sale", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);
    }
}
