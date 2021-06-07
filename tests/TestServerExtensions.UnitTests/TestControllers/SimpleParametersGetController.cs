using Microsoft.AspNetCore.Mvc;
using System;

namespace TestServerExtensions.UnitTests.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleParametersGetController : ControllerBase
    {
        [HttpGet("path")]
        public IActionResult RouteWithoutParameters()
        {
            return Ok();
        }

        [HttpGet("path")]
        public IActionResult RouteWithIntParameter(int value = 0)
        {
            return Ok(value);
        }

        [HttpGet("path")]
        public IActionResult RouteWithStringParameter(string value = "")
        {
            return Ok(value);
        }

        [HttpGet("path")]
        public IActionResult RouteWithFloatParameter(float value = 0F)
        {
            return Ok(value);
        }

        [HttpGet("path")]
        public IActionResult RouteWithDecimalParameter(decimal value = 0M)
        {
            return Ok(value);
        }

        [HttpGet("path")]
        public IActionResult RouteWithCharParameter(char value = char.MinValue)
        {
            return Ok(value);
        }

        [HttpGet("path")]
        public IActionResult RouteWithGuidParameter(Guid value)
        {
            return Ok(value);
        }

        [HttpGet("path")]
        public IActionResult RouteWithSomeParameters(int a, string b, float c, decimal d, char e, Guid f)
        {
            return Ok();
        }

    }
}
