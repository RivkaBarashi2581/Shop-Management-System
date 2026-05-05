using DO;


using DalApi;

namespace DalFacade.DalApi;

public interface IDal
{
     ICustomer  Customer { get; }
     IProduct Product { get;}
     ISale Sale { get; }

}
