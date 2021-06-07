using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TestServerExtensions.UnitTests
{
    public class TestServerBuilder
    {
        private readonly WebHostBuilder _webHostBuilder;

        public TestServerBuilder()
        {
            _webHostBuilder = new WebHostBuilder();

        }

        public TestServerBuilder UseStartup()
        {
            _webHostBuilder.UseStartup<Startup>();

            return this;
        }


        public TestServer Build()
        {
            return new TestServer(_webHostBuilder);
        }

        class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddApplicationPart(Assembly.Load(new AssemblyName("TestServerExtensions.UnitTests")));
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
}
