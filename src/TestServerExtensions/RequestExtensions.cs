using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using TestServerExtensions.Content;
using TestServerExtensions.Routing;

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
            AddFromFormArgument(requestBuilder, action);

            return requestBuilder;
        }

        private static void AddFromBodyArgument(RequestBuilder requestBuilder, TestServerAction action)
        {
            var fromBodyArgument = action.Arguments.SingleOrDefault(argument => argument.IsFromBody);
            if (fromBodyArgument != null)
            {
                requestBuilder.And(x => x.Content = new FromBodyContentBuilder().CreateHttpContent(fromBodyArgument));
            }
        }

        private static void AddFromFormArgument(RequestBuilder requestBuilder, TestServerAction action)
        {
            var fromFormArgument = action.Arguments.SingleOrDefault(argument => argument.IsFromForm);
            if (fromFormArgument != null)
            {
                requestBuilder.And(x => x.Content = new FromFormContentBuilder().CreateHttpContent(fromFormArgument));
            }
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
