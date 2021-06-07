using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using TestServerExtensions.Routing.Tokenizers;

namespace TestServerExtensions.Routing.Templates
{
    internal class RouteTemplateResolver : TemplateResolver
    {
        public override string Resolve(TestServerAction action, TokenCollection tokens)
        {
            var routeDefinitions = action.MethodInfo.GetCustomAttributes<RouteAttribute>();

            return routeDefinitions.Any()
                ? SubstitutePathTokens(routeDefinitions.First().Template.ToLowerInvariant(), tokens)
                : string.Empty;
        }
    }
}
