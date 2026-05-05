using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface ISale
    {
        // כל פעולות הCRUD של המכירה
        int Create(BO.Sale item);
        BO.Sale? Read(int id);
        BO.Sale? Read(Func<BO.Sale, bool> filter);
        List<Sale?> ReadAll(Func<BO.Sale, bool>? filter = null);
        void Update(BO.Sale item);
        void Delete(int id);
    }
}
