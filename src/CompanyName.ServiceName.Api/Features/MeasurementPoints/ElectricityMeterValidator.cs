using CompanyName.ServiceName.Contracts.DataAccess;
using CompanyName.ServiceName.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace CompanyName.ServiceName.Api.Features.MeasurementPoints
{
    public sealed class ElectricityMeterValidator : AbstractValidator<ElectricityMeter>
    {
        public ElectricityMeterValidator(DataContext context)
        {
            RuleFor(x => x.Number)
                .GreaterThan(0)
                .WithMessage("Electricity Meter's Number should be positive.");

            // This can not be taken out to base abstact class MeasurementDeviceValidator,
            // because then LINQ will be calculated on client side:
            // https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            // I hope it will be fixed in EF Core 3.0:
            // https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.0/breaking-changes#linq-queries-are-no-longer-evaluated-on-the-client
            RuleFor(x => x.Number)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var pmp = await context.PowerMeasurementPoints
                        .FirstOrDefaultAsync(p => p.ElectricityMeter.Number == x, cancellationToken);
                    return pmp == null;
                })
                .WithMessage(x => $"There is already exist Electricity Meter with Number '{x.Number}'");

            RuleFor(x => x.MeterType)
                .NotEmpty()
                .WithMessage("Specify Electricity Meter's Type.");

            RuleFor(x => x.VerificationDate)
                .ExclusiveBetween(new DateTime(2010, 1, 1), DateTime.UtcNow.Date.AddYears(5))
                .WithMessage("Electricity Meter's Transformation Ratio should be positive.");
        }
    }
}
