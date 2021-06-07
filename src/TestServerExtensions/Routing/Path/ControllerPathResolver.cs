using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using TestServerExtensions.Routing.Tokenizers;

namespace TestServerExtensions.Routing.Templates
{
    internal class ControllerPathResolver
    {
        public string Resolve<T>(TokenCollection tokens)
        {
            if (!typeof(T).IsDefined(typeof(RouteAttribute)))
            {
                return string.Empty;
            }

            var routeDefinitions = typeof(T).GetCustomAttributes<RouteAttribute>();

            return routeDefinitions.Any()
                ? SubstituteConventionalTokens(routeDefinitions.First().Template.ToLowerInvariant(), tokens)
                : string.Empty;
        }

        private string SubstituteConventionalTokens(string template, TokenCollection tokens)
        {
            foreach (var token in tokens.GetConventionalTokens())
            {
                var conventionalToken = $"[{token.Name}]";

                if (template.Contains(conventionalToken))
                {
                    template = template
                        .Replace(conventionalToken, token.Value)
                        .ToLowerInvariant();

                    token.SetAsUsed();
                }
            }

            return template;
        }
    }
}
