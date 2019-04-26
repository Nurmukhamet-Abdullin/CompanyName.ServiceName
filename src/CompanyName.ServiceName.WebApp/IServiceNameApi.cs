using CompanyName.ServiceName.Contracts.DataAccess;
using CompanyName.ServiceName.WebApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyName.ServiceName.WebApp
{
    public interface IServiceNameApi
    {
        [Post("/mp")]
        Task<ApiResponse<PowerMeasurementPoint>> AddMeasurementPoint([Body] AddMeasurementPoint request);

        [Get("/mp/{id}")]
        Task<ApiResponse<PowerMeasurementPoint>> GetMeasurementPoint(long id);

        [Get("/mp/{id}/outdated{deviceType}")]
        Task<List<T>> GetOutdatedMeasurementDevices<T>(long id, string deviceType)
            where T : MeasurementDevice;

        [Get("/cmd")]
        Task<List<CalculationMeteringDevice>> GetCalculationDevices(DateTime? startDate, DateTime? endDate);
    }
}