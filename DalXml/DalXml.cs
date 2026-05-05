using DalApi;
using DalFacade.DalApi;

namespace Dal;

internal sealed class DalXml : IDal
{
    // יצירת מופע של כל ממשק עם המימוש המתאים
    public ICustomer Customer => new CustomerImplementation();
    public IProduct Product => new ProductImplementation();
    public ISale Sale => new SaleImplementation();

    private static readonly DalXml instance = new DalXml();

    public static DalXml Instance => instance;

    private DalXml() { }
}