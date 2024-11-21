using LoadDWVentas.Data.Entities.DWVentas;
using Microsoft.EntityFrameworkCore;

namespace LoadDWVentas.Data.Context.DWVentas
{
    public class DbSalesContext : DbContext
    {
        #region "DbSets"
        public DbSet<DimEmployee> DimEmployees { get; set; }
        public DbSet<DimProductCategory> DimProductCategories { get; set; }
        #endregion
        public DbSalesContext(DbContextOptions<DbSalesContext> options) : base(options)
        {

        }



    }
}
