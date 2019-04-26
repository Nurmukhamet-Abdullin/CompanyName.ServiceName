using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Api.Features.MeasurementPoints
{
    public sealed class GetMeasurementPointRequest
    {
        [Required]
        public long Id { get; set; }
    }
}
