using Microsoft.AspNetCore.Mvc;
using System;

namespace TestServerExtensions.UnitTests.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleParametersInRouteGetController : ControllerBase
    {
        [HttpGet("path/{value}")]
        public IActionResult RouteWithIntParameter(int value = 0)
        {
            return Ok(value);
        }

        [HttpGet("path/{value}")]
        public IActionResult RouteWithStringParameter(string value = "")
        {
            return Ok(value);
        }

        [HttpGet("path/{value}")]
        public IActionResult RouteWithFloatParameter(float value = 0F)
        {
            return Ok(value);
        }

        [HttpGet("path/{value}")]
        public IActionResult RouteWithDecimalParameter(decimal value = 0M)
        {
            return Ok(value);
        }

        [HttpGet("path/{value}")]
        public IActionResult RouteWithCharParameter(char value = char.MinValue)
        {
            return Ok(value);
        }

        [HttpGet("path/{value}")]
        public IActionResult RouteWithGuidParameter(Guid value)
        {
            return Ok(value);
        }

        [HttpGet("path/{a}/{b}/{c}/{d}/{e}/{f}")]
        public IActionResult RouteWithSomeParameters(int a, string b, float c, decimal d, char e, Guid f)
        {
            return Ok();
        }

        [HttpGet("path/{a}/{b}/{c}/{d}/{e}")]
        public IActionResult RouteWithSomeParametersNotAllInRoute(int a, string b, float c, decimal d, char e, Guid f)
        {
            return Ok();
        }
    }
}
