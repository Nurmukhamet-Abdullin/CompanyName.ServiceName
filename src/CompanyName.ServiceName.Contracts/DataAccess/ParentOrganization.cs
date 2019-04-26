using System.Collections.Generic;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Организация </summary>
    public sealed class ParentOrganization : Organization
    {
        public List<ChildOrganization> ChildOrganizations { get; set; }
            = new List<ChildOrganization>();
    }
}
