using LoadDWVentas.Data.Entities.DWVentas;
using Microsoft.EntityFrameworkCore;

namespace LoadDWVentas.Data.Context.DWVentas
{
    public class DbSalesContext : DbContext
    {
        #region "DbSets"
        public DbSet<dim_Employee> dim_Employees { get; set; }
        public DbSet<dim_ProductCategories> dim_ProductCategories { get; set; }
        #endregion
        public DbSalesContext(DbContextOptions<DbSalesContext> options) : base(options)
        {

        }



    }
}
