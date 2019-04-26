using System;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    public sealed class PowerMeasurementPointCalculationMeteringDevice
    {
        public long PowerMeasurementPointId { get; set; }
        public PowerMeasurementPoint PowerMeasurementPoint { get; set; }

        public long CalculationMeteringDeviceId { get; set; }
        public CalculationMeteringDevice CalculationMeteringDevice { get; set; }

        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
    }
}
