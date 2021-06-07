using Microsoft.AspNetCore.Mvc;
using TestServerExtensions.UnitTests.TestControllers.Models;

namespace TestServerExtensions.UnitTests.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexParametersInRouteGetController : ControllerBase
    {
        [HttpGet("path/{IntProperty:int}/{IntOptionalProperty:int?}/{StringProperty?}/{FloatProperty:float}/{FloatOptionalProperty?}/{DecimalProperty}/{DecimalOptionalProperty?}/{CharProperty}/{CharOptionalProperty?}/{GuidProperty}/{GuidOptionalProperty?}")]
        public IActionResult RouteWithComplexParameter(ComplexParameter complexParameter)
        {
            return Ok();
        }

        [HttpGet]
        [Route("path/{IntProperty}/{IntOptionalProperty?}/{StringProperty?}/{FloatProperty}/{FloatOptionalProperty?}/{DecimalProperty}/{DecimalOptionalProperty?}/{CharProperty}/{CharOptionalProperty?}")]
        public IActionResult RouteWithComplexParameterNotAllInRoute(ComplexParameter complexParameter)
        {
            return Ok();
        }

        [HttpGet("path/{stringProperty:string?}")]
        public IActionResult RouteWithComplexParameterWithNullableProperties(ComplexParameterWithNullableProperties parameter)
        {
            return Ok();
        }
    }
}
