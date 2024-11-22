namespace LoadDWVentas.Data.Entities.Northwind
{
    public class Vwventas
    {
        public string? CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public int ShipperId { get; set; }

        public string? CompanyName { get; set; }

        public int? Año { get; set; }

        public int? Mes { get; set; }

        public double? TotalVentas { get; set; }

        public int? Cantidad { get; set; }
    }
}
