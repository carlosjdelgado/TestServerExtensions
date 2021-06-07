using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestServerExtensions.UnitTests.TestControllers;
using TestServerExtensions.UnitTests.TestControllers.Models;

namespace TestServerExtensions.UnitTests.TestCases.RequestExtensions
{
    [TestClass]
    public class ComplexParametersInRouteGetTest
    {
        private readonly TestServer _testServer;

        public ComplexParametersInRouteGetTest()
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
                .CreateRequest<ComplexParametersInRouteGetController>(c => c.RouteWithComplexParameter(parameter));

            var expectedPath = $"api/complexparametersinrouteget/path/1/2/TEST/0.000211/0.00121/0.0002/0.0001/C/D/{parameter.GuidProperty}/{parameter.GuidOptionalProperty}";

            request.GetPath().Should().Be(expectedPath);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithFullComplexParameterNotAllInRoute_Then_Resolve_Correct_Path()
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
                .CreateRequest<ComplexParametersInRouteGetController>(c => c.RouteWithComplexParameterNotAllInRoute(parameter));

            var expectedPath = $"api/complexparametersinrouteget/path/1/2/TEST/0.000211/0.00121/0.0002/0.0001/C/D?guidproperty={parameter.GuidProperty}&guidoptionalproperty={parameter.GuidOptionalProperty}";

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
                .CreateRequest<ComplexParametersInRouteGetController>(c => c.RouteWithComplexParameter(parameter));

            var expectedPath = $"api/complexparametersinrouteget/path/1/TEST/0.000211/0.0002/C/{parameter.GuidProperty}";

            request.GetPath().Should().Be(expectedPath);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithEmptyComplexParameter_Then_Resolve_Correct_Path()
        {
            var parameter = new ComplexParameter();

            var request = _testServer
                .CreateRequest<ComplexParametersInRouteGetController>(c => c.RouteWithComplexParameter(parameter));

            var expectedPath = $"api/complexparametersinrouteget/path/0/0/0/{parameter.CharProperty}/{Guid.Empty}";

            request.GetPath().Should().Be(expectedPath);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithEmptyComplexParameterWithNullableProperties_Then_Resolve_Correct_Path()
        {
            var parameter = new ComplexParameterWithNullableProperties();

            var request = _testServer
                .CreateRequest<ComplexParametersInRouteGetController>(c => c.RouteWithComplexParameterWithNullableProperties(parameter));

            var expectedPath = "api/complexparametersinrouteget/path";

            request.GetPath().Should().Be(expectedPath);
        }

    }
}
