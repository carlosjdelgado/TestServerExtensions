using System.Linq;
using TestServerExtensions.Routing.Tokenizers;

namespace TestServerExtensions.Routing.Templates
{
    internal class QueryStringResolver
    {
        public string Resolve(TokenCollection tokens)
        {
            var unusedTokens = tokens.GetUnusedTokens();
            if (!unusedTokens.Any())
            {
                return string.Empty;
            }

            var queryStringChunks = unusedTokens.Select(token => $"{token.Name}={token.Value}");
            return $"?{string.Join("&", queryStringChunks)}";
        }
    }
}
