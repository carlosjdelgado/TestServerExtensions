using Microsoft.AspNetCore.Mvc;
using TestServerExtensions.UnitTests.TestControllers.Models;

namespace TestServerExtensions.UnitTests.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexParametersInRoutePostController : ControllerBase
    {
        [HttpPost("path/{intProperty:int}")]
        public IActionResult RouteWithComplexParameter(int intProperty, [FromBody] ComplexParameter complexParameter)
        {
            return Ok();
        }

        [HttpPost("path/{intProperty:int}")]
        public IActionResult RouteWithComplexParameterAndFromFormBody(int intProperty, [FromForm] ComplexParameter complexParameter)
        {
            return Ok();
        }

        [HttpPost]
        [Route("path/{doubleOptionalProperty?}")]
        public IActionResult RouteWithComplexParameterNotAllInRoute([FromBody] ComplexParameter complexParameter, double? doubleOptionalProperty)
        {
            return Ok();
        }

        [HttpPost("path/{stringProperty:string?}")]
        public IActionResult RouteWithComplexParameterWithNullableProperties(string stringProperty, [FromBody] ComplexParameterWithNullableProperties parameter)
        {
            return Ok();
        }

        [HttpPost("path/{stringProperty:string?}")]
        public IActionResult RouteWithComplexParameterWithNullablePropertiesAndFromFormBody(string stringProperty, [FromForm] ComplexParameterWithNullableProperties parameter)
        {
            return Ok();
        }
    }
}
