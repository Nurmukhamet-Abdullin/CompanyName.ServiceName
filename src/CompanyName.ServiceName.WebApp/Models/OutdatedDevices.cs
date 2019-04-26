using CompanyName.ServiceName.Contracts.DataAccess;
using System.Collections.Generic;

namespace CompanyName.ServiceName.WebApp.Models
{
    public sealed class OutdatedDevices
    {
        public IReadOnlyList<ElectricityMeter> ElectricityMeters { get; set; }
        public IReadOnlyList<CurrentTransformer> CurrentTransformers { get; set; }
        public IReadOnlyList<VoltageTransformer> VoltageTransformers { get; set; }
    }
}
