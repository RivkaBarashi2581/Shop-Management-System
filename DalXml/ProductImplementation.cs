
using DalApi;
using DalFacade.DalApi;
using DalXml;
using DO;
using System.Reflection;
using System.Xml.Serialization;
using Tools;

namespace Dal;

internal class ProductImplementation : IProduct
{
    // כתיבה לקבצי XML
    private static readonly string path = Path.Combine(AppContext.BaseDirectory, "xml", "Products.xml");

    private static readonly XmlSerializer serializer = new(typeof(List<Product>));

    private List<Product> LoadList()
    {
        if (!File.Exists(path))
            return new List<Product>();

        using StreamReader sr = new StreamReader(path);
        return serializer.Deserialize(sr) as List<Product> ?? new List<Product>();
    }

    private void SaveList(List<Product> list)
    {
        using StreamWriter sw = new StreamWriter(path);
        serializer.Serialize(sw, list);
    }

    public int Create(Product item)
    {
        LogManager.WriteToLog("Start creating product", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var products = LoadList();

        if (products.Any(p => p.Id == item.Id))
            throw new DalIsExistException("Product already exists");

        int newId = Config.GetProductId;
        Product newProduct = item with { Id = newId };

        products.Add(newProduct);
        SaveList(products);

        LogManager.WriteToLog("Finished creating product", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return newProduct.Id;
    }

    public Product? Read(int id)
    {
        LogManager.WriteToLog("Start reading product by ID", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var products = LoadList();

        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            throw new DalIsNotExistException("Product not found");

        LogManager.WriteToLog("Finished reading product by ID", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return product;
    }

    public Product? Read(Func<Product, bool> filter)
    {
        LogManager.WriteToLog("Start reading product by filter", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        if (filter == null)
            throw new ArgumentNullException(nameof(filter));

        var products = LoadList();

        var product = products.FirstOrDefault(filter);

        if (product == null)
            throw new DalIsNotExistException("No product matches filter");

        LogManager.WriteToLog("Finished reading product by filter", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return product;
    }

    public List<Product?> ReadAll(Func<Product, bool>? filter = null)
    {
        LogManager.WriteToLog("Start reading all products", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var products = LoadList();

        var result = (filter == null)
            ? products
            : products.Where(filter).ToList();

        LogManager.WriteToLog("Finished reading all products", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        return result.Cast<Product?>().ToList();
    }

    public void Update(Product item)
    {
        LogManager.WriteToLog("Start updating product", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var products = LoadList();

        int index = products.FindIndex(p => p.Id == item.Id);

        if (index == -1)
            throw new DalIsNotExistException("Product not found");

        products[index] = item;

        SaveList(products);

        LogManager.WriteToLog("Finished updating product", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);
    }

    public void Delete(int id)
    {
        LogManager.WriteToLog("Start deleting product", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);

        var products = LoadList();

        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            throw new DalIsNotExistException("Product not found");

        products.Remove(product);

        SaveList(products);

        LogManager.WriteToLog("Finished deleting product", GetType().FullName, MethodBase.GetCurrentMethod()!.Name);
    }

}