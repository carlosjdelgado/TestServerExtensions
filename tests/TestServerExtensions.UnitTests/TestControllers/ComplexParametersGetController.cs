using Microsoft.AspNetCore.Mvc;
using TestServerExtensions.UnitTests.TestControllers.Models;

namespace TestServerExtensions.UnitTests.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexParametersGetController : ControllerBase
    {
        [HttpGet("path")]
        public IActionResult RouteWithComplexParameter(ComplexParameter complexParameter)
        {
            return Ok();
        }

        [HttpGet("~/path")]
        public IActionResult RouteWithComplexParameterWithNullableProperties(ComplexParameterWithNullableProperties parameter)
        {
            return Ok();
        }

    }
}
