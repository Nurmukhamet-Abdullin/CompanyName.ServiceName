using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    /// <summary> Трансформатор тока </summary>
    public sealed class CurrentTransformer : Transformer
    {
        /// <summary> Gets or sets current transformer's type. </summary>
        /// <value> Тип трансформатора тока </value>
        // TODO may be complex object or Enum
        // that is why it is not in base Transformer class
        [Required]
        public string Type { get; set; }
    }
}
