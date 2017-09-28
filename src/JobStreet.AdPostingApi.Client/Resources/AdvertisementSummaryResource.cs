using System;
using Newtonsoft.Json;
using JobStreet.AdPostingApi.Client.Hal;
using JobStreet.AdPostingApi.Client.Models;

namespace JobStreet.AdPostingApi.Client.Resources
{
    public class AdvertisementSummaryResource : AdvertisementSummary, IResource
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public Links Links { get; set; }

        [JsonIgnore]
        public Uri Uri => this.Links.GenerateLink("self");

        public void Initialise(Hal.Client client)
        {
        }
    }
}