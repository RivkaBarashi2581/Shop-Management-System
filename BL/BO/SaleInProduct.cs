namespace BO;

public class SaleInProduct
{
    public int SaleId { get; set; }

    public int QuantityForSale { get; set; }

    public double Price { get; set; }

    public bool ForEveryone { get; set; }

    public override string ToString() => this.ToStringProperty();
}