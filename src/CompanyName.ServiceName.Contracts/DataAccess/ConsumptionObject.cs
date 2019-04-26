using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Объект потребления </summary>
    public sealed class ConsumptionObject
    {
        public int Id { get; set; }

        [Required]
        public ChildOrganization Organization { get; set; }

        /// <summary> Gets or sets name of consumprion object. </summary>
        /// <value> Название объекта потребления. </value>
        /// <example>"ПС 110/10 Весна"</example>
        [Required]
        [MaxLength(Consts.MaxNameLength)]
        public string Name { get; set; }

        // TODO May be complex object
        [Required]
        public string Address { get; set; }

        public List<PowerSupplyPoint> PowerSupplyPoints { get; set; }
            = new List<PowerSupplyPoint>();

        public List<PowerMeasurementPoint> PowerMeasurementPoints { get; set; }
            = new List<PowerMeasurementPoint>();
    }
}
