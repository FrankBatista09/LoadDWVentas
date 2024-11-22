using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDWVentas.Data.Context.DWVentas
{
    public class fact_orders
    {
        [Key]
        public int pk_order_id {  get; set; }
        public int order_date { get; set; }
        public string fk_customer_id { get; set; }
        public int fk_employee_id { get; set; }
        public int fk_product_id { get; set; }
        public int fact_sales_quantity { get; set; }
        public float? fact_sales_discount { get; set; }
        
    }
}
