using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWVentas.Data.Entities.DWVentas
{
    [Table("DimEmployees")]
    public class DimEmployee
    {
        public int EmployeeId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
    }
}
