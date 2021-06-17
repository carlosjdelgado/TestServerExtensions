namespace TestServerExtensions.Routing.Tokenizers
{
    internal class Token
    {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public bool IsConventional { get; private set; }
        public bool IsUsed { get; private set; }
        
        public Token(string name, string value, bool isConvencional)
        {
            Name = name;
            Value = value;
            IsConventional = isConvencional;
        }

        public void SetAsUsed()
        {
            IsUsed = true;
        }
    }
}
