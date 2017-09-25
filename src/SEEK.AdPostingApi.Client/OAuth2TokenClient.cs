﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SEEK.AdPostingApi.Client.Models;

namespace SEEK.AdPostingApi.Client
{
    internal class OAuth2TokenClient : IOAuth2TokenClient
    {
        private readonly string _id;
        private readonly string _secret;
        private readonly Uri _tokenUri;
        private readonly HttpClient _httpClient;

        public OAuth2TokenClient(string id, string secret)
        {
            _id = id;
            _secret = secret;
            //_tokenUri = new Uri("https://api.seek.com.au/auth/oauth2/token");
            _tokenUri = new Uri("http://seekasiaidentityserver-test.ap-southeast-1.elasticbeanstalk.com/connect/token");
            _httpClient = new HttpClient();
        }

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public async Task<OAuth2Token> GetOAuth2TokenAsync()
        {
            using (var tokenRequest = new HttpRequestMessage(HttpMethod.Post, _tokenUri))
            {
                var content = $"client_id={_id}&client_secret={_secret}&grant_type=client_credentials";

                tokenRequest.Content = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");

                using (HttpResponseMessage response = await _httpClient.SendAsync(tokenRequest))
                {
                    await HandleBadResponseAsync(response);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<OAuth2Token>(responseContent, _jsonSettings);

                    return token;
                }
            }
        }

        private async Task HandleBadResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            IEnumerable<string> requestIds;
            string requestId = response.Headers.TryGetValues("X-Request-Id", out requestIds) ? requestIds.First() : null;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException(requestId, (int)response.StatusCode, "Could not get OAuth2 access token.");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            throw new RequestException(requestId, (int)response.StatusCode, response.ReasonPhrase, responseContent, response.Content.Headers.ContentType?.MediaType);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}