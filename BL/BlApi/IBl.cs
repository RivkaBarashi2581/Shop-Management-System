namespace BlApi
{
    public interface IBl
    {
        public ICustomer Customer { get; }
        public IProduct Product { get; }
        public ISale Sale { get; }
        IOrder Order { get; }
    }
}
