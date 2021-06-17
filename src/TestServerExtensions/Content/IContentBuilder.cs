using System.Net.Http;
using TestServerExtensions.Routing.Argument;

namespace TestServerExtensions.Content
{
    internal interface IContentBuilder
    {
        HttpContent CreateHttpContent(TestServerArgument argument);
    }
}
