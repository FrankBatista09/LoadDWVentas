using LoadDWVentas.Data.Context.DWVentas;
using LoadDWVentas.Data.Context.Northwind;
using LoadDWVentas.Data.Entities.DWVentas;
using LoadDWVentas.Data.Interfaces;
using LoadDWVentas.Data.Result;
using Microsoft.EntityFrameworkCore;

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
        public Task<OperactionResult> LoadDHW()
        {
            throw new NotImplementedException();
        }

        public async Task<OperactionResult> LoadDimEmployee()
        {
            OperactionResult result = new();

            try
            {
                var employees = await _northwindContext.Employees.AsNoTracking().Select(emp => new DimEmployee()
                {
                    EmployeeId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                }).ToListAsync();

                await _salesContext.DimEmployees.AddRangeAsync(employees);
                await _salesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error cargando la dimension de empleado.";
            }
            return result;
        }

        public async Task<OperactionResult> LoadDimProductCategory()
        {
            OperactionResult result = new();

            try
            {
                var productCategories = await(from product in _northwindContext.Products
                                              join category in _northwindContext.Categories on product.CategoryId equals category.CategoryId
                                              select new DimProductCategory()
                                              {
                                                  CategoryId = category.CategoryId,
                                                  ProductName = product.ProductName,
                                                  CategoryName = category.CategoryName,
                                                  ProductId = product.ProductId
                                              }).AsNoTracking().ToListAsync();

                await _salesContext.DimProductCategories.AddRangeAsync(productCategories);
                await _salesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error cargando la dimension de ProductCategory.";
            }
            return result;
        }
    }
}
