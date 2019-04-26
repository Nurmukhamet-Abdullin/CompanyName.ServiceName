using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Дочерняя организация </summary>
    public sealed class ChildOrganization : Organization
    {
        [Required]
        public ParentOrganization ParentOrganization { get; set; }

        public List<ConsumptionObject> ConsumptionObject { get; set; }
            = new List<ConsumptionObject>();
    }
}
