using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    public abstract class Organization
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(Consts.MaxNameLength)]
        public string Name { get; set; }

        // TODO May be complex object
        [Required]
        public string Address { get; set; }
    }
}
