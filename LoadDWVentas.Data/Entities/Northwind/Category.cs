using System.ComponentModel.DataAnnotations;

namespace LoadDWVentas.Data.Entities.Northwind
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
