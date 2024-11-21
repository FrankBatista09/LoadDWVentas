using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWVentas.Data.Entities.DWVentas
{
    [Table("ProductCategories")]
    public class DimProductCategory
    {
        public int ProductKey { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        
    }
}
