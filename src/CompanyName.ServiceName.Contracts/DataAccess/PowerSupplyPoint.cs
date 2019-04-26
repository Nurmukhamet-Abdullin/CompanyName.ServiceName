using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Точка поставки электроэнергии </summary>
    public sealed class PowerSupplyPoint
    {
        public long Id { get; set; }

        [Required]
        public ConsumptionObject ConsumptionObject { get; set; }

        public double MaximumPower { get; set; }

        [Required]
        [MaxLength(Consts.MaxNameLength)]
        public string Name { get; set; }
    }
}
