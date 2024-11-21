using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWVentas.Data.Entities.DWVentas
{
    [Table("dim_Employees")]
    public class dim_Employee
    {
        [Key]
        public int pk_employee_id { get; set; }
        public string? last_name { get; set; }
        public string? first_name { get; set; }
    }
}
