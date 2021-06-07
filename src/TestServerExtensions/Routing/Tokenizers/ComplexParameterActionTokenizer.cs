using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Reflection;
using TestServerExtensions.Routing.Utils;

namespace TestServerExtensions.Routing.Tokenizers
{
    internal class ComplexParameterActionTokenizer : ITokenizer
    {
        public void AddTokens<T>(TestServerAction action, TokenCollection tokens) where T : class
        {
            var parameters = action.MethodInfo.GetParameters();

            for (int i = 0; i < parameters.Length; i++)
            {
                var type = parameters[i].ParameterType;

                if (!type.IsSimple())
                {
                    if (!IgnoreBind(parameters[i]))
                    {
                        foreach (var property in type.GetProperties())
                        {
                            var tokenName = property.Name.ToLowerInvariant();
                            var value = property.GetValue(action.Arguments[i].Instance);

                            if (value != null)
                            {
                                tokens.AddToken(tokenName, Convert.ToString(value, CultureInfo.InvariantCulture), isConventional: false);
                            }
                        }
                    }
                }
            }
        }

        private bool IgnoreBind(ParameterInfo parameter)
        {
            var attributes = parameter.GetCustomAttributes(false);

            foreach (var attribute in attributes)
            {
                if (attribute is FromBodyAttribute ||
                    attribute is FromFormAttribute ||
                    attribute is BindNeverAttribute)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
