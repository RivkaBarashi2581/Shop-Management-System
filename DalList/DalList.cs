using DO;

using DalApi;
using DalFacade.DalApi;



namespace Dal
{
    internal sealed class DalList : IDal

    {
        public ICustomer Customer => new CustomerImplementation();
        public IProduct Product => new ProductImplementation();
        public ISale Sale => new SaleImplementation();

        private  static readonly DalList instance=new DalList();
        public  static DalList Instance { get { return instance; } }

        private DalList()
        {

        }

    }
}
