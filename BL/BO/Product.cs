


namespace BO
{
    public class Product
    {
      

        public int Id { get; init; }
        public string ProductName { get; init; }
        public Categries? Category { get; init; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
      
        public override string ToString() => this.ToStringProperty();
    }
}
