using AutoMapper;
using CompanyName.ServiceName.Api.Features.MeasurementPoints;
using CompanyName.ServiceName.Contracts.DataAccess;

namespace CompanyName.ServiceName.Api.Features
{
    internal sealed class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AddMeasurementPointRequest, PowerMeasurementPoint>()
                .ForMember(d => d.ConsumptionObject, o => o.Ignore())
                .ForMember(d => d.PowerMeasurementPointCalculationMeteringDevice, o => o.Ignore())
                .ForMember(d => d.Id, o => o.MapFrom(_ => 0));
        }
    }
}
