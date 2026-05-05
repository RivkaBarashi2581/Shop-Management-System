using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DO
{
    /// <summary>
    /// ישות לקוח
    /// בחנות איפור
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="CustomerName"></param>
    /// <param name="Address"></param>
    /// <param name="Phone"></param>
    public record Customer
        (
          int Id,
          string? CustomerName,
          string? Address,
          string? Phone
        )
    {
        public Customer() : this(0, null, null, null)
        {

        }
    }
}
