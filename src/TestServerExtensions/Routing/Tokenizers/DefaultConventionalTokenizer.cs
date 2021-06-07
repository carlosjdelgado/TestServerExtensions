namespace TestServerExtensions.Routing.Tokenizers
{
    internal class DefaultConventionalTokenizer : ITokenizer
    {
        public void AddTokens<T>(TestServerAction action, TokenCollection tokens) where T : class
        {
            const string ControllerTypeNameSuffix = "Controller";

            const string controllerKey = "controller";

            if (!tokens.ContainsToken(controllerKey))
            {
                var controllerName = typeof(T).Name
                    .Replace(ControllerTypeNameSuffix, string.Empty);

                tokens.AddToken(controllerKey, controllerName, isConventional: true);
            }
        }
    }
}
