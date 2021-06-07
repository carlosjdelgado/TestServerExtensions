using System.Collections.Generic;
using System.Linq;

namespace TestServerExtensions.Routing.Tokenizers
{
    internal class TokenCollection
    {
        private readonly Dictionary<string, Token> _activeTokens;

        public TokenCollection()
        {
            _activeTokens = new Dictionary<string, Token>();
        }

        public bool ContainsToken(string tokenName)
        {
            return _activeTokens.ContainsKey(tokenName);
        }

        public void AddToken(string tokenName, string tokenValue, bool isConventional = false)
        {
            if (!ContainsToken(tokenName))
            {
                _activeTokens.Add(tokenName,
                    new Token(tokenName, tokenValue, isConventional));
            }
        }

        internal IEnumerable<Token> GetUnusedTokens()
        {
            return _activeTokens.Values
                .Where(token => !token.IsConventional && !token.Used);
        }

        internal IEnumerable<Token> GetConventionalTokens()
        {
            return _activeTokens.Values
                .Where(token => token.IsConventional);
        }

        internal Token Find(string tokenName)
        {
            if (ContainsToken(tokenName))
            {
                return _activeTokens[tokenName];
            }

            return default;
        }
    }
}
