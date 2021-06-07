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
    public class ComplexParametersPostTest
    {
        private readonly TestServer _testServer;

        public ComplexParametersPostTest()
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
                .CreateRequest<ComplexParametersPostController>(c => c.RouteWithComplexParameter(parameter));

            var expectedPath = "api/complexparameterspost/path";
            request.GetPath().Should().Be(expectedPath);

            var content = JsonSerializer.Deserialize<ComplexParameter>(request.GetContentAsString());
            content.Should().BeEquivalentTo(parameter);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithComplexParameterWithoutOptionalProperties_Then_Resolve_Correct_PathAndContent()
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
                .CreateRequest<ComplexParametersPostController>(c => c.RouteWithComplexParameter(parameter));

            var expectedPath = "api/complexparameterspost/path";
            request.GetPath().Should().Be(expectedPath);

            var content = JsonSerializer.Deserialize<ComplexParameter>(request.GetContentAsString());
            content.Should().BeEquivalentTo(parameter);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithEmptyComplexParameter_Then_Resolve_Correct_PathAndContent()
        {
            var parameter = new ComplexParameter();

            var request = _testServer
                .CreateRequest<ComplexParametersPostController>(c => c.RouteWithComplexParameter(parameter));

            var expectedPath = "api/complexparameterspost/path";
            request.GetPath().Should().Be(expectedPath);

            var content = JsonSerializer.Deserialize<ComplexParameter>(request.GetContentAsString());
            content.Should().BeEquivalentTo(parameter);
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithEmptyComplexParameterWithNullableProperties_Then_Resolve_Correct_PathAndContent()
        {
            var parameter = new ComplexParameterWithNullableProperties();

            var request = _testServer
                .CreateRequest<ComplexParametersPostController>(c => c.RouteWithComplexParameterWithNullableProperties(parameter));

            var expectedPath = "path";
            request.GetPath().Should().Be(expectedPath);

            var content = JsonSerializer.Deserialize<ComplexParameter>(request.GetContentAsString());
            content.Should().BeEquivalentTo(parameter);
        }
    }
}
