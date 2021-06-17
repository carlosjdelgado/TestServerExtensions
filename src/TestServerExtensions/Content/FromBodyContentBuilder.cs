using System.Net.Http;
using System.Text;
using System.Text.Json;
using TestServerExtensions.Routing.Argument;

namespace TestServerExtensions.Content
{
    internal class FromBodyContentBuilder : IContentBuilder
    {
        public HttpContent CreateHttpContent(TestServerArgument argument)
        {
            return new StringContent(JsonSerializer.Serialize(argument.Instance), Encoding.UTF8, "application/json");
        }
    }
}
