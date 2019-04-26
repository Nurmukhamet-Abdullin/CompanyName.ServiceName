using CompanyName.ServiceName.Contracts.Api;
using CompanyName.ServiceName.Contracts.DataAccess;
using CompanyName.ServiceName.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CompanyName.ServiceName.WebApp.Controllers
{
    public sealed class HomeController : Controller
    {
        private static readonly DateTime StartDate = new DateTime(2018, 1, 1);
        private static readonly DateTime EndDate = new DateTime(2018, 12, 31);

        private readonly IServiceNameApi _api;

        public HomeController(IServiceNameApi api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<ViewResult> Index()
        {
            var outdatedDevices = new OutdatedDevices
            {
                ElectricityMeters =
                    await _api.GetOutdatedMeasurementDevices<ElectricityMeter>(1, nameof(ElectricityMeter)),
                CurrentTransformers =
                    await _api.GetOutdatedMeasurementDevices<CurrentTransformer>(1, nameof(CurrentTransformer)),
                VoltageTransformers =
                    await _api.GetOutdatedMeasurementDevices<VoltageTransformer>(1, nameof(VoltageTransformer))
            };

            return View(outdatedDevices);
        }

        public async Task<ViewResult> CalculationDevices()
        {
            var devices = await _api.GetCalculationDevices(StartDate, EndDate);

            return View(devices);
        }

        [HttpGet]
        public ViewResult CreateMeasurementPoint() => View();

        [HttpPost]
        public async Task<ViewResult> CreateMeasurementPoint(AddMeasurementPoint request)
        {
            if (!ModelState.IsValid) // if client-side validation was ignored
                return View(request);

            request.ConsumptionObjectId = 1; // TODO ConsumptionObjectId should be set by authorization
            var response = await _api.AddMeasurementPoint(request);

            if (response.Error == null)
                return View(new AddMeasurementPoint());

            ViewBag.Errors = response.Error.StatusCode == HttpStatusCode.BadRequest
                ? JsonConvert.DeserializeObject<ErrorResponse>(response.Error.Content).Errors
                : new[] { response.Error.Content };

            return View(request);
        }
    }
}
