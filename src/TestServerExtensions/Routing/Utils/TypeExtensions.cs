using System;
using System.Collections.Generic;
using System.Linq;

namespace TestServerExtensions.Routing.Utils
{
    public static class TypeExtensions
    {
        private static readonly List<Type> _simpleTypes = new List<Type>
        {
            typeof(int?),
            typeof(string),
            typeof(decimal),
            typeof(decimal?),
            typeof(float?),
            typeof(double?),
            typeof(Guid)
        };

        public static bool IsSimple(this Type type)
        {
            return type.IsPrimitive || _simpleTypes.Any(simpleType => type == simpleType);
        }
    }
}
