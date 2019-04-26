using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Счетчик электрической энергии </summary>
    public sealed class ElectricityMeter : MeasurementDevice
    {
        /// <summary> Gets or sets electricity meter type. </summary>
        /// <value> Тип счетчика </value>
        // TODO may be complex object or Enum
        [Required]
        public string MeterType { get; set; }
    }
}
