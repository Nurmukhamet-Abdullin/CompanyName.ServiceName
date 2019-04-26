using FluentValidation;

namespace CompanyName.ServiceName.Api.Features.MeasurementPoints
{
    public sealed class GetMeasurementPointValidator : AbstractValidator<GetMeasurementPointRequest>
    {
        public GetMeasurementPointValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage($"Power Measurement Point's Id should be positive.");
        }
    }
}
