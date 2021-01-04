using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger /*IBankAccount readerAccount,*/ /*PremiumBankAccount readerAccount*/)
        {
            string weatherRegion = "abcdefdad";
            string imei = weatherRegion[2..4];
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /*
         *  Send request with url https://localhost:44374/weatherforecast/isonduty with POST Method
         *  Request Headers:
            Content-Type: application/json
            Accept: application/json
            User-Agent: PostmanRuntime/7.26.1
            Postman-Token: bb41d39e-a045-4daf-942f-7613201b62a2
            Host: localhost:44374
            Accept-Encoding: gzip, deflate, br
            Connection: keep-alive
            Content-Length: 2
         *  Request Body:
            {"MonitorId":12}
         *  if without [Route("isonduty")], the url should be https://localhost:44374/weatherforecast or https://localhost:44374/weatherforecast/
         */
        [HttpPost]
        [Route("isonduty")]
        //public bool PostOnDuty([FromBody]WeatherMonitor person)   // the effect is same as without any attribute
        public bool PostOnDuty(WeatherMonitor person)
        {
            return true;
        }

        /*
         *  POST https://localhost:44374/weatherforecast/isonduty-form HTTP/1.1
            Content-Type: application/x-www-form-urlencoded
            Accept: application/json
            User-Agent: PostmanRuntime/7.26.1
            Postman-Token: 70855446-a5ba-4f1a-bb1f-a90d78ea407e
            Host: localhost:44374
            Accept-Encoding: gzip, deflate, br
            Connection: keep-alive
            Content-Length: 12

            MonitorId=12

            For binding successfully, [FromForm] have to be added
         */
        [HttpPost]
        [Route("isonduty-form")]
        public string PostOnDuty2([FromForm]WeatherMonitor person)   
        {
            return "from form";
        }

        [HttpGet]
        [Route("heartbeat")]
        public ActionResult<string> IsWorking()
        {
            return Ok();
        }
    }
}
