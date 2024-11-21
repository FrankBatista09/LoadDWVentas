﻿using LoadDWVentas.Data.Entities.DWVentas;
using LoadDWVentas.Data.Result;

namespace LoadDWVentas.Data.Interfaces
{
    public interface IDataServiceDwVentas
    {
        Task<OperactionResult> LoadDHW();
        Task<OperactionResult> LoadDimEmployee();
        Task<OperactionResult> LoadDimProductCategory();
    }
}