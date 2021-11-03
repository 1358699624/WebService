using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwaggerAPI.DALServer;
using Newtonsoft.Json;

namespace SwaggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public WeatherForecastController(UserInfoServer schoolUserInfoContext)
        {
            _userInfoContext = schoolUserInfoContext;

        }
        private readonly UserInfoServer _userInfoContext;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
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
        /// <summary>
        /// Json查询
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get_Linq_Userinfo(JsonResult searchTerm)
        {


            var searchTerm1 = $"where UserName ='{searchTerm}'";


            var linqs = _userInfoContext.UserInfos.FromSqlInterpolated($"SELECT * FROM dbo.UserInfo ({searchTerm1})").Where(b => b.Id < 1000)
    .OrderByDescending(b => b.Id)
    .ToList();

            return Json {linqs };

        }*/
    }
}
