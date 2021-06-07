using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace TestServerExtensions.UnitTests
{
    internal static class RequestBuilderExtensions
    {
        public static string GetPath(this RequestBuilder requestBuilder)
        {
            var req = typeof(RequestBuilder).GetField("_req", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var message = (HttpRequestMessage)req.GetValue(requestBuilder);
            return message.RequestUri.ToString();
        }

        public static string GetContentAsString(this RequestBuilder requestBuilder)
        {
            var req = typeof(RequestBuilder).GetField("_req", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var message = (HttpRequestMessage)req.GetValue(requestBuilder);
            return message.Content?.ReadAsStringAsync().Result;
        }
    }
}
