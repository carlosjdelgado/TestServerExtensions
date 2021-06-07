using Microsoft.AspNetCore.Mvc;
using System;
using TestServerExtensions.UnitTests.TestControllers.Models;

namespace TestServerExtensions.UnitTests.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexParametersPostController : ControllerBase
    {
        [HttpPost("path")]
        public IActionResult RouteWithComplexParameter([FromBody] ComplexParameter complexParameter)
        {
            return Ok();
        }

        [HttpPost("~/path")]
        public IActionResult RouteWithComplexParameterWithNullableProperties([FromBody] ComplexParameterWithNullableProperties parameter)
        {
            return Ok();
        }
    }
}
