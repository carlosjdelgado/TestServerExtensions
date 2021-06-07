using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestServerExtensions.UnitTests.TestControllers;

namespace TestServerExtensions.UnitTests.TestCases.RequestExtensions
{
    [TestClass]
    public class SimpleParamentersGetTest
    {
        private readonly TestServer _testServer;
        
        public SimpleParamentersGetTest()
        {
            _testServer = new TestServerBuilder().UseStartup().Build();
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithoutParameters_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersGetController>(c => c.RouteWithoutParameters());

            request.GetPath().Should().Be("api/simpleparametersget/path");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithIntParameter_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersGetController>(c => c.RouteWithIntParameter(1));

            request.GetPath().Should().Be("api/simpleparametersget/path?value=1");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithStringParameter_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersGetController>(c => c.RouteWithStringParameter("test"));

            request.GetPath().Should().Be("api/simpleparametersget/path?value=test");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithFloatParameter_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersGetController>(c => c.RouteWithFloatParameter(1.00001F));

            request.GetPath().Should().Be("api/simpleparametersget/path?value=1.00001");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithDecimalParameter_Then_Resolve_Correct_Path()
        {
            var request = _testServer
                .CreateRequest<SimpleParametersGetController>(c => c.RouteWithDecimalParameter(1.00001M));

            request.GetPath().Should().Be("api/simpleparametersget/path?value=1.00001");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithGuidParameter_Then_Resolve_Correct_Path()
        {
            var guid = Guid.NewGuid();

            var request = _testServer
                .CreateRequest<SimpleParametersGetController>(c => c.RouteWithGuidParameter(guid));

            request.GetPath().Should().Be($"api/simpleparametersget/path?value={guid}");
        }

        [TestMethod]
        public void When_CreateRequestFor_RouteWithSomeParameters_Then_Resolve_Correct_Path()
        {
            var guid = Guid.NewGuid();

            var request = _testServer
                .CreateRequest<SimpleParametersGetController>(c => c.RouteWithSomeParameters(1, "test", 0.1234F, 0.4321M, 'T', guid));

            request.GetPath().Should().Be($"api/simpleparametersget/path?a=1&b=test&c=0.1234&d=0.4321&e=T&f={guid}");
        }
    }
}
