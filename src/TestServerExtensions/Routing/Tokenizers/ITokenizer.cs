
namespace TestServerExtensions.Routing.Tokenizers
{
    internal interface ITokenizer
    {
        void AddTokens<T>(TestServerAction action, TokenCollection tokens) where T : class;
    }
}
