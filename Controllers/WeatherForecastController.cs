using api_DISCON.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_DISCON.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        disconCTX ctx;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, disconCTX _ctx)
        {
            _logger = logger;
            ctx = _ctx;
        }

        [HttpGet]
        public IEnumerable<Credenciales> Get()
        {
            return ctx.Credenciales.ToList();
        }
    }
}
