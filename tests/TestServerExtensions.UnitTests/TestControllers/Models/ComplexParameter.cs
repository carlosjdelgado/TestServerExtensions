using System;

namespace TestServerExtensions.UnitTests.TestControllers.Models
{
    public class ComplexParameter
    {
        public int IntProperty { get; set; }
        public int? IntOptionalProperty { get; set; }
        public string StringProperty { get; set; }
        public float FloatProperty { get; set; }
        public float? FloatOptionalProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public decimal? DecimalOptionalProperty { get; set; }
        public char CharProperty { get; set; }
        public char? CharOptionalProperty { get; set; }
        public Guid GuidProperty { get; set; }
        public Guid? GuidOptionalProperty { get; set; }
    }
}
