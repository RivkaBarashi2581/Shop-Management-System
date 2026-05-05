using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BO
//{
//    public class Order
//    {
//        bool favorite;
//        List<ProductInOrder> ProductInOrder;
//        double sum;

//    }
//}

namespace BO;

public class Order
{
    public bool IsFavorite { get; set; }

    public List<ProductInOrder>? Products { get; set; }

    public double TotalPrice { get; set; }
    public override string ToString() => this.ToStringProperty();
}