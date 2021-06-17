using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestServerExtensions.Routing.Argument
{
    internal class TestServerArgument
    {
        private readonly ParameterInfo _parameterInfo;

        public object Instance { get; private set; }
        public Type Type => _parameterInfo.ParameterType;
        public string Name => _parameterInfo.Name;
        public IEnumerable<Attribute> Attributes => _parameterInfo.GetCustomAttributes();
        public bool IsFromBody => Attributes.Any(attribute => attribute is FromBodyAttribute);
        public bool IsFromForm => Attributes.Any(attribute => attribute is FromFormAttribute);

        public TestServerArgument(object instance, ParameterInfo parameterInfo)
        {
            Instance = instance;
            _parameterInfo = parameterInfo;
        }
    }
}
