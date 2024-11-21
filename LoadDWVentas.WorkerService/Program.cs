using LoadDWVentas.Data.Context.DWVentas;
using LoadDWVentas.Data.Context.Northwind;
using LoadDWVentas.Data.Interfaces;
using LoadDWVentas.Data.Services;
using LoadDWVentas.WorkerService;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>{

            services.AddDbContext<NorthwindContext>(options =>
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("Northwind")));

            services.AddDbContext<DbSalesContext>(options =>
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DWSales")));

            services.AddScoped<IDataServiceDwVentas, DataServiceDwVentas>();

        });
}