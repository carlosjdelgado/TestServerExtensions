using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using System.Reflection;
using TestServerExtensions.Routing.Tokenizers;

namespace TestServerExtensions.Routing.Templates
{
    internal class HttpMethodTemplateResolver : TemplateResolver
    {
        public override string Resolve(TestServerAction action, TokenCollection tokens)
        {
            var getAttributes = action.MethodInfo.GetCustomAttributes<HttpGetAttribute>()
                .OfType<HttpMethodAttribute>();

            var postAttributes = action.MethodInfo.GetCustomAttributes<HttpPostAttribute>()
                .OfType<HttpMethodAttribute>();

            var putAttributes = action.MethodInfo.GetCustomAttributes<HttpPutAttribute>()
                .OfType<HttpMethodAttribute>();

            var deleteAttributes = action.MethodInfo.GetCustomAttributes<HttpDeleteAttribute>()
                .OfType<HttpMethodAttribute>();

            var verbAttribute = getAttributes
                .Union(postAttributes)
                .Union(putAttributes)
                .Union(deleteAttributes)
                .SingleOrDefault();

            if (verbAttribute?.Template != null)
            {
                return SubstitutePathTokens(verbAttribute.Template.ToLowerInvariant(), tokens);
            }

            return null;
        }
    }
}
