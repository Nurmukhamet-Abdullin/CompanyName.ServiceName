using CompanyName.ServiceName.Api.Features;
using CompanyName.ServiceName.Contracts.Api;
using CompanyName.ServiceName.Contracts.DataAccess;
using CompanyName.ServiceName.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyName.ServiceName.Api.Controllers
{
    [Route("api/cmd")]
    [Produces(Consts.ApplicationJson)]
    public sealed class CalculationMeteringDevicesController : ControllerBase
    {
        private readonly DataContext _context;

        public CalculationMeteringDevicesController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<CalculationMeteringDevice>>> GetCalculationDevices(
            GetCalcMeasurementDeviceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse(
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
            }

            // TODO these ids should be defined by authorization
            var mymeasurementPointIds = new[] { 1L };
            var startDate = request.StartDate ?? DateTime.MinValue;
            var endDate = request.EndDate ?? DateTime.MaxValue;

            var ids = await _context.CalculationMeteringDevices
                .SelectMany(pmp => pmp.PowerMeasurementPointCalculationMeteringDevice)
                .Where(x => mymeasurementPointIds.Contains(x.PowerMeasurementPointId)
                            && ((x.StartPeriod.Date < startDate && x.EndPeriod >= endDate)
                                || (x.StartPeriod.Date >= startDate && x.StartPeriod < endDate)
                                || (x.EndPeriod.Date >= startDate && x.EndPeriod < endDate)))
                .Select(x => x.CalculationMeteringDeviceId)
                .ToArrayAsync();

            var devices = await _context.CalculationMeteringDevices
                .Where(x => ids.Contains(x.Id))
                .ToArrayAsync();

            return devices;
        }
    }
}
