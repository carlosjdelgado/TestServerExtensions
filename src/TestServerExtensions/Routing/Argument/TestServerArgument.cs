namespace TestServerExtensions.Routing.Argument
{
    internal class TestServerArgument
    {
        public object Instance { get; private set; }
        public bool IsFromBody { get; private set; }

        public TestServerArgument(object instance, bool isFromBody)
        {
            Instance = instance;
            IsFromBody = isFromBody;
        }
    }
}
