
using BlApi;
using BO;

namespace BlImplementation
{
    internal class CustomerImplementation : ICustomer
    {
        private DalFacade.DalApi.IDal _dal = DalApi.Factory.Get;
        
        public int Create(BO.Customer item)
        {
            try
            {
                return _dal.Customer.Create(item.ToDO());
            }
            catch (DO.DalIsExistException ex)
            {
                throw new BO.BlAlreadyExistsException("Customer already exists", ex);
            }
        }

        public BO.Customer? Read(int id)
        {
            try
            {
                DO.Customer? customer = _dal.Customer.Read(id);
                return customer == null ? null : customer.ToBO();
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Customer does not exist", ex);
            }
        }

        public BO.Customer? Read(Func<BO.Customer, bool> filter)
        {
            try
            {
                return ReadAll(filter).FirstOrDefault();
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Customer does not exist", ex);
            }
        }

        public List<BO.Customer?> ReadAll(Func<BO.Customer, bool>? filter = null)
        {
            try
            {
                var list = _dal.Customer.ReadAll()
                    .Where(c => c != null)
                    .Select(c => c!.ToBO());

                if (filter != null)
                    list = list.Where(filter);

                return list.Cast<BO.Customer?>().ToList();
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Customer does not exist", ex);
            }
        }

        public void Update(BO.Customer item)
        {
            try
            {
                _dal.Customer.Update(item.ToDO());
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Customer does not exist", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _dal.Customer.Delete(id);
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Customer does not exist", ex);
            }
        }
    }
}

