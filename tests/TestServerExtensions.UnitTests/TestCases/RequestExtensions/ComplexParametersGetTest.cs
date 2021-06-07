using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestServerExtensions.UnitTests.TestControllers;
using TestServerExtensions.UnitTests.TestControllers.Models;

namespace TestServerExtensions.UnitTests.TestCases.RequestExtensions
{
    [TestClass]
    public class ComplexParametersGetTest
    {
        private readonly TestServer _testServer;

        public ComplexParametersGetTest()
        {
            _testServer = new TestServerBuilder().UseStartup().Build();
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithFullComplexParameter_Then_Resolve_Correct_Path()
        {
            var parameter = new ComplexParameter
            {
                IntProperty = 1,
                IntOptionalProperty = 2,
                StringProperty = "TEST",
                FloatProperty = 0.000211F,
                FloatOptionalProperty = 0.00121F,
                DecimalProperty = 0.0002M,
                DecimalOptionalProperty = 0.0001M,
                CharProperty = 'C',
                CharOptionalProperty = 'D',
                GuidProperty = Guid.NewGuid(),
                GuidOptionalProperty = Guid.NewGuid()
            };

            var request = _testServer
                .CreateRequest<ComplexParametersGetController>(c => c.RouteWithComplexParameter(parameter));

            var expectedPath = "api/complexparametersget/path?intproperty=1&intoptionalproperty=2&stringproperty=TEST&floatproperty=0.000211&" +
                              "floatoptionalproperty=0.00121&decimalproperty=0.0002&decimaloptionalproperty=0.0001&charproperty=C&charoptionalproperty=D&" +
                              $"guidproperty={parameter.GuidProperty}&guidoptionalproperty={parameter.GuidOptionalProperty}";

            request.GetPath().Should().Be(expectedPath);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithComplexParameterWithoutOptionalProperties_Then_Resolve_Correct_Path()
        {
            var parameter = new ComplexParameter
            {
                IntProperty = 1,
                StringProperty = "TEST",
                FloatProperty = 0.000211F,
                DecimalProperty = 0.0002M,
                CharProperty = 'C',
                GuidProperty = Guid.NewGuid()
            };

            var request = _testServer
                .CreateRequest<ComplexParametersGetController>(c => c.RouteWithComplexParameter(parameter));

            var ExpectedPath = "api/complexparametersget/path?intproperty=1&stringproperty=TEST&floatproperty=0.000211&" +
                              $"decimalproperty=0.0002&charproperty=C&guidproperty={parameter.GuidProperty}";

            request.GetPath().Should().Be(ExpectedPath);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithEmptyComplexParameter_Then_Resolve_Correct_Path()
        {
            var parameter = new ComplexParameter();

            var request = _testServer
                .CreateRequest<ComplexParametersGetController>(c => c.RouteWithComplexParameter(parameter));

            var expectedPath = "api/complexparametersget/path?intproperty=0&floatproperty=0&" +
                              $"decimalproperty=0&charproperty={parameter.CharProperty}&guidproperty={Guid.Empty}";

            request.GetPath().Should().Be(expectedPath);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithEmptyComplexParameterWithNullableProperties_Then_Resolve_Correct_Path()
        {
            var parameter = new ComplexParameterWithNullableProperties();

            var request = _testServer
                .CreateRequest<ComplexParametersGetController>(c => c.RouteWithComplexParameterWithNullableProperties(parameter));

            var expectedPath = "path";

            request.GetPath().Should().Be(expectedPath);
        }
    }
}
