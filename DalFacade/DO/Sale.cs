using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DO;



namespace DO
{
    /// <summary>
    /// ישות מבצע
    /// בחנות איפור
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ProductId"></param>
    /// <param name="QuantityRequier"></param>
    /// <param name="SalePrice"></param>
    /// <param name="IsSaleToCustomer"></param>
    /// <param name="StartSale"></param>
    /// <param name="EndSale"></param>
    public record Sale
        (
          int Id,
          int ProductId,
          int QuantityRequier,
          double? SalePrice,
          bool IsSaleToCustomer,
          DateTime? StartSale,
          DateTime? EndSale
        )
    {
        public Sale() : this(0, 0, 0, null, false, null, null)
        {
        }
    }
 

}

