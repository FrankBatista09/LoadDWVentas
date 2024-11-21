using LoadDWVentas.Data.Entities.Northwind;
using Microsoft.EntityFrameworkCore;

namespace LoadDWVentas.Data.Context.Northwind
{
    public class NorthwindContext : DbContext
    {
        #region "DbSets"
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {

        }
    }
}
