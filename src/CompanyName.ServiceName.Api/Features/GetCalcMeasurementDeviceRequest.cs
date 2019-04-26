using System;

namespace CompanyName.ServiceName.Api.Features
{
    // TODO add pagination
    public sealed class GetCalcMeasurementDeviceRequest
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
