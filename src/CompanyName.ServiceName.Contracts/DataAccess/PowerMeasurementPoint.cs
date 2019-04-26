using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Точка измерения электроэнергии </summary>
    public sealed class PowerMeasurementPoint
    {
        public long Id { get; set; }

        [Required]
        public ConsumptionObject ConsumptionObject { get; set; }

        [Required]
        public List<PowerMeasurementPointCalculationMeteringDevice>
            PowerMeasurementPointCalculationMeteringDevice { get; set; }

        [Required]
        [MaxLength(Consts.MaxNameLength)]
        public string Name { get; set; }

        public ElectricityMeter ElectricityMeter { get; set; }

        public CurrentTransformer CurrentTransformer { get; set; }

        public VoltageTransformer VoltageTransformer { get; set; }
    }
}
