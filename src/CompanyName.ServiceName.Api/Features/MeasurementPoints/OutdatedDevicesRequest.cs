using System.ComponentModel.DataAnnotations;

namespace CompanyName.ServiceName.Api.Features.MeasurementPoints
{
    // TODO add pagination
    public sealed class OutdatedDevicesRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string DeviceType { get; set; }
    }
}
