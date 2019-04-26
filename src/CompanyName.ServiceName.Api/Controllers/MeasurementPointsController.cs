using AutoMapper;
using CompanyName.ServiceName.Api.Features.MeasurementPoints;
using CompanyName.ServiceName.Contracts.Api;
using CompanyName.ServiceName.Contracts.DataAccess;
using CompanyName.ServiceName.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.ServiceName.Api.Controllers
{
    [Route("api/mp")]
    [Produces(Consts.ApplicationJson)]
    public sealed class MeasurementPointsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MeasurementPointsController(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{Id:int}/outdated{DeviceType}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> OutdatedMeasurementDevices(OutdatedDevicesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse(
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
            }

            // This can not be taken out into one LINQ query,
            // because then LINQ will be calculated on client side:
            // https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            // I hope it will be fixed in EF Core 3.0:
            // https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.0/breaking-changes#linq-queries-are-no-longer-evaluated-on-the-client
            var initialQuery = _context.PowerMeasurementPoints.Where(co => co.ConsumptionObject.Id == request.Id);
            var deviceType = request.DeviceType.ToUpperInvariant();

            if (deviceType == nameof(ElectricityMeter).ToUpperInvariant())
            {
                return Ok(await initialQuery
                    .Where(co => co.ElectricityMeter.VerificationDate <= DateTime.UtcNow)
                    .Select(p => p.ElectricityMeter)
                    .ToListAsync());
            }

            if (deviceType == nameof(CurrentTransformer).ToUpperInvariant())
            {
                return Ok(await initialQuery
                    .Where(co => co.CurrentTransformer.VerificationDate <= DateTime.UtcNow)
                    .Select(p => p.CurrentTransformer)
                    .ToListAsync());
            }

            if (deviceType == nameof(VoltageTransformer).ToUpperInvariant())
            {
                return Ok(await initialQuery
                    .Where(co => co.VoltageTransformer.VerificationDate <= DateTime.UtcNow)
                    .Select(p => p.VoltageTransformer)
                    .ToListAsync());
            }

            return NotFound($"Device type {request.DeviceType} is not valid.");
        }

        [HttpGet("{Id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PowerMeasurementPoint>> Get(GetMeasurementPointRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse(
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
            }

            var pmp = await _context.PowerMeasurementPoints.FindAsync(request.Id);
            if (pmp == null)
                return NotFound($"Power Measurement Point with Id '{request.Id}' not found.");

            return pmp;
        }

        /// <summary>
        /// Add new power measurement point.
        /// </summary>
        /// <param name="request">New Power Measurement Point</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Created Power Measurement Point</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PowerMeasurementPoint>> Add(
            [FromBody]AddMeasurementPointRequest request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse(
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
            }

            var mp = _mapper.Map<PowerMeasurementPoint>(request);

            mp.ConsumptionObject = _context.ConsumptionObjects.Find(request.ConsumptionObjectId);
            if (mp.ConsumptionObject == null)
                return NotFound($"Consumption Object with id '{request.ConsumptionObjectId}' not found.");

            await _context.PowerMeasurementPoints.AddAsync(mp, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(Get), new { id = mp.Id }, request);
        }
    }
}
