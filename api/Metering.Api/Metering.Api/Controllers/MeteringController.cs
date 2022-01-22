using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Metering.Api.Hubs;
using Metering.Api.Models;
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

        public MeteringController(IHubContext<MeteringHub> meteringHubContext)
        {
            this.meteringHubContext = meteringHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MeasurementDto measurement)
        {
            Console.WriteLine(JsonSerializer.Serialize(measurement));
            await meteringHubContext.Clients.All.SendAsync("MeasurementAdded", measurement);
            return Ok();
        }
    }
}