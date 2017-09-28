using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JobStreet.AdPostingApi.Client.Hal;
using JobStreet.AdPostingApi.Client.Models;

namespace JobStreet.AdPostingApi.Client.Resources
{
    [MediaType("application/vnd.seekasia.advertisement+json;version=1")]
    public class AdvertisementResource : Advertisement, IResource
    {
        private Hal.Client _client;

        public AdvertisementError[] Errors { get; set; }

        public DateTime ExpiryDate { get; set; }

        public Guid Id { get; set; }

        [JsonIgnore]
        public Links Links { get; set; }

        [JsonIgnore]
        [Obsolete("The processing status will always be completed. All validation is done upfront and the advertisement will not fail once successfully submitted.")]
        [FromHeader("Processing-Status")]
        public ProcessingStatus ProcessingStatus { get; set; }

        [JsonIgnore]
        [FromHeader("X-Request-Id")]
        public string RequestId { get; set; }

        public AdvertisementState State { get; set; }

        public AdvertisementError[] Warnings { get; set; }

        [JsonIgnore]
        public Uri Uri => this.Links.GenerateLink("self");

        public async Task<AdvertisementResource> ExpireAsync()
        {
            return await this._client.PatchResourceAsync<AdvertisementResource, ExpireAdvertisementJsonPatch>(this.Uri, new ExpireAdvertisementJsonPatch());
        }

        public void Initialise(Hal.Client client)
        {
            this._client = client;
        }

        public async Task<AdvertisementResource> SaveAsync()
        {
            return await this._client.PutResourceAsync<AdvertisementResource, Advertisement>(this.Uri, this);
        }

        public bool ShouldSerializeErrors()
        {
            return false;
        }

        public bool ShouldSerializeState()
        {
            return false;
        }
    }
}