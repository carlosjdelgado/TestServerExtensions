using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using TestServerExtensions.UnitTests.TestControllers;
using TestServerExtensions.UnitTests.TestControllers.Models;

namespace TestServerExtensions.UnitTests.TestCases.RequestExtensions
{
    [TestClass]
    public class ComplexParametersInRoutePostTest
    {
        private readonly TestServer _testServer;

        public ComplexParametersInRoutePostTest()
        {
            _testServer = new TestServerBuilder().UseStartup().Build();
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithFullComplexParameter_Then_Resolve_Correct_PathAndContent()
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
                .CreateRequest<ComplexParametersInRoutePostController>(c => c.RouteWithComplexParameter(1, parameter));

            var expectedPath = $"api/complexparametersinroutepost/path/1";
            request.GetPath().Should().Be(expectedPath);

            var content = JsonSerializer.Deserialize<ComplexParameter>(request.GetContentAsString());
            content.Should().BeEquivalentTo(parameter);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithFullComplexParameterAndFromFormBody_Then_Resolve_Correct_PathAndContent()
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
                .CreateRequest<ComplexParametersInRoutePostController>(c => c.RouteWithComplexParameterAndFromFormBody(1, parameter));

            var expectedPath = $"api/complexparametersinroutepost/path/1";
            request.GetPath().Should().Be(expectedPath);

            var expectedContent = "IntProperty=1&IntOptionalProperty=2&StringProperty=TEST&FloatProperty=0.000211&FloatOptionalProperty=0.00121&" +
                "DecimalProperty=0.0002&DecimalOptionalProperty=0.0001&CharProperty=C&CharOptionalProperty=D&" +
                $"GuidProperty={parameter.GuidProperty}&GuidOptionalProperty={parameter.GuidOptionalProperty}";
            var content = request.GetContentAsString();
            content.Should().Be(expectedContent);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithFullComplexParameterNotAllInRoute_Then_Resolve_Correct_PathAndContent()
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
                .CreateRequest<ComplexParametersInRoutePostController>(c => c.RouteWithComplexParameterNotAllInRoute(parameter, null));

            var expectedPath = $"api/complexparametersinroutepost/path/";
            request.GetPath().Should().Be(expectedPath);

            var content = JsonSerializer.Deserialize<ComplexParameter>(request.GetContentAsString());
            content.Should().BeEquivalentTo(parameter);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithEmptyComplexParameterWithNullableProperties_Then_Resolve_Correct_PathAndContent()
        {
            var parameter = new ComplexParameterWithNullableProperties();

            var request = _testServer
                .CreateRequest<ComplexParametersInRoutePostController>(c => c.RouteWithComplexParameterWithNullableProperties(null, parameter));

            var expectedPath = "api/complexparametersinroutepost/path/";
            request.GetPath().Should().Be(expectedPath);

            var content = JsonSerializer.Deserialize<ComplexParameter>(request.GetContentAsString());
            content.Should().BeEquivalentTo(parameter);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithEmptyComplexParameterWithNullablePropertiesAndFromFormBody_Then_Resolve_Correct_PathAndContent()
        {
            var parameter = new ComplexParameterWithNullableProperties();

            var request = _testServer
                .CreateRequest<ComplexParametersInRoutePostController>(c => c.RouteWithComplexParameterWithNullablePropertiesAndFromFormBody(null, parameter));

            var expectedPath = "api/complexparametersinroutepost/path/";
            request.GetPath().Should().Be(expectedPath);

            var content = request.GetContentAsString();
            content.Should().Be(string.Empty);
        }
    }
}
