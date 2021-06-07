using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestServerExtensions.UnitTests.TestControllers;

namespace TestServerExtensions.UnitTests.TestCases.RequestExtensions
{
    [TestClass]
    public class SimpleParametersInRouteGetTest
    {
        private readonly TestServer _testServer;

        public SimpleParametersInRouteGetTest()
        {
            _testServer = new TestServerBuilder().UseStartup().Build();
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithIntParameter_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersInRouteGetController>(c => c.RouteWithIntParameter(1));

            request.GetPath().Should().Be("api/simpleparametersinrouteget/path/1");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithStringParameter_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersInRouteGetController>(c => c.RouteWithStringParameter("test"));

            request.GetPath().Should().Be("api/simpleparametersinrouteget/path/test");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithFloatParameter_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersInRouteGetController>(c => c.RouteWithFloatParameter(1.00001F));

            request.GetPath().Should().Be("api/simpleparametersinrouteget/path/1.00001");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithDecimalParameter_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersInRouteGetController>(c => c.RouteWithDecimalParameter(1.00001M));

            request.GetPath().Should().Be("api/simpleparametersinrouteget/path/1.00001");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithGuidParameter_Then_Resolve_Correct_Path()
        {
            var guid = Guid.NewGuid();

            var request = _testServer
                .CreateRequest<SimpleParametersInRouteGetController>(c => c.RouteWithGuidParameter(guid));

            request.GetPath().Should().Be($"api/simpleparametersinrouteget/path/{guid}");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithSomeParameters_Then_Resolve_Correct_Path()
        {
            var guid = Guid.NewGuid();

            var request = _testServer
                .CreateRequest<SimpleParametersInRouteGetController>(c => c.RouteWithSomeParameters(1, "test", 0.1234F, 0.4321M, 'T', guid));

            request.GetPath().Should().Be($"api/simpleparametersinrouteget/path/1/test/0.1234/0.4321/T/{guid}");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithSomeParametersNotAllInRoute_Then_Resolve_Correct_Path()
        {
            var guid = Guid.NewGuid();

            var request = _testServer
                .CreateRequest<SimpleParametersInRouteGetController>(c => c.RouteWithSomeParametersNotAllInRoute(1, "test", 0.1234F, 0.4321M, 'T', guid));

            request.GetPath().Should().Be($"api/simpleparametersinrouteget/path/1/test/0.1234/0.4321/T?f={guid}");
        }
    }
}
