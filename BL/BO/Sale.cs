
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Sale
    {
       

        public int Id { get; init; }
        public int ProductId { get; init; }
        public int QuantityRequier { get; set; }
        public double? SalePrice { get; set; }
        public DateTime? StartSale { get; set; }
        public DateTime ?EndSale { get; set; }
    
       public override string ToString() => this.ToStringProperty();
    }

}
