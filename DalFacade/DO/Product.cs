

namespace DO
{
    /// <summary>
    /// ישות מוצר 
    /// בחנות איפור
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ProductName"></param>
    /// <param name="Category"></param>
    /// <param name="Price"></param>
    /// <param name="Stock"></param>
    public record Product
        (
          int Id,
          string? ProductName,
          Categries Category,
          double? Price,
          int? Stock
        )
    {
        public Product()
            : this(0, null, Categries.Eyebrows, null, 0)
        {

        }
    }
}
