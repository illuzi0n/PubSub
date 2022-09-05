using API.Abstractions;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISomeService _someService;

        public WeatherForecastController(ISomeService someService)
        {
            _someService = someService;
        }

        [HttpPost]
        public IActionResult Post(SomeDto someDto)
        {
            _someService.Create(someDto);

            return Ok();
        }
    }
}