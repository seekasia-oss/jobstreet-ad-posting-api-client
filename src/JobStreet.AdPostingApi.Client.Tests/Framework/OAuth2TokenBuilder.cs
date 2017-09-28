using JobStreet.AdPostingApi.Client.Models;

namespace JobStreet.AdPostingApi.Client.Tests.Framework
{
    public class OAuth2TokenBuilder
    {
        private string _accessToken;

        public OAuth2TokenBuilder()
        {
            this.WithAccessToken(AccessTokens.ValidAccessToken);
        }

        public OAuth2TokenBuilder WithAccessToken(string accessToken)
        {
            this._accessToken = accessToken;
            return this;
        }

        public OAuth2Token Build()
        {
            return new OAuth2Token
            {
                AccessToken = this._accessToken,
                ExpiresIn = 3600,
                Scope = "seek",
                TokenType = "Bearer"
            };
        }
    }
}