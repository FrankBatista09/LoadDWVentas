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
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Vwvventas> Vwventas { get; set; }
        #endregion
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vwvventas>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("Vwvventas");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .HasColumnName("CustomerID");
                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(31);
                entity.Property(e => e.ProductID).HasColumnName("ProductID");
                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
            });
        }
    }
}
