using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using TestServerExtensions.Routing;
using TestServerExtensions.Routing.Argument;

namespace Microsoft.AspNetCore.TestHost
{
    public static class RequestExtensions
    {
        public static RequestBuilder CreateRequest<T>(this TestServer server, Expression<Func<T, object>> actionSelector) where T : ControllerBase
        {
            var action = GetAction(actionSelector);
            var path = PathResolver.Resolve<T>(action);
            
            var requestBuilder = server.CreateRequest(path);
            AddFromBodyArgument(requestBuilder, action);

            return requestBuilder;
        }

        private static void AddFromBodyArgument(RequestBuilder requestBuilder, TestServerAction action)
        {
            var fromBodyArgument = action.Arguments.SingleOrDefault(argument => argument.IsFromBody);
            if (fromBodyArgument != null)
            {
                requestBuilder.And(x => x.Content = CreateHttpContent(fromBodyArgument));
            }
        }

        private static HttpContent CreateHttpContent(TestServerArgument fromBodyArgument)
        {
            return new StringContent(JsonSerializer.Serialize(fromBodyArgument.Instance), Encoding.UTF8, "application/json");
        }

        private static TestServerAction GetAction<T>(Expression<Func<T, object>> actionSelector) where T : ControllerBase
        {
            if (actionSelector.NodeType != ExpressionType.Lambda)
            {
                throw new InvalidOperationException($"The action selector is not a valid lambda expression");
            }

            var methodCall = (MethodCallExpression)actionSelector.Body;
            return new TestServerAction(methodCall);
        }
    }
}
