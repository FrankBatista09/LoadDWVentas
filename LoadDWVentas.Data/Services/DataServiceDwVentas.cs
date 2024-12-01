using LoadDWVentas.Data.Context.DWVentas;
using LoadDWVentas.Data.Context.Northwind;
using LoadDWVentas.Data.Entities.DWVentas;
using LoadDWVentas.Data.Interfaces;
using LoadDWVentas.Data.Result;
using Microsoft.EntityFrameworkCore;
using System.Xml.XPath;

namespace LoadDWVentas.Data.Services
{
    public class DataServiceDwVentas : IDataServiceDwVentas
    {
        private readonly NorthwindContext _northwindContext;
        private readonly DbSalesContext _salesContext;
        public DataServiceDwVentas(NorthwindContext northwindContext,
                                   DbSalesContext salesContext)
        {
            _northwindContext = northwindContext;
            _salesContext = salesContext;
        }
        public async Task<OperationResult> LoadDHW()
        {
            OperationResult result = new OperationResult();
            try
            {
                //await ClearTable(_salesContext.dim_ProductCategories);
                //await ClearTable(_salesContext.dim_Customers);
                //await ClearTable(_salesContext.dim_Employees);
                //await ClearTable(_salesContext.fact_Orders);

                //await LoadDimProductCategory();
                //await LoadDimCustomers();
                //await LoadDimEmployee();
                //await LoadFactSales();
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"Error cargando el DWH Ventas. {ex.Message}";
            }

            return result;
        }

        private async Task<OperationResult> LoadDimEmployee()
        {
            OperationResult result = new();

            try
            {
                //await ClearTable(_salesContext.dim_ProductCategories);

                var employees = await _northwindContext.Employees.AsNoTracking().Select(emp => new dim_Employee()
                {
                    pk_employee_id = emp.EmployeeId,
                    first_name = emp.FirstName,
                    last_name = emp.LastName,
                }).ToListAsync();

                await _salesContext.dim_Employees.AddRangeAsync(employees);
                await _salesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error cargando la dimension de empleado.";
            }
            return result;
        }

        private async Task<OperationResult> LoadDimCustomers()
        {
            OperationResult result = new();
            try
            {
               // await ClearTable(_salesContext.dim_Customers);

                var customer = await _northwindContext.Customers.AsNoTracking().Select(emp => new dim_Customers()
                {
                    pk_customer_id = emp.CustomerId,
                    company_name = emp.CompanyName,
                    contact_name = emp.ContactName,

                }).ToListAsync();

                await _salesContext.dim_Customers.AddRangeAsync(customer);
                await _salesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error cargando la dimension de empleado.";
            }
            return result;
        }

        private async Task<OperationResult> LoadDimProductCategory()
        {
            OperationResult result = new();

            try
            {
               // await ClearTable(_salesContext.dim_ProductCategories);

                var productCategories = await(from product in _northwindContext.Products
                                              join category in _northwindContext.Categories on product.CategoryId equals category.CategoryId
                                              select new dim_ProductCategories()
                                              {
                                                  CategoryId = category.CategoryId,
                                                  ProductName = product.ProductName,
                                                  CategoryName = category.CategoryName,
                                                  ProductId = product.ProductId
                                              }).AsNoTracking().ToListAsync();

                await _salesContext.dim_ProductCategories.AddRangeAsync(productCategories);
                await _salesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error cargando la dimension de ProductCategory.";
            }
            return result;
        }

        private async Task ClearTable<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
        {
            try
            {
                await _salesContext.Database.ExecuteSqlRawAsync("DELETE FROM " + dbSet.EntityType.GetTableName());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al vaciar la tabla {dbSet.EntityType.GetTableName()}: {ex.Message}");
            }
        }

        private async Task<OperationResult> LoadFactSales()
        {
            OperationResult result = new();

            try
            {
                var ventas = await _northwindContext.Vwventas.AsNoTracking().ToListAsync();
                int[] ordersId = await _salesContext.fact_Orders.Select(cd => cd.pk_order_id).ToArrayAsync();

                if (ordersId.Any())
                {
                    await _salesContext.fact_Orders.Where(cd => ordersId.Contains(cd.pk_order_id))
                                                  .AsNoTracking()
                                                  .ExecuteDeleteAsync();
                }
                int con = 1;
                foreach (var venta in ventas)
                {
                    var customer = await _salesContext.dim_Customers.SingleOrDefaultAsync(cust => cust.pk_customer_id == venta.CustomerId);
                    var employee = await _salesContext.dim_Employees.SingleOrDefaultAsync(emp => emp.pk_employee_id == venta.EmployeeId);
                    var ventass = await _northwindContext.Vwventas.FirstOrDefaultAsync(emp => emp.CustomerId == venta.CustomerId);
                    var product = await _salesContext.dim_ProductCategories.FirstOrDefaultAsync(emp => emp.ProductId == venta.ProductID);
                    if (customer == null || employee == null)
                    {
                        // Handle the case where customer or employee is not found
                        continue; // Skip to the next venta
                    }
                    
                    fact_orders factOrder = new fact_orders()
                    {
                        fact_sales_quantity = venta.Cantidad.Value,
                        fk_customer_id = customer.pk_customer_id,
                        fk_employee_id = employee.pk_employee_id,
                        fact_sales_discount = (float?)Convert.ToDecimal(venta.TotalVentas),
                        fk_product_id = product.ProductKey,
                        order_date = int.Parse(string.Concat(ventass.Año, "", ventass.Mes)),
                        pk_order_id = con
                        
                    }; 

                    await _salesContext.fact_Orders.AddAsync(factOrder);
                    await _salesContext.SaveChangesAsync();
                    con++;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error cargando el fact de Sales.";
            }
            return result;
        }

    }
}
