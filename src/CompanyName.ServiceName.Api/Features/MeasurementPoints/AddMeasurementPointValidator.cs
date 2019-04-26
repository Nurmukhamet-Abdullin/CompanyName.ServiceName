using FluentValidation;

namespace CompanyName.ServiceName.Api.Features.MeasurementPoints
{
    public sealed class AddMeasurementPointValidator : AbstractValidator<AddMeasurementPointRequest>
    {
        public AddMeasurementPointValidator()
        {
            RuleFor(x => x.ConsumptionObjectId)
                .GreaterThan(0)
                .WithMessage($"Consumption Object has to have positive Identifier.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Specify Name of Power Measurement Point.");

            RuleFor(x => x.ElectricityMeter)
                .NotNull()
                .WithMessage("You should specify Electricity Meter.");

            RuleFor(x => x.CurrentTransformer)
                .NotNull()
                .WithMessage("You should specify Current Transformer.");

            RuleFor(x => x.VoltageTransformer)
                .NotNull()
                .WithMessage("You should specify Voltage Transformer.");
        }
    }
}
