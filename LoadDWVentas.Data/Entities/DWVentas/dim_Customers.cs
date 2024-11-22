using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace LoadDWVentas.Data.Entities.DWVentas
{
    [Table("dim_Customers")]
    public class dim_Customers
    {
        [Key]
        public string? pk_customer_id {  get; set; }  
        public string? company_name { get; set; }   
        public string? contact_name { get; set; }

    }
}
