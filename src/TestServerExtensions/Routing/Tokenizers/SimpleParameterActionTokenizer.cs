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
            var parameters = action.MethodInfo.GetParameters();

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType.IsSimple())
                {
                    var tokenName = parameters[i].Name.ToLowerInvariant();
                    var tokenInstance = action.Arguments[i].Instance;

                    var tokenValue = tokenInstance is ConstantExpression expression
                        ? expression.Value
                        : tokenInstance;

                    tokens.AddToken(tokenName, Convert.ToString(tokenValue, CultureInfo.InvariantCulture), isConventional: false);
                }
            }
        }
    }
}
