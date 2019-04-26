using CompanyName.ServiceName.Contracts.DataAccess;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace CompanyName.ServiceName.Api.Features.MeasurementPoints
{
    public sealed class OutdatedDevicesValidator : AbstractValidator<OutdatedDevicesRequest>
    {
        private static readonly(string deviceTypesMessage, IReadOnlyCollection<string> typeNames) _ = GetMessage();
        public OutdatedDevicesValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id should be greater than zero.");

            RuleFor(x => x.DeviceType)
                .NotEmpty()
                .WithMessage(_.deviceTypesMessage)
                .Must(dt => _.typeNames.Select(t => t.ToUpperInvariant()).Any(x => x == dt.ToUpperInvariant()))
                .WithMessage(_.deviceTypesMessage);
        }

        private static(string _deviceTypesMessage, IReadOnlyCollection<string> typeNames) GetMessage()
        {
            var types = typeof(MeasurementDevice).Assembly.DefinedTypes
                .Where(t => t.IsSubclassOf(typeof(MeasurementDevice)) && !t.IsAbstract)
                .Select(t => t.Name)
                .ToArray();
            var deviceTypes = string.Join(", ", types);

            return ($"You should specify device type as one of {deviceTypes}.", types);
        }
    }
}
