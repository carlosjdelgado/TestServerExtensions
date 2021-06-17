using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Linq;
using TestServerExtensions.Routing.Argument;
using TestServerExtensions.Routing.Utils;

namespace TestServerExtensions.Routing.Tokenizers
{
    internal class ComplexParameterActionTokenizer : ITokenizer
    {
        public void AddTokens<T>(TestServerAction action, TokenCollection tokens) where T : class
        {
            foreach (var argument in action.Arguments)
            {
                if (argument.Type.IsSimple() || IgnoreBind(argument))
                {
                    continue;
                }

                foreach (var property in argument.Type.GetProperties())
                {
                    var tokenName = property.Name.ToLowerInvariant();
                    var value = property.GetValue(argument.Instance);

                    if (value != null)
                    {
                        tokens.AddToken(tokenName, Convert.ToString(value, CultureInfo.InvariantCulture), isConventional: false);
                    }
                }
            }
        }

        private bool IgnoreBind(TestServerArgument argument)
        {
            return argument.Attributes.Any(attribute =>
                attribute is FromBodyAttribute ||
                attribute is FromFormAttribute ||
                attribute is BindNeverAttribute);
        }
    }
}
