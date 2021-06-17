using System;
using System.Globalization;
using System.Linq.Expressions;
using TestServerExtensions.Routing.Utils;

namespace TestServerExtensions.Routing.Tokenizers
{
    internal class SimpleParameterActionTokenizer : ITokenizer
    {
        public void AddTokens<T>(TestServerAction action, TokenCollection tokens) where T : class
        {
            foreach (var argument in action.Arguments)
            {
                if (argument.Type.IsSimple())
                {
                    var tokenName = argument.Name.ToLowerInvariant();

                    var tokenValue = argument.Instance is ConstantExpression expression
                        ? expression.Value
                        : argument.Instance;

                    tokens.AddToken(tokenName, Convert.ToString(tokenValue, CultureInfo.InvariantCulture), isConventional: false);
                }
            }
        }
    }
}
