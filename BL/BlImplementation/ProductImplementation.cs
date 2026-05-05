using BlApi;
using BO;
using DalFacade.DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class ProductImplementation:IProduct
    {
        private DalFacade.DalApi.IDal _dal = DalApi.Factory.Get;

        public int Create(BO.Product item)
        {
            try
            {
                return _dal.Product.Create(item.ToDO());
            }
            catch (DO.DalIsExistException ex)
            {
                throw new BO.BlAlreadyExistsException("Product already exists", ex);
            }
        }

        public BO.Product? Read(int id)
        {
            try
            {
                DO.Product? product = _dal.Product.Read(id);
                return product == null ? null : product.ToBO();
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Product does not exist", ex);
            }
        }

        public BO.Product? Read(Func<BO.Product, bool> filter)
        {
            return ReadAll(filter).FirstOrDefault();
        }

        public List<BO.Product?> ReadAll(Func<BO.Product, bool>? filter = null)
        {
            try
            {
                var list = _dal.Product.ReadAll()
                    .Where(p => p != null)
                    .Select(p => p!.ToBO());

                if (filter != null)
                    list = list.Where(filter);

                return list.Cast<BO.Product?>().ToList();
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Product does not exist", ex);
            }
        }

        public void Update(BO.Product item)
        {
            try
            {
                _dal.Product.Update(item.ToDO());
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Product does not exist", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _dal.Product.Delete(id);
            }
            catch (DO.DalIsNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Product does not exist", ex);
            }
        }
    }

}
