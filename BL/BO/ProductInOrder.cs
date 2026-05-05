



namespace BO;

public class ProductInOrder
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public double BasePrice { get; set; }

    public int Amount { get; set; }

    public List<SaleInProduct>? Sales { get; set; }

    public double TotalPrice { get; set; }

    public override string ToString() => this.ToStringProperty();
}