using System.Linq;
using System.Text.RegularExpressions;
using TestServerExtensions.Routing.Tokenizers;

namespace TestServerExtensions.Routing.Templates
{
    internal abstract class TemplateResolver
    {
        public abstract string Resolve(TestServerAction action, TokenCollection tokens);

        public virtual string SubstitutePathTokens(string template, TokenCollection tokens)
        {
            var tokenRegexPattern = "{[a-zA-Z0-9?]*:??[a-zA-Z0-9?]*}";
            var tokenMatches = Regex.Matches(template, tokenRegexPattern, RegexOptions.Compiled);

            foreach (Match match in tokenMatches)
            {
                var tokenNameRegexPattern = @"{([^\?:}]*)";
                var tokenName = Regex.Match(match.Value, tokenNameRegexPattern)
                    .Groups
                    .Values
                    .LastOrDefault()?
                    .Value;

                var token = tokens.Find(tokenName);
                if (token != default(Token))
                {
                    template = template.Replace(match.Value, token.Value);

                    token.SetAsUsed();
                }
            }

            var unusedOptionalTokenRegexPattern = @"\/{[^{}]*\?}";
            template = Regex.Replace(template, unusedOptionalTokenRegexPattern, string.Empty);

            return template;
        }
    }
}
