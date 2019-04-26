using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Трансформатор напряжения </summary>
    public sealed class VoltageTransformer : Transformer
    {
        /// <summary> Gets or sets voltage transformer's type. </summary>
        /// <value> Тип трансформатора напряжения </value>
        // TODO may be complex object or Enum
        // that is why it is not in base Transformer class
        [Required]
        public string Type { get; set; }
    }
}
