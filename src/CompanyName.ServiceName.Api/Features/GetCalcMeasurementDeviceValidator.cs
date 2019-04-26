using FluentValidation;
using System;

namespace CompanyName.ServiceName.Api.Features
{
    public sealed class GetCalcMeasurementDeviceValidator : AbstractValidator<GetCalcMeasurementDeviceRequest>
    {
        public GetCalcMeasurementDeviceValidator()
        {
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate ?? DateTime.MinValue)
                .When(x => x.EndDate != null)
                .WithMessage("EndDate should be grater than StartDate");
        }
    }
}
