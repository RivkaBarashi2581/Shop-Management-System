
using BlApi;
using BO;
namespace BlImplementation
{
    internal class SaleImplementation:ISale
    {
        private DalFacade.DalApi.IDal _dal = DalApi.Factory.Get;
        public int Create(BO.Sale item)
        {
            try
            {
                return _dal.Sale.Create(item.ToDO());
            }
            catch (DO.DalIsExistException ex)
            {
                throw new BO.BlAlreadyExistsException("Sale already exists", ex);
            }
        }

        public BO.Sale? Read(int id)
        {
            try
            {
                DO.Sale? sale = _dal.Sale.Read(id);
                return sale == null ? null : sale.ToBO();
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Sale does not exist", ex);
            }
        }

        public BO.Sale? Read(Func<BO.Sale, bool> filter)
        {
            return ReadAll(filter).FirstOrDefault();
        }

        public List<BO.Sale?> ReadAll(Func<BO.Sale, bool>? filter = null)
        {
            try
            {
                var list = _dal.Sale.ReadAll()
                    .Where(s => s != null)
                    .Select(s => s!.ToBO());

                if (filter != null)
                    list = list.Where(filter);

                return list.Cast<BO.Sale?>().ToList();
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Sale does not exist", ex);
            }
        }

        public void Update(BO.Sale item)
        {
            try
            {
                _dal.Sale.Update(item.ToDO());
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Sale does not exist", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _dal.Sale.Delete(id);
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Sale does not exist", ex);
            }
        }
    }
}
