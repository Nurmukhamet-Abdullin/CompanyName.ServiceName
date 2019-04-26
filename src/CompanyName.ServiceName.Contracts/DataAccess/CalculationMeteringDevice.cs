using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Рсчетный прибор учета </summary>
    public sealed class CalculationMeteringDevice
    {
        public long Id { get; set; }

        [Required]
        public List<PowerMeasurementPointCalculationMeteringDevice>
            PowerMeasurementPointCalculationMeteringDevice { get; set; }
    }
}
