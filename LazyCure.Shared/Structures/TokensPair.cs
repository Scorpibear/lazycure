namespace LifeIdea.LazyCure.Shared.Structures
{
    public struct TokensPair
    {
        public string Token, TokenSecret;

        public TokensPair(string token, string tokenSecret)
        {
            Token = token;
            TokenSecret = tokenSecret;
        }

        public bool IsValid
        {
            get
            {
                return !(string.IsNullOrWhiteSpace(Token) || string.IsNullOrWhiteSpace(TokenSecret));
            }
        }

        public static TokensPair Empty { get { return new TokensPair(null, null); } }
    }
}