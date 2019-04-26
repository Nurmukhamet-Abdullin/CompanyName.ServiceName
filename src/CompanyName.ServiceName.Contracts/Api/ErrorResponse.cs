using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyName.ServiceName.Contracts.Api
{
    public sealed class ErrorResponse
    {
        public ErrorResponse(IEnumerable<string> errors)
        {
            Errors = errors?.ToArray() ?? throw new ArgumentNullException(nameof(errors));
        }

        public IReadOnlyCollection<string> Errors { get; } = Array.Empty<string>();
    }
}
