using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Metering.Api.Hubs;
using Metering.Api.Models;
using Metering.Api.Services;
using Metering.Api.TableStorageEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Metering.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeteringController : ControllerBase
    {
        private readonly IHubContext<MeteringHub> meteringHubContext;
        private readonly TableStorageService tableStorageService;

        public MeteringController(IHubContext<MeteringHub> meteringHubContext, TableStorageService tableStorageService)
        {
            this.meteringHubContext = meteringHubContext;
            this.tableStorageService = tableStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MeasurementDto measurementDto)
        {
            Console.WriteLine(JsonSerializer.Serialize(measurementDto));
            var measurement = new Measurement(measurementDto);
            await tableStorageService.InsertOrMergeAsync(measurement);
            await meteringHubContext.Clients.All.SendAsync("MeasurementAdded", measurementDto);
            return Ok();
        }
    }
}