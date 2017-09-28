using System;
using System.Threading.Tasks;
using JobStreet.AdPostingApi.Client.Models;

namespace JobStreet.AdPostingApi.Client
{
    internal interface IOAuth2TokenClient : IDisposable
    {
        Task<OAuth2Token> GetOAuth2TokenAsync();
    }
}