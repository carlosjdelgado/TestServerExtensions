using System.Collections.Generic;
using TestServerExtensions.Routing.Templates;
using TestServerExtensions.Routing.Tokenizers;

namespace TestServerExtensions.Routing
{
    internal static class PathResolver
    {
        private static readonly IEnumerable<ITokenizer> _tokenizers = new List<ITokenizer>
        {
            new SimpleParameterActionTokenizer(),
            new ComplexParameterActionTokenizer(),
            new DefaultConventionalTokenizer()
        };

        public static string Resolve<T>(TestServerAction action) where T : class
        {
            var tokens = GetTokens<T>(action);

            var controllerPath = new ControllerPathResolver().Resolve<T>(tokens);

            var httpMethodTemplate = new HttpMethodTemplateResolver().Resolve(action, tokens);
            var routeTemplate = new RouteTemplateResolver().Resolve(action, tokens);
            var template = httpMethodTemplate ?? routeTemplate;

            var queryString = new QueryStringResolver().Resolve(tokens);

            return GetPath(controllerPath, template, queryString);
        }

        private static TokenCollection GetTokens<T>(TestServerAction action) where T : class
        {
            var tokens = new TokenCollection();

            foreach (var tokenizer in _tokenizers)
            {
                tokenizer.AddTokens<T>(action, tokens);
            }

            return tokens;
        }

        private static string GetPath(string controllerPath, string template, string queryString)
        {
            if (template == null)
            {
                return $"{controllerPath}{queryString}";
            }

            if (template.StartsWith("~/"))
            {
                return $"{template[2..]}{queryString}";
            }

            return $"{controllerPath}/{template}{queryString}";
        }
    }
}
