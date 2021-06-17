using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using TestServerExtensions.Routing.Argument;

namespace TestServerExtensions.Content
{
    internal class FromFormContentBuilder : IContentBuilder
    {
        public HttpContent CreateHttpContent(TestServerArgument argument)
        {
            return new FormUrlEncodedContent(ToDictionary(argument.Instance));
        }

        private IDictionary<string, string> ToDictionary(object instance)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(instance))
            {
                var value = property.GetValue(instance);                
                if (value != null)
                {
                    dictionary.Add(property.Name, Convert.ToString(value, CultureInfo.InvariantCulture));
                }
            }
            return dictionary;
        }
    }
}
